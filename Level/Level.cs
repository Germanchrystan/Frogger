using System.Collections.Generic;
using System.Numerics;
using Entities;
using Microsoft.Xna.Framework.Graphics;
using Prototypes;

namespace Levels
{
  public class Level
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
    enum ACTOR
    {
      PLAYER = 4,
      WATER,
      TO_RIGHT_LOG,
      TO_LEFT_LOG,
      NORMAL_TO_RIGHT_CAR,
      NORMAL_TO_LEFT_CAR,
      TRUCK,
      LEAF,
    };

    public uint[,] BackgroundRep;
    public uint[,] ActorsRep;
    public GameObject[] ActorsPool;
    public Tile[] TilesPool;
    Texture2D texture;
    public Level(Texture2D texture, uint[,] BackgroundRep, uint[,] ActorsRep)
    {
      this.BackgroundRep = BackgroundRep;
      this.ActorsRep = ActorsRep;
      int TileLength = BackgroundRep.Length * BackgroundRep.GetLength(0);
      TilesPool = new Tile[TileLength];
      this.texture = texture;
    }
    public void Load()
    {
      List<Tile> tmpTileList = new List<Tile>();
      List<GameObject> tmpActorList = new List<GameObject>();

      for(int i = 0; i < 11; i++)
      {
        for(int j = 0; j < 11; j++)
        {
          Vector2 position = new Vector2(Constants.General.SIZE * j, Constants.General.SIZE * i);
          Tile newTile = intToTile(BackgroundRep[i, j], position);
          GameObject newActor = intToActor(ActorsRep[i, j], position);
          if (newTile != null) tmpTileList.Add(newTile);
          if(newActor != null) tmpActorList.Add(newActor);
        }
      }
      TilesPool = tmpTileList.ToArray();
      ActorsPool = tmpActorList.ToArray();
    }

    public Tile intToTile(uint rep, Vector2 position)
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

    public GameObject intToActor(uint rep, Vector2 position)
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
        default:
          return null;
      }
    }
  }
}