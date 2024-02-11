using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Components;
using Components.Collisions;
using Prefabs;
using System.Collections.Generic;
using System.Diagnostics;
using System;


namespace Entities
{
  public class MovingObject : Actor, CollisionHandler
  {
    Transform transform;
    CollisionBox collisionBox;
    Renderer renderer;
    Movement movement;
    UpdateableComponent updater;

    public MovingObject(Texture2D texture, Vector2 position, Color color, string tag, int xDirection, int layer)
    {
      this.transform = new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE));
      this.collisionBox = new CollisionBox(transform, tag, this);
      this.movement = new Movement(50, xDirection);
      this.renderer = new Renderer(texture, transform, color, 5);
      this.updater = new MovingObjectUpdater(this);
    }

    public Movement GetMovement(){ return movement;}
    public Transform GetTransform(){return transform;}

    public void OnCollision(object source, CollisionData collisionData)
    {
      
    }
  }

  public class MovingObjects
  {
    public static Actor NormalLog(Texture2D texture, Vector2 position, int direction)
    {
      return new MovingObject(texture, position, Color.Brown, "log", direction, 5);
    }
    public static Actor NormalCar(Texture2D texture, Vector2 position, int direction)
    {
      return new MovingObject(texture, position, Color.Red, "car", direction, 9);
    }
  }
}
