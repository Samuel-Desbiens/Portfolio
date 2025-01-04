using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace EFCS
{
  public class Movable : Drawable
  {
    public override float Angle
    {
      get
      {
        return base.Angle;
      }

      set
      {
        base.Angle = value;
        Direction = new Vector2f((float)Math.Cos(Math.PI * Angle / 180.0f), (float)Math.Sin(Math.PI * Angle / 180.0f));
      }
    }
    public float Size { get { return Math.Max(BoundingBox.Height, BoundingBox.Width); } }


    public Vector2f Direction { get; set; }
    public virtual bool IsAlive { get; protected set; }

    public float Speed { get; set; }
    public Movable(float posX, float posY, uint nbVertices, Color color, float speed)
      : base(posX, posY, nbVertices, color)
    {
      Angle = 0;
      IsAlive = true;
      Speed = speed;
    }
          
    protected void Rotate(float angleInDegrees)
    {
      Angle = Angle + angleInDegrees;
    }
    protected virtual void Advance(float nbPixels)
    {
      Position = Position + Direction * nbPixels;
    }
  }
}
