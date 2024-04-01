using System.Collections.Generic;
using Components.Collisions;
using Constants;
using Entities;

namespace Managers
{
  class CollisionManager
  {
    public struct ColliderRelation
    {
      public string mainColliderTag;
      public string relatedCollidersTag;
    }

    public static List<ColliderRelation> colliderRelations = new List<ColliderRelation>
    {
      new ColliderRelation()
      {
        mainColliderTag = Tags.PLAYER, relatedCollidersTag = Tags.CAR
      },
      new ColliderRelation()
      {
        mainColliderTag = Tags.PLAYER, relatedCollidersTag = Tags.WATER
      },
      new ColliderRelation()
      {
        mainColliderTag = Tags.PLAYER, relatedCollidersTag = Tags.LOG
      },
      new ColliderRelation()
      {
        mainColliderTag = Tags.PLAYER, relatedCollidersTag = Tags.LEAF
      }      
    };

    public static void Update()
    {
      for (int i = 0; i < colliderRelations.Count; i++)
      {
        if (CollisionBox.CollisionBoxCollection.ContainsKey(colliderRelations[i].mainColliderTag) && CollisionBox.CollisionBoxCollection.ContainsKey(colliderRelations[i].relatedCollidersTag))
        {
          CollisionList mainColliders = CollisionBox.CollisionBoxCollection[colliderRelations[i].mainColliderTag];
          CollisionList relatedColliders = CollisionBox.CollisionBoxCollection[colliderRelations[i].relatedCollidersTag];

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