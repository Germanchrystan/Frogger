using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Prototypes;

namespace Components;

public class Transform: Component
{
    private Vector2 position;
    private Vector2 centerPosition;
    private Vector2 size;
    private Rectangle rect;
    private List<Transform> childTransforms = new List<Transform>();
    private Vector2 offset;
    public bool Active { get { return true; } }
    public string Type() { return Constants.Components.TRANSFORM; }
    // Constructor for parent transform
    public Transform(Vector2 position, Vector2 size)
    {
        this.position = position;
        this.size = size;
        this.offset = new Vector2(0,0);
        this.rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        this.centerPosition = recalculateCenterPosition();
    }
    // Constructor for child transform
    public Transform(Vector2 offset, Vector2 size, Transform parentTransform)
    {
        parentTransform.AddChildTransform(this);
        this.offset = offset;
        this.position = new Vector2(parentTransform.Position.X + offset.X, parentTransform.position.Y + offset.Y);
        this.size = size;
        this.rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        this.centerPosition = recalculateCenterPosition();
    }
    public Vector2 Position { get { return position; } 
        set 
        {
            centerPosition = recalculateCenterPosition();
            position = new Vector2(value.X + offset.X, value.Y + offset.Y);
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            foreach(Transform child in childTransforms)
            {
                child.Position = position;
            }
        }
    }
    public Vector2 Size { get { return size; } set { size = value; } }
    public Vector2 Offset { set { offset = value; }}
    public Vector2 CenterPosition { get { return centerPosition; } }

    public Rectangle Rect { get { return rect; } }
    private Vector2 recalculateCenterPosition()
    {
        return new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);
    }
    public void AddChildTransform(Transform childTransform)
    {
        childTransforms.Add(childTransform);
    }
}