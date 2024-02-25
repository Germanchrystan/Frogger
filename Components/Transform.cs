using Microsoft.Xna.Framework;
using Prototypes;

namespace Components;

public class Transform: Component
{
    private Vector2 position;
    private Vector2 centerPosition;
    private Vector2 size;
    private Rectangle rect;
    public Transform(Vector2 position, Vector2 size)
    {
        this.position = position;
        this.size = size;
        this.rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        this.centerPosition = recalculateCenterPosition();
    }
    public Vector2 Position { get { return position; } 
        set 
        {
            centerPosition = recalculateCenterPosition();
            position = value;
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }
    }
    public Vector2 Size { get { return size; } set { size = value; } }
    public Vector2 CenterPosition { get { return centerPosition; } }

    public Rectangle Rect { get { return rect; } }
    private Vector2 recalculateCenterPosition()
    {
        return new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);
    }
}