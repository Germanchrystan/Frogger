using System;
using System.Collections.Generic;
using Components.Collisions;

namespace Managers
{
    public struct ColliderRelation
    {
      public string mainColliderTag;
      public string relatedCollidersTag;
    }
  class CollisionManager
  {
    private Dictionary<string, CollisionList> collisionBoxCollection = new Dictionary<string, CollisionList>();
    public void Add(CollisionBox collisionBox)
    {
      if(!collisionBoxCollection.ContainsKey(collisionBox.GetTag()))
      {
        collisionBoxCollection[collisionBox.GetTag()] = new CollisionList();
      }
        collisionBoxCollection[collisionBox.GetTag()].ActiveNum += 1;
        collisionBoxCollection[collisionBox.GetTag()].list.Add(collisionBox);

    }
    public void Deactivate(CollisionBox collisionBox)
    {
      CollisionList collisionListItem = collisionBoxCollection[collisionBox.GetTag()];
      int index = collisionListItem.list.IndexOf(collisionBox);
      if(index == -1) throw new Exception("Collision box was not present in collection");
      
      // TODO: Check this
      CollisionBox temp = collisionListItem.list[collisionListItem.list.Count - 1];
      collisionListItem.list[collisionListItem.list.Count - 1] = collisionBox;
      collisionListItem.list [index] = temp;
      
      collisionListItem.ActiveNum--;
    }

    public void Activate(CollisionBox collisionBox)
    {
      CollisionList collisionListItem = collisionBoxCollection[collisionBox.GetTag()];
      int index = collisionListItem.list.IndexOf(collisionBox);
      if(index == -1) throw new Exception("Collision box was not present in collection");

      // TODO: Check this
      CollisionBox temp = collisionListItem.list[collisionListItem.ActiveNum];
      collisionListItem.list[collisionListItem.ActiveNum] = collisionBox;
      collisionListItem.list[index] = temp;

      collisionListItem.ActiveNum++;
    }
  
    private List<ColliderRelation> colliderRelations;

    public CollisionManager()
    {
      colliderRelations = new List<ColliderRelation>();
    }

    public CollisionManager(List<ColliderRelation> colliderRelations)
    {
      this.colliderRelations = colliderRelations;
    }

    public void Update()
    {
      for (int i = 0; i < colliderRelations.Count; i++)
      {
        if (collisionBoxCollection.ContainsKey(colliderRelations[i].mainColliderTag) && collisionBoxCollection.ContainsKey(colliderRelations[i].relatedCollidersTag))
        {
          CollisionList mainColliders = collisionBoxCollection[colliderRelations[i].mainColliderTag];
          CollisionList relatedColliders = collisionBoxCollection[colliderRelations[i].relatedCollidersTag];

          if (mainColliders.ActiveNum > 0 && relatedColliders.ActiveNum > 0)
          {
            for(int j = 0; j < mainColliders.ActiveNum; j++)
            {
              for(int k = 0; k < relatedColliders.ActiveNum; k++)
              {
                if (areColliding(mainColliders.list[j], relatedColliders.list[k]))
                {
                  mainColliders.list[j].CollisionDetected(relatedColliders.list[k]);
                  relatedColliders.list[k].CollisionDetected(mainColliders.list[j]);
                }
              }
            }
          }
        }
      }
    }

    public static bool areColliding(CollisionBox collisionBox1, CollisionBox collisionBox2)
    {
      return collisionBox1.GetTransform().Position.X < collisionBox2.GetTransform().Position.X + collisionBox2.GetTransform().Size.X &&
          collisionBox1.GetTransform().Position.X + collisionBox1.GetTransform().Size.X > collisionBox2.GetTransform().Position.X &&
          collisionBox1.GetTransform().Position.Y < collisionBox2.GetTransform().Position.Y + collisionBox2.GetTransform().Size.Y &&
          collisionBox1.GetTransform().Position.Y + collisionBox1.GetTransform().Size.Y > collisionBox2.GetTransform().Position.Y;
    }
  }
}