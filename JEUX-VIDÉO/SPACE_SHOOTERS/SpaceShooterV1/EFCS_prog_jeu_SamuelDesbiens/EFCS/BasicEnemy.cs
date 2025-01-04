using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;

namespace EFCS
{
    public class BasicEnemy : Enemy
    {
        /// <summary>
        /// Attribut de la class BasicEnemy.
        /// </summary>
        private const int BasicEnemySpeed = 2;
        private int timeBeforeRealUpdate;
        private int nbUpdate = 1;
        /// <summary>
        /// Constructeur de la class BasicEnemy.
        /// </summary>
        /// <param name="posX">Position horizontal ou le basic ennemis vas apparaitre.</param>
        /// <param name="posY">Position horizontal ou le basic ennemis vas apparaitre.</param>
        /// <param name="Angle">Angle d'apparition du BasicEnemy.</param>
        public BasicEnemy(float posX,float posY,float Angle)
            :base(posX,posY,3,10,Color.Red,BasicEnemySpeed)
        {
            this.Angle = Angle;
            this.timeBeforeRealUpdate = 0;
                

        }
        /// <summary>
        /// Méthode qui s'occupe de détruire l'énnemis
        /// </summary>
        /// <param name="gw"></param>
        public override void Explode(GW gw)
        {
            gw.DeletingBasicEnemy(this);
        }
        /// <summary>
        /// Mise a jour des ennemis de base du jeu avec un override pour faire une petite différence avec la classe énnemis.
        /// </summary>
        /// <param name="deltaT">Angle du BasicEnemy.</param>
        /// <param name="gw">Accès a la logique du jeu et ses fonction.</param>
        /// <param name="heroPos">Position du héro.</param>
        /// <returns></returns>
        public override bool Update(float deltaT, GW gw, Vector2f heroPos)
        {
            if(this.timeBeforeRealUpdate < 90)
            {
                Advance(BasicEnemySpeed);
                this.timeBeforeRealUpdate++;
                return true;
            }
            else
            {
                if(nbUpdate % 91 == 0)
                {
                    Fire(gw, this.Angle);
                    nbUpdate = 1;
                }
                nbUpdate++;
                return base.Update(deltaT, gw, heroPos);
            }
           
        }

    }
}
