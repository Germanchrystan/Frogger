using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Prototypes;

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
  public class CollisionList
  {
    private int activeNum;
    public int ActiveNum { get { return activeNum; } set { activeNum = value; }}
    public List<CollisionBox> list;
    public CollisionList()
    {
      this.activeNum = 0;
      this.list = new List<CollisionBox>();
    }
  }

  public class CollisionBox : Component
  {
    // Static properties
    public static Dictionary<string, CollisionList> CollisionBoxCollection = new Dictionary<string, CollisionList>();
    public static void AddToCollection(CollisionBox collisionBox)
    {
      if(!CollisionBoxCollection.ContainsKey(collisionBox.GetTag()))
      {
        CollisionBoxCollection[collisionBox.GetTag()] = new CollisionList();
      }
        CollisionBoxCollection[collisionBox.GetTag()].ActiveNum += 1;
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
      
      collisionListItem.ActiveNum--;
    }

    public static void ActivateColliderBox(CollisionBox collisionBox)
    {
      CollisionList collisionListItem = CollisionBoxCollection[collisionBox.GetTag()];
      int index = collisionListItem.list.IndexOf(collisionBox);
      if(index == -1) throw new Exception("Collision box was not present in collection");

      CollisionBox temp = collisionListItem.list[collisionListItem.ActiveNum];
      collisionListItem.list[collisionListItem.ActiveNum] = collisionBox;
      collisionListItem.list[index] = temp;

      collisionListItem.ActiveNum++;
    }

    // Instance
    private string tag;
    private Transform transform;
    private bool active;
    private GameObject parent;
    // Remove this once child transform is tested
    // private Renderer renderer;

    public event EventHandler<CollisionBox> OnCollision;

    // Constructor without child transform
    public CollisionBox(string tag, CollisionHandler parentCollisionHandler, GameObject parent)
    {
      active = true;
      this.tag = tag;
      this.parent = parent;
      this.transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      AddToCollection(this);
      OnCollision += parentCollisionHandler.OnCollision;
    }

    // Constructor with child transform
    public CollisionBox(string tag, CollisionHandler parentCollisionHandler, GameObject parent, Vector2 offset, Vector2 size) //, Texture2D texture)
    {
      active = true;
      this.tag = tag;
      Transform parentTransform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      this.transform = new Transform(offset, size, parentTransform);
      this.parent = parent;
      AddToCollection(this);
      OnCollision += parentCollisionHandler.OnCollision;
      // Remove this once child transform is tested
      // this.renderer = new Renderer(texture, Color.BlueViolet, 10, new GameObject().AddComponent(Constants.Components.TRANSFORM, transform));
    }

    public CollisionBox setSize(Vector2 size)
    {
      this.transform.Size = size;
      return this;
    }

    public CollisionBox setOffset(Vector2 offset)
    {
      this.transform.Offset = offset;
      return this;
    }

    public bool IsActive() { return active; }
    public void SetActive(bool value) { active = value; }
    public string GetTag() { return tag; }
    public void SetTag(string newTag) { this.tag = newTag; }
    public Transform GetTransform() { return transform; }
    public GameObject Parent { get { return parent; }}

    public void CollisionDetected(CollisionBox collider)
    {
      if (OnCollision != null) OnCollision(this, collider);
    }
    ~CollisionBox() { CollisionBoxCollection[GetTag()].list.Remove(this); }
  }
}
