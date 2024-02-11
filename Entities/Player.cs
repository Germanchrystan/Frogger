using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Components.Collisions;
using Prefabs;

namespace Entities
{
  public class Player:  Actor, CollisionHandler 
  {
    Transform transform;
    CollisionBox collisionBox;
    Renderer renderer;
    Movement movement;
    int speed = 100;
    public Player(Texture2D texture, Vector2 position)
    {
      transform = new Transform(position, new Vector2(Constants.General.SIZE,Constants.General.SIZE));
      collisionBox = new CollisionBox(transform, "player", this);
      renderer = new Renderer(texture, transform, Color.Green, 9);
      movement = new Movement(speed, 0);
    }

    public Movement GetMovement() {return movement;}
    public Transform GetTransform() {return transform;}
    public void OnCollision(object source, CollisionData collisionData)
    {

    }
  }
}