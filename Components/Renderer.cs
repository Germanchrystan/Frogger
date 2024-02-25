using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prototypes;
using Utils;

namespace Components
{
  public interface Layered
  {
    public int Layer{ get { return Layer;}}
  }
  public class Renderer: Component, Layered
  {
    public static List<Renderer> RendererList = new List<Renderer>();
    public static void AddToList(Renderer renderer)
    {
      RendererList.Add(renderer);
      QuickSort.Sort(RendererList, 0, RendererList.Count - 1);
    }
    public static void Render(SpriteBatch spriteBatch)
    {
      for(int i = 0; i < RendererList.Count; i++)
      {
        RendererList[i].Draw(spriteBatch);
      }
    }
    Color color;
    Transform transform;
    Texture2D texture;
    int layer;
    public Renderer(Texture2D texture, Color color, int layer, GameObject parent)
    {
      this.color = color;
      this.transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      this.texture = texture;
      this.layer = layer;
      AddToList(this);
    }
    public void setColor (Color newColor)
    {
      color = newColor;
    }
    public int Layer { get { return layer; }}
    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, transform.Rect, color);
    }
  }
}