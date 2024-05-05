using System;
using System.Collections.Generic;

using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prototypes;

namespace Scenes
{
  public class TileLoader
  {
    enum TILE
    {
      WALKWAY1 = 1,
      WALKWAY2,
      STREET1,
      STREET2,
      STREET3,
      STREET4,
      STREET5,
      GRASS,
      WATER1,
      WATER2,
      WATER3,
      WATER4,
    };
    public static Tile IntToTile(uint rep, Vector2 position)
    {
      switch((TILE)rep)
      {
        case TILE.WALKWAY1:
          return Tiles.WalkwayTile(0, position);
        case TILE.WALKWAY2:
          return Tiles.WalkwayTile(1, position);
        case TILE.STREET1:
          return Tiles.StreetTile(0, position);
        case TILE.STREET2:
          return Tiles.StreetTile(1, position);
        case TILE.STREET3:
          return Tiles.StreetTile(2, position);
        case TILE.STREET4:
          return Tiles.StreetTile(3, position);
        case TILE.STREET5:
          return Tiles.StreetTile(4, position);
        case TILE.GRASS:
          return Tiles.GrassTile(position);
        case TILE.WATER1:
          return Tiles.Water(3, position);
        case TILE.WATER2:
          return Tiles.Water(4, position);
        case TILE.WATER3:
          return Tiles.Water(5, position);
        case TILE.WATER4:
          return Tiles.Water(6, position);
        default:
          return null;
      }      
    }
  }

  public class ActorLoader
  {
    enum ACTOR
    {
      PLAYER = 4,
      TURTLE_TO_LEFT,
      TO_RIGHT_LOG,
      TO_LEFT_LOG,
      NORMAL_TO_RIGHT_CAR,
      NORMAL_TO_LEFT_CAR,
      TRUCK,
      LEAF,
    };
    public static GameObject IntToActor(uint rep, Vector2 position)
    {
      switch((ACTOR)rep)
      {
        case ACTOR.PLAYER:
          return new Player(position);
        case ACTOR.TO_RIGHT_LOG:
          return MovingObjects.NormalLog(position, 1);
        case ACTOR.TO_LEFT_LOG:
          return MovingObjects.NormalLog(position, -1);
        case ACTOR.NORMAL_TO_RIGHT_CAR:
          return MovingObjects.NormalCar(position, 1);
        case ACTOR.NORMAL_TO_LEFT_CAR:
          return MovingObjects.NormalCar(position, -1);
        case ACTOR.LEAF:
          return new Leaf(position);
        case ACTOR.TURTLE_TO_LEFT:
          return new Turtle(position, -1);
        default:
          return null;
      }
    }
  }
}