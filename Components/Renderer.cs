using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prototypes;
using Utils;
using Components.GraphicComponents;

namespace Components
{
  public interface Layered
  {
    public int Layer{ get { return Layer;}}
  }
  public interface Renderable
  {
    public int Layer{ get { return Layer;}}
    public void Render(SpriteBatch spriteBatch);
  }
  public class Renderer: Layered
  {
    List<Renderable> RendererList = new List<Renderable>();
    public void AddToList(Renderable renderable)
    {
      RendererList.Add(renderable);
      QuickSort.Sort(RendererList, 0, RendererList.Count - 1);
    }
    public void Render(SpriteBatch spriteBatch)
    {
      for(int i = 0; i < RendererList.Count; i++)
      {
        RendererList[i].Render(spriteBatch);
      }
    }
    public void Clear()
    {
      RendererList.Clear();
    }
  }
  // Simple Texture Renderer
  public class SimpleTextureRenderer: Component, Renderable
  {
    Color color;
    Transform transform;
    Texture2D texture;
    int layer;
    public int Layer{ get { return layer;}}
    private bool active;
    public bool Active { get { return active; } }
    public string Type() { return Constants.Components.RENDERER; } 
    public SimpleTextureRenderer(Texture2D texture, Color color, int layer, GameObject parent)
    {
      this.color = color;
      this.transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      this.texture = texture;
      this.layer = layer;
      this.active = true;
    }
    public void setColor (Color newColor)
    {
      color = newColor;
    }
    public void Render(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, transform.Rect, color);
    }
  }
  // Single Frame Renderer
  public class SingleFrameRenderer: Component, Renderable
  {
    FrameData frameData;
    Color color;
    int layer;
    public int Layer{ get { return layer;}}
    private bool active;
    public bool Active { get { return active; } }
    Transform transform;
    public string Type() { return Constants.Components.RENDERER; } 
    public SingleFrameRenderer(FrameData frameData, Color color, int layer, GameObject parent)
    {
      this.frameData = frameData;
      this.color = color;
      this.layer = layer;
      this.transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      this.active = true;
    }
    public void SetNewFrameData(FrameData frameData)
    {
      this.frameData = frameData;
    }
    public void Render(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(frameData.texture, transform.Rect, frameData.rect, color);
    }
  }
  // Animator Renderer
  public class AnimatorRenderer: Component, Renderable
  {
    Color color;
    Transform transform;
    Animator animator;
    int layer;
    public int Layer{ get { return layer;}}
    public string Type () { return Constants.Components.RENDERER; } 
    private bool active;
    public bool Active { get { return active; } }
    public AnimatorRenderer(int layer, GameObject parent)
    {
      this.transform = parent.GetComponent<Transform>(Constants.Components.TRANSFORM);
      this.animator = parent.GetComponent<Animator>(Constants.Components.ANIMATOR);
      this.layer = layer;
      this.color = Color.White;
      this.active = true;
    }
    public void Render(SpriteBatch spriteBatch)
    {
      FrameData frameData = animator.GetCurrentFrameData();
      spriteBatch.Draw(frameData.texture, transform.Rect, frameData.rect, color);
    }
  }
}