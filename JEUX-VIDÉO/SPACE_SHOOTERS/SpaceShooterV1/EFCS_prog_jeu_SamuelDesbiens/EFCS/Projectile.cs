using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace EFCS
{
    public class Projectile : Movable
    {
        /// <summary>
        /// Attribut des projectiles.
        /// </summary>
        private CharacterType type;
        private Vector2f pos;
        private UInt32 nbVertices;
        private Color projectileColor;

        public CharacterType Type
        {
            get { return this.type; }
            set {type = value;}
        }
        private const int ProjectileSpeed = 4;
        /// <summary>
        /// Constructeur de la classe Projectile
        /// </summary>
        /// <param name="type">Entre héro et ennemis pour empecher le "friendly fire</param>
        /// <param name="posX">Position horizontal ou le projectile vas apparaitre.</param>
        /// <param name="posY">Position vertical ou le projectile vas apparaitre.</param>
        /// <param name="nbVertices">Nombre de point format la figure géometrique</param>
        /// <param name="color">Couleur du projectile.</param>
        public Projectile(CharacterType type,float posX,float posY,UInt32 nbVertices,Color color)
            :base(posX,posY,nbVertices,color,ProjectileSpeed)
        {
            this.Type = type;
            pos.X = posX;
            pos.Y = posY;
            this.nbVertices = nbVertices;
            projectileColor = color;
            //Point formant la forme géométrique.
            this[0] = new Vector2f(0,0);
            this[1] = new Vector2f(10,0);
            this[2] = new Vector2f(10,10);
            this[3] = new Vector2f(0, 10);

        }
        /// <summary>
        /// Mise a jour des projectiles en jeu.
        /// </summary>
        /// <param name="deltaT">Angle du projectile.</param>
        /// <param name="gw">Accès a la logique du jeu et ses fonctions</param>
        /// <returns>true si le héro est en vie false sinon.</returns>
        public bool Update(float deltaT,GW gw)
        {
            var angleInRadians = (float)(Math.PI * deltaT / 180.0f);
            float X = ProjectileSpeed * (float)Math.Cos(angleInRadians);
            float Y = ProjectileSpeed * (float)Math.Sin(angleInRadians);
            Position += new Vector2f(X, Y);
            return false;

        }
       

    }
}
