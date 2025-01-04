using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace EFCS
{
  public abstract class Drawable
  {
    ConvexShape shape = null;
	protected Shape DrawableShape
    {
      get { return shape; }
    } 
    public Vector2f Position { get; set;}
    public Color Color { get { return shape.FillColor; } set { shape.FillColor = value; } }
    public virtual float Angle
    {
      get { return shape.Rotation; }
      set { shape.Rotation =value ; }
    }

    protected Drawable(float posX, float posY, uint nbVertices, Color color)
    {
      Position = new Vector2f(posX, posY);
      shape = new ConvexShape(nbVertices);
      shape.FillColor = color;
      Angle = 0;
    }
    
    public Vector2f this[uint index]
    {
      get { return DrawableShape.GetPoint(index); }
      set { shape.SetPoint(index, value); }
    }

    public virtual void Draw(RenderWindow window)
    {
      DrawableShape.Position = Position;      
      window.Draw(DrawableShape);
    }

    /// <summary>
    /// Retourne la boîte englobante associée à la forme.  Utilisée pour les collisions.
    /// </summary>
    public FloatRect BoundingBox
    {
      get { return DrawableShape.GetGlobalBounds(); }
    }

    /// <summary>
    /// Vérifie si deux éléments affichés s'entrecoupent
    /// </summary>
    /// <param name="m">Le second élément avec lequel vérifier la collision</param>
    /// <returns>true s'il y a collision, false sinon.</returns>
    public bool Intersects(Drawable m)
    {
      FloatRect r = m.BoundingBox;
      r.Left = m.Position.X;
      r.Top = m.Position.Y;
      return BoundingBox.Intersects(r);
    }

    /// <summary>
    /// Vérifie si l'objet contient le point où se trouve l'élément reçu en paramètre
    /// </summary>
    /// <param name="m">L'élément dont il faut vérifier la position</param>
    /// <returns>true si la boîte englobante de l'objet courant contient le point (la position) où se
    /// trouve l'objet reçu en paramètre</returns>
    public bool Contains(Drawable m)
    {
      return BoundingBox.Contains(m.Position.X, m.Position.Y);
    }
  }
}
