using Components;

namespace Prefabs
{
  public interface Actor
  {
    public Movement GetMovement();
    public Transform GetTransform();
  }
}