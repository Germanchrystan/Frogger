using System.Collections.Generic;
using System.Numerics;
using Entities;
using Microsoft.Xna.Framework.Graphics;
using Prefabs;

namespace Levels
{
  public class Level
  {
    enum TILE
    {
      WALKWAY = 1,
      STREET,
      GRASS,
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
        case TILE.WALKWAY:
          return Tiles.WalkwayTile(texture, position);
        case TILE.STREET:
          return Tiles.StreetTile(texture, position);
        case TILE.GRASS:
          return Tiles.GrassTile(texture, position);
        default:
          return null;
      }      
    }

    public GameObject intToActor(uint rep, Vector2 position)
    {
      switch((ACTOR)rep)
      {
        case ACTOR.PLAYER:
          return new Player(texture, position);
        case ACTOR.TO_RIGHT_LOG:
          return MovingObjects.NormalLog(texture, position, 1);
        case ACTOR.TO_LEFT_LOG:
          return MovingObjects.NormalLog(texture, position, -1);
        case ACTOR.NORMAL_TO_RIGHT_CAR:
          return MovingObjects.NormalCar(texture, position, 1);
        case ACTOR.NORMAL_TO_LEFT_CAR:
          return MovingObjects.NormalCar(texture, position, -1);
        default:
          return null;
      }
    }
  }
}