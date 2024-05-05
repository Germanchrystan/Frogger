using Prototypes;

namespace Components
{
  public class Movement: Component
  {
    public int xDirection;
    public int yDirection;
    public int speed;
    public string Type() {return Constants.Components.MOVEMENT; } 

    public Movement(int speed, int xDirection, int yDirection)
    {
      this.speed = speed;
      this.xDirection = xDirection;
      this.yDirection = yDirection;
    }

    public void resetMovement()
    {
      speed = 0;
      xDirection = 0;
    }
  }
}