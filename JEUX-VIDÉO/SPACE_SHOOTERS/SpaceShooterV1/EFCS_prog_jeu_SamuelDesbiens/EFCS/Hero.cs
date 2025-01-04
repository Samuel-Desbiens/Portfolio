using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
namespace EFCS
{
    class Hero : Character
    {

        /// <summary>
        ///type de character pour différencier les balle et désactivé le "friendly fire".
        /// </summary>
        private CharacterType type;
        /// <summary>
        /// Vitesse du héro.
        /// </summary>
        public const int HeroSpeed = 3;
        /// <summary>
        /// Compteur d'update de la classe.
        /// </summary>
        private int nbUpdate = 0;
        /// <summary>
        /// Bool qui vérifie si le joueur est encore en vie(true) ou pas (false).
        /// </summary>
        public bool IsAlive
        {
            get { return this.liveLeft>0; }
            set {; }
        }
        /// <summary>
        /// Vérifie le nombre de vie restante au joueur.
        /// </summary>
        public int Life
        {
            get { return this.liveLeft; }
            set {this.liveLeft = value; }
        }
        /// <summary>
        /// Retourne le nombre de Bombe restante au joueur.
        /// </summary>
        public int Bomb
        {
            get { return this.nbBombs; }
        }
        /// <summary>
        /// Nombre de vie que le joueur possède en début de partie.
        /// </summary>
        public const int LIFE_AT_BEGINING = 10;
        /// <summary>
        /// Couleur du héro.
        /// </summary>
        private static Color HeroColor = Color.Green;
        /// <summary>
        /// Nombre de bomb du joueur.
        /// </summary>
        private int nbBombs;
        /// <summary>
        /// Vie Restante au joueur.
        /// </summary>
        private int liveLeft;
        /// <summary>
        /// Constructeur du héro dans le jeu.
        /// </summary>
        /// <param name="posX">Position horizontal ou faire apparaitre le héro.</param>
        /// <param name="posY">Position horizontal ou faire apparaitre le héro.</param>
        public Hero(float posX, float posY)
            : base(posX, posY, 3, HeroColor, HeroSpeed)
        {
            //Donne une bombe en début de parti au joueur.
            nbBombs = 1;
            //Assigne les vie de départ au joueur.
            liveLeft = LIFE_AT_BEGINING;
            //Type des projectile que le héro vas tirer.
            Type = CharacterType.Hero;
            //Points pour l'affichage du héro
            this[0] = new Vector2f(10, 0);
            this[1] = new Vector2f(-10, 3);
            this[2] = new Vector2f(-10, -3);
        }
        /// <summary>
        /// Met a jour le héro en override des updates character et autres il vérifie les touche appuyer pour transformer les
        /// commandes de l'utilisateur en action.
        /// </summary>
        /// <param name="deltaT">Angle du vaisseau du héro</param>
        /// <param name="gw">Accès aux logiques du jeux</param>
        /// <param name="heroPos">Position du héro</param>
        /// <returns>Si le héro est en vie(true) ou non (false)</returns>
        public override bool Update(float deltaT, GW gw, Vector2f heroPos)
        {
            if (this.Life <= 0)
            {
                return false;
            }
            else
            {
                if (nbUpdate == int.MaxValue)
                {
                    nbUpdate = 0;
                }
                else
                {
                    nbUpdate++;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                    Rotate(-5);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                    Rotate(5);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    Advance(HeroSpeed);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                    Advance(-HeroSpeed);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    if (nbUpdate % 15 == 0)
                    {
                        Fire(gw, this.Angle);
                    }
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                {  
                    if(this.nbBombs >= 1)
                    {
                        this.nbBombs--;
                        FireBomb(gw);
                    }
                }
                        

                
                return true;
            }


        }
        /// <summary>
        /// Ajoute un bombe au joueur (après 15 seconde)
        /// </summary>
        public void AddBomb()
        {
            this.nbBombs++;
        }
        /// <summary>
        /// Lance le code qui détruit tout les ennemis a l'écran pour l'instant
        /// </summary>
        /// <param name="gw">Accès au logique du jeux et ses fonction.</param>
        private void FireBomb(GW gw)
        {
            gw.AllEnnemyExplode();
        }
       

       
                
    }
}

