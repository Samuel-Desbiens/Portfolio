using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace EFCS
{
    public class Star : Movable
    {
        /// <summary>
        /// Attribut de la classe étoile.
        /// </summary>
        private const int starSize = 1;
        private static Color starColor;
        private  static int starSpeed;
        private int StarSpeed
        {
            get;
            set;
        }

        private static Random r = new Random();
        private static UInt32 nbVertices = 32;
        /// <summary>
        /// Constructeur de la classe étoile
        /// </summary>
        /// <param name="posX">Position horizontal ou doit apparaitre l'étoile.</param>
        /// <param name="posY">Position vertical ou doit apparaitre l'étoile.</param>
        public Star(float posX,float posY)
            :base(posX,posY,nbVertices,starColor,starSpeed)
        {
            int random = r.Next(7);
            if(random == 0)
            {
                starColor = Color.Blue;
            }
            else if(random == 1)
            {
                starColor = Color.Cyan;
            }
            else if(random == 2)
            {
                starColor = Color.Green;
            }
            else if(random == 3)
            {
                starColor = Color.Magenta;
            }
            else if(random == 4)
            {
                starColor = Color.Red;
            }
            else if(random == 5)
            {
                starColor = Color.White;
            }
            else
            {
                starColor = Color.Yellow;
            }
            random = r.Next(1,3);
            starSpeed = random;
            StarSpeed = starSpeed;
            for(UInt32 i = 0; i < nbVertices; i++)
            {
                float x = (float)(Math.Cos(2 * Math.PI * i / nbVertices)) * starSize;
                float y = (float)(Math.Sin(2 * Math.PI * i / nbVertices)) * starSize;
                this[i] = new Vector2f(x, y);
            }
        }
        /// <summary>
        /// Mets a jour les étoiles dépendant de la direction du héro.
        /// </summary>
        /// <param name="heroAngle">Angle du héro pour diriger les étoiles.</param>
        /// <param name="gw">Accès au logique du jeu et ses fonctions.</param>
        /// <returns>true si le héro est en vie false sinon.</returns>
        public bool Update(float heroAngle,GW gw)
        {
            if (gw.Contains(this))
            {
                ;
            }
            else
            {
                if(this.Position.X < 0)
                {
                    float difference = Position.X;
                    this.Position = new Vector2f(-difference + 1024,this.Position.Y);
                }
                if (this.Position.Y < 0)
                {
                    float difference = Position.Y;
                    this.Position = new Vector2f(this.Position.X, -difference + 768 );
                }
                if (this.Position.X > 1024)
                {
                    float difference = Position.X;
                    this.Position = new Vector2f(this.Position.X -difference, this.Position.Y);
                }
                if (this.Position.Y > 768)
                {
                    float difference = Position.Y;
                    this.Position = new Vector2f(this.Position.X, this.Position.Y - difference);
                }

            }
            this.Angle = heroAngle - 180;
            Advance(this.StarSpeed);
            return true;
        }
      

    }
}
