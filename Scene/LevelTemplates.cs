using System.Collections.Generic;
using Constants;
using Managers;

namespace Scenes
{
  public class LevelTemplates
  {
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
        mainColliderTag = Tags.PLAYER, relatedCollidersTag = Tags.TURTLE
      },
      new ColliderRelation()
      {
        mainColliderTag = Tags.PLAYER, relatedCollidersTag = Tags.LEAF
      }      
    };
    public static uint[,] LVL1_B = new uint[11, 11]{
      {8,9,8,10,8,11,8,12,8,9,8},
      {9,10,9,10,9,10,9,10,9,10,9},
      {11,12,11,12,11,12,11,12,11,12,11},
      {9,10,9,10,9,10,9,10,9,10,9},
      {11,12,11,12,11,12,11,12,11,12,11},
      {1,2,1,2,1,2,1,2,1,2,1},
      {5,3,4,5,3,4,5,3,4,5,3},
      {4,5,3,4,5,3,4,5,3,4,5},
      {5,3,4,5,3,4,5,3,4,5,3},
      {7,7,7,7,7,7,7,7,7,7,7},
      {1,2,1,2,1,2,1,2,1,2,1},
    };
    public static uint[,] LVL1_A = new uint[11, 11]{
      {0,11,0,11,0,11,0,11,0,11,0},
      {5,5,0,5,5,0,5,5,0,5,0},
      {0,7,7,7,0,7,7,7,0,7,7},
      {0,6,6,0,6,0,6,6,0,6,0},
      {0,7,7,7,0,7,7,7,0,7,7},
      {0,0,0,0,0,0,0,0,0,0,0},
      {0,0,0,0,0,0,0,0,0,0,0},
      {0,0,0,0,0,0,0,0,0,0,0},
      {0,0,0,9,0,0,9,0,0,0,0},
      {0,8,0,0,8,0,0,8,0,0,8},
      {0,0,0,0,0,4,0,0,0,0,0},
    };
  }
}