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
    public class Character : Movable
    {
        /// <summary>
        /// S'occupe du type des projectile ennemis ou du héros
        /// </summary>
        private CharacterType type;
        public CharacterType Type
        {
            get {return this.type ; }
            set {type = value; }
        }
       /// <summary>
       /// Vérification de si un objet contient (physiquement) un autre object.
       /// </summary>
       /// <param name="m">Object auquel on vérifie si sa se contient.</param>
       /// <returns>SI l'object est a l'intérieur(true) ou non(false).</returns>
        public bool Contains(Movable m)
        {
            return true;
        }
        /// <summary>
        /// S'occupe des tir enemis et du héro.
        /// </summary>
        /// <param name="gw">Accès au logique du jeu et de ces fonctions.</param>
        /// <param name="deltaT">Angle du tireur.</param>
        public virtual void Fire(GW gw,float deltaT)
        {
            var newProjectileAngle = this.Angle;
            var angleInRadians = (float)(Math.PI * newProjectileAngle / 180.0f);
            var projectileXPosition = this.Position.X +
                                      (float)Math.Cos(angleInRadians) / 2.0f;
            var projectileYPosition = this.Position.Y +
                                      (float)Math.Sin(angleInRadians) / 2.0f;
            Projectile p = new Projectile(Type, projectileXPosition, projectileYPosition, 4, Color.White);
            p.Angle = this.Angle;
            gw.AddProjectile(p);

        }
        /// <summary>
        /// Fonction S'occupant de faire avancer l'objet qui l'appelle.
        /// </summary>
        /// <param name="nbPixels">Nombre de pixel a avancer par appelle.</param>
        protected override void Advance(float nbPixels)
        {
            base.Advance(nbPixels);
            if (Position.X > GW.WIDTH || Position.Y > GW.HEIGHT || Position.X < 0 || Position.Y < 0)
            {
                Vector2f posMove = new Vector2f(0,0);
                if (Position.X > GW.WIDTH)
                {
                    posMove.X = Position.X - GW.WIDTH;
                }
                if (Position.Y > GW.HEIGHT)
                {
                    posMove.Y = Position.Y - GW.HEIGHT;
                }
                Position = Position - posMove;
                if (Position.X < 0)
                {
                    Position = new Vector2f(0, Position.Y);
                }
                if (Position.Y < 0)
                {
                    Position = new Vector2f(Position.X,0);
                }
            }

        }
        /// <summary>
        /// Constructeur du character plus utiliser comme transition vers les constructeur supérieur movable et drawable.
        /// </summary>
        /// <param name="posX">Position horizontal du personnage a créer.</param>
        /// <param name="posY">Position vertical du personnage a créer.</param>
        /// <param name="nbVertice">Nombre de point qui forme la shape.</param>
        /// <param name="color">Couleur pour représenter le personnage créer.</param>
        /// <param name="speed">Vitesse de l'objet a créer.</param>
        protected Character(float posX,float posY,UInt32 nbVertice,Color color,float speed)
            :base(posX,posY,nbVertice,color,speed)
        {
 
        }
        /// <summary>
        /// Méthode Update qui fait le lien entre les updates plus haut et celle-ci
        /// </summary>
        /// <param name="deltaT">Angle du personnage.</param>
        /// <param name="gw">Accès au logique du jeu et des fonctions</param>
        /// <param name="heroPos">Position du héro</param>
        /// <returns></returns>
        public virtual bool Update(float deltaT,GW gw,Vector2f heroPos)
        {
            return true;
        }
        /// <summary>
        /// Méthode présente pour faciliter l'appelle des méthodes pour détruire tout les ennemis pour une bombe.
        /// </summary>
        /// <param name="gw"></param>
        public virtual void Explode(GW gw)
        {
            ;
        }
    }
}
