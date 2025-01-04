using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace EFCS
{
    public class Enemy : Character
    {
        /// <summary>
        /// Gère les évenements aléatoires des ennemis.
        /// </summary>
        private static Random rnd;
       /// <summary>
       /// Compte le nombre de mise a jour des ennemis.
       /// </summary>
        protected int nbUpdates;
        /// <summary>
        /// S'occupe de créer les basicsEnemys lorsqu'un ennemy meurt et les retires de la list et donc du jeu.
        /// </summary>
        /// <param name="gw">Accès au logique du jeu et ces fonctions.</param>
        public override void Explode(GW gw)
        {
            gw.AddBasicEnemy(new BasicEnemy(this.Position.X, this.Position.Y, this.Angle - 90));
            gw.AddBasicEnemy(new BasicEnemy(this.Position.X, this.Position.Y, this.Angle + 90));
            gw.AddBasicEnemy(new BasicEnemy(this.Position.X, this.Position.Y, this.Angle));
            gw.DeletingEnemy(this);
            

        }
        /// <summary>
        /// S'occupe de mettre a jour les ennemis.
        /// </summary>
        /// <param name="deltaT">Angle de l'ennemis.</param>
        /// <param name="gw">Accès au logique du jeu et ses fonctions.</param>
        /// <param name="heroPos">Position du héro pour mettre a jour les pattern ennemy.</param>
        /// <returns>true si le héro est en vie false sinon.</returns>
        public override bool Update(float deltaT,GW gw,Vector2f heroPos)
        {
            this.Angle = (float)Math.Atan2((heroPos.Y - this.Position.Y), (heroPos.X - this.Position.X)) * 180 / (float)Math.PI;
            Advance(Speed);
            return true;
        }
        protected Enemy(float posX,float posY,UInt32 nbVertices,float size,Color color,float speed)
            :base(posX,posY,nbVertices,color,speed)
        {
            for(UInt32 i = 0; i < nbVertices; i++)
            {
                float x = (float)(Math.Cos(2 * Math.PI * i / nbVertices)) * size;
                float y = (float)(Math.Sin(2 * Math.PI * i / nbVertices)) * size;
                this[i] = new Vector2f(x, y);
            }
            Type = CharacterType.Ennemy;    
        }
    }
}
