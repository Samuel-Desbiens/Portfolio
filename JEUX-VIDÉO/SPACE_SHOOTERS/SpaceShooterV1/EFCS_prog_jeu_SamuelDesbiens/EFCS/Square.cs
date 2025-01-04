using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace EFCS
{
    class Square : Enemy
    {
        /// <summary>
        /// Attribut de la classe square.
        /// </summary>
        private const int SquareSize = 10;
        private const int SquareSpeed = 25;
        private int nbUpdate = 0;
        private int moveTimeLapse = 0;
        /// <summary>
        /// Constructeur de la classe Square qui est créer plus haut dans le diagram de classe.
        /// </summary>
        /// <param name="posX">Position horizontal ou le Square vas apparaitre.</param>
        /// <param name="posY">Position vertical ou le Square vas apparaitre.</param>
        public Square(float posX, float posY)
            : base(posX, posY, 4, SquareSize, Color.Yellow, SquareSpeed)
        {

        }
        /// <summary>
        /// Mise en jour en override pour faire le comportement particulier du carré
        /// </summary>
        /// <param name="deltaT">Angle du carré.</param>
        /// <param name="gw">Accès a la logique du jeu et ses fonction</param>
        /// <param name="heroPos">Position du héro.</param>
        /// <returns>true si le héro est en vie false sinon.</returns>
        public override bool Update(float deltaT, GW gw, Vector2f heroPos)
        {
            if (nbUpdate == int.MaxValue)
            {
                nbUpdate = 0;
            }
            else
            {
                nbUpdate++;
            }
            this.Angle = (float)Math.Atan2((heroPos.Y - this.Position.Y), (heroPos.X - this.Position.X)) * 180 / (float)Math.PI;
            if (moveTimeLapse > 0)
            {
                moveTimeLapse--;
                return base.Update(deltaT, gw, heroPos);

            }
            if (nbUpdate % 300 == 0)
            {
                moveTimeLapse += 5;
                Fire(gw, Angle);
                return true;

            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Overide de la fonction fire pour accomoder la manière particulière du carré de tirer
        /// </summary>
        /// <param name="gw">Accès a la logique du jeu et ses fonctions</param>
        /// <param name="angle">Angle du carré.</param>
        public override void Fire(GW gw, float angle)
        {
            var newProjectileAngle = this.Angle;
            var angleInRadians = (float)(Math.PI * newProjectileAngle / 180.0f);
            var projectileXPosition = this.Position.X +
                                      (float)Math.Cos(angleInRadians) / 2.0f;
            var projectileYPosition = this.Position.Y +
                                      (float)Math.Sin(angleInRadians) / 2.0f;
            Projectile c = new Projectile(Type, projectileXPosition, projectileYPosition, 4, Color.White);
            Projectile l = new Projectile(Type, projectileXPosition, projectileYPosition, 4, Color.White);
            Projectile r = new Projectile(Type, projectileXPosition, projectileYPosition, 4, Color.White);
            c.Angle = this.Angle + 180 ;
            l.Angle = this.Angle +180 - 10;
            r.Angle = this.Angle +180 + 10;
            gw.AddProjectile(c);
            gw.AddProjectile(l);
            gw.AddProjectile(r);
        }




    }
}
