using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Audio;
using SFML.Graphics;
namespace EFCS
{
    class Circle : Enemy
    {
        /// <summary>
        /// Attribut de la classe circle.
        /// </summary>
        private const int CircleSpeed = 2;
        private const UInt32 NBSides = 50 ;
        private const int Radius = 10;
        private int nbUpdate = 1;
        private Vector2f nextPos;
        /// <summary>
        /// Constructeur de la classe circle qui se construit plus haut dans le diagram de classe.
        /// </summary>
        /// <param name="posX">Position horizontal ou le circle doit apparaitre.</param>
        /// <param name="posY">Position vertical ou le circle doit apparaitre.</param>
        public Circle(float posX,float posY)
            :base(posX,posY,NBSides,Radius,Color.Blue,CircleSpeed)
        {

        }
        /// <summary>
        /// Update override pour le comportement particulier de l'ennemis comparer a la classe ennemy.
        /// </summary>
        /// <param name="deltaT">Angle du circle.</param>
        /// <param name="gw">Accès au logique du jeu et ses fonctions</param>
        /// <param name="heroPos">Position du héro.</param>
        /// <returns>true si le héro est en vie false sinon.</returns>
        public override bool Update(float deltaT, GW gw, Vector2f heroPos)
        {
           
            if (nbUpdate % 120 == 0)
            {
                this.Angle = (float)Math.Atan2((heroPos.Y - this.Position.Y), (heroPos.X - this.Position.X)) * 180 / (float)Math.PI;
                Fire(gw, this.Angle);
                nbUpdate = 1;
            }
            else if(nbUpdate == 1)
            {
                nextPos = new Vector2f(gw.r.Next(0, 1024), gw.r.Next(0, 768));
                this.Angle = (float)Math.Atan2((nextPos.Y - this.Position.Y), (nextPos.X - this.Position.X)) * 180 / (float)Math.PI;
                nbUpdate++;

            }
            else
            {
                Advance(CircleSpeed);
                nbUpdate++;
            }
            return true;
        }
    }
}
