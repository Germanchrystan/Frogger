using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Prototypes;

namespace Components.Collisions
{
  public class CollisionData: Iterable
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

  public class CollisionBox : Iterable, Component
  {
    
    private string tag;
    private Transform transform;
    private GameObject parent;
    public string Type() { return Constants.Components.COLLISION_BOX; } 
    public Transform Transform { get { return transform;}}
    public string GetTag() { return tag; }
    public void SetTag(string newTag) { this.tag = newTag; }
    public Transform GetTransform() { return transform; }
    public GameObject Parent { get { return parent; }}

    public event EventHandler<CollisionBox> OnCollision;

    // Constructor without child transform
    public CollisionBox(string tag, CollisionHandler parentCollisionHandler, GameObject parent)
    {
      Active = true;
      this.tag = tag;
      this.parent = parent;
      this.transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      OnCollision += parentCollisionHandler.OnCollision;
    }

    // Constructor with child transform
    public CollisionBox(string tag, CollisionHandler parentCollisionHandler, GameObject parent, Vector2 offset, Vector2 size) //, Texture2D texture)
    {
      Active = true;
      this.tag = tag;
      Transform parentTransform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      this.transform = new Transform(offset, size, parentTransform);
      this.parent = parent;
      OnCollision += parentCollisionHandler.OnCollision;
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

    public void CollisionDetected(CollisionBox collider)
    {
      if (OnCollision != null) OnCollision(this, collider);
    }
  }
}
