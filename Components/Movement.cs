namespace Components
{
  public class Movement
  {
    public int xDirection;
    public int yDirection;
    public int speed;
    // public Movement(int speed)
    // {
    //   this.speed = speed;
    //   this.xDirection = 0;
    //   this.yDirection = 0;
    // }
    public Movement(int speed, int xDirection)
    {
      this.speed = speed;
      this.xDirection = xDirection;
    }
  }
}