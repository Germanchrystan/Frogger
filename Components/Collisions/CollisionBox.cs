using System;
using System.Collections.Generic;
using Prefabs;

namespace Components.Collisions
{
  public class CollisionData 
  {
    private string colliderTag;
    private Transform colliderTransform;
    public string ColliderTag { get { return colliderTag;}}
    public Transform ColliderTransform { get { return colliderTransform;}}

    public CollisionData(string colliderTag, Transform colliderTransform)
    {
      this.colliderTag = colliderTag;
      this.colliderTransform = colliderTransform;
    }
  }
  
  public struct CollisionList
  {
    public int activeNum;
    public List<CollisionBox> list;
  }

  public class CollisionBox : Component
  {
    // Static properties
    public static Dictionary<string, CollisionList> CollisionBoxCollection = new Dictionary<string, CollisionList>();
    public static void AddToCollection(CollisionBox collisionBox)
    {
      if(!CollisionBoxCollection.ContainsKey(collisionBox.GetTag()))
      {
        CollisionBoxCollection[collisionBox.GetTag()] = new CollisionList(){ activeNum = 0, list = new List<CollisionBox>()};
      }
        CollisionBoxCollection[collisionBox.GetTag()].list.Add(collisionBox);
    }
    public static void DeactivateColliderBox(CollisionBox collisionBox)
    {
      CollisionList collisionListItem = CollisionBoxCollection[collisionBox.GetTag()];
      int index = collisionListItem.list.IndexOf(collisionBox);
      if(index == -1) throw new Exception("Collision box was not present in collection");
      
      CollisionBox temp = collisionListItem.list[collisionListItem.list.Count - 1];
      collisionListItem.list[collisionListItem.list.Count - 1] = collisionBox;
      collisionListItem.list [index] = temp;
      
      collisionListItem.activeNum--;
    }

    public static void ActivateColliderBox(CollisionBox collisionBox)
    {
      CollisionList collisionListItem = CollisionBoxCollection[collisionBox.GetTag()];
      int index = collisionListItem.list.IndexOf(collisionBox);
      if(index == -1) throw new Exception("Collision box was not present in collection");

      CollisionBox temp = collisionListItem.list[collisionListItem.activeNum];
      collisionListItem.list[collisionListItem.activeNum] = collisionBox;
      collisionListItem.list[index] = temp;

      collisionListItem.activeNum++;
    }

    // Instance
    private string tag;
    private Transform transform;
    private bool active;

    public event EventHandler<CollisionData> OnCollision;

    // Constructor without metadata
    public CollisionBox(Transform transform, string tag, CollisionHandler parentCollisionHandler)
    {
      active = true;
      this.transform = transform;
      this.tag = tag;
      AddToCollection(this);
      OnCollision += parentCollisionHandler.OnCollision;
    }

    public bool IsActive() { return active; }
    public void SetActive(bool value) { active = value; }
    public string GetTag() { return tag; }
    public Transform GetTransform() { return transform; }

    public void CollisionDetected(CollisionBox collider)
    {
      if (OnCollision != null) OnCollision(this, new CollisionData(collider.GetTag(), collider.GetTransform()));
    }
    ~CollisionBox() { CollisionBoxCollection[GetTag()].list.Remove(this); }
  }
}
