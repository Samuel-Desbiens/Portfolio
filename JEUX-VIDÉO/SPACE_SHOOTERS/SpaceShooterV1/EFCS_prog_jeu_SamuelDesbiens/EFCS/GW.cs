using System;
using System.IO;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.Collections.Generic;
namespace EFCS
{
    public class GW
    {
        // Constantes et propriétés statiques
        public const int WIDTH = 1024;
        public const int HEIGHT = 768;
        public const uint FRAME_LIMIT = 60;
        const float DELTA_T = 1.0f / (float)FRAME_LIMIT;
        public Random r = new Random();
        /// <summary>
        /// Instance du héro le rendant accessible a la classe.
        /// </summary>
        Hero spaceship;



        // SFML
        RenderWindow window = null;
        Font font = new Font("Data/emulogic.ttf");
        Text text = null;

        // Propriétés pour la partie
        Language CurrentLanguage { get; set; }
        private float totalTime = 0;
        private int nbEnnemy;
        private int nbUpdate = 0;

        /// <summary>
        /// Liste des List utilisé partout a travers le jeu.
        /// </summary>
        private List<Character> characters = new List<Character>();
        private List<BasicEnemy> basicEnemys = new List<BasicEnemy>();
        private List<Enemy> enemiesToRemove = new List<Enemy>();
        private List<BasicEnemy> basicEnemiesToRemove = new List<BasicEnemy>();
        private List<Projectile> projectiles = new List<Projectile>();
        private List<Projectile> projectilesToRemove = new List<Projectile>();

        private List<Star> stars = new List<Star>();


        private void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
        /// <summary>
        /// Commande pour changer la langue affiché.
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">...</param>
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            
            if (e.Code == Keyboard.Key.F4)
                CurrentLanguage = Language.French;
            else if (e.Code == Keyboard.Key.F5)
                CurrentLanguage = Language.English;
        }
        /// <summary>
        /// Créer la partie avec les choses nécessaire a son démarage.
        /// </summary>
        public GW()
        {
            text = new Text("", font);
            CurrentLanguage = Language.English;
            window = new RenderWindow(new SFML.Window.VideoMode(WIDTH, HEIGHT), "GW");
            window.Closed += new EventHandler(OnClose);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.SetKeyRepeatEnabled(false);
            window.SetFramerateLimit(FRAME_LIMIT);
            spaceship = new Hero(HEIGHT / 2, WIDTH / 2);
            characters.Add(spaceship);

            //Ajoute les étoiles en fond.
            for (int i = 0; i < 100; i++)
            {
                stars.Add(new Star(r.Next(0, WIDTH), r.Next(0, HEIGHT)));

            }

        }


        public void Run()
        {

            // Chargement de la StringTable.
            if (ErrorCode.OK == StringTable.GetInstance().Parse(File.ReadAllText("Data/st.txt")))
            {
                window.SetActive();

                while (window.IsOpen)
                {
                    window.Clear(Color.Black);
                    window.DispatchEvents();
                    if (false == Update())
                        break;
                    Draw();
                    window.Display();
                }
            }
        }
        /// <summary>
        /// Affiche les graphiques du jeux en passant a travers tous les objects du jeu.
        /// </summary>
        public void Draw()
        {

            foreach (Character character in characters)
            {
                character.Draw(window);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(window);
            }
            foreach (BasicEnemy basicEnemy in basicEnemys)
            {
                basicEnemy.Draw(window);
            }
            foreach (Star star in stars)
            {
                star.Draw(window);
            }


            // Temps total afficher.
            text.Position = new Vector2f(0, 10);
            text.DisplayedString = string.Format("{1} = {0,-5}", ((int)(totalTime)).ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_TOTAL_TIME"));
            window.Draw(text);

            // Points de vie afficher.
            text.Position = new Vector2f(0, 50);
            text.DisplayedString = string.Format("{1} = {0,-4}", spaceship.Life.ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_LIFE"));
            window.Draw(text);
      
            // Nombre de Bombe afficher.
            text.Position = new Vector2f(720,10);
            text.DisplayedString = string.Format("{1} = {0,-4}", spaceship.Bomb.ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_BOMB"));
            window.Draw(text);
        }
        /// <summary>
        /// Ajoute les projectile lorsque Fire() a été appeler
        /// </summary>
        /// <param name="proj">Le projectile a rajouter a la liste avec déja ses information nésessaire.</param>
        public void AddProjectile(Projectile proj)
        {
            projectiles.Add(proj);
        }
        /// <summary>
        /// Ajoute l'ennemis Basic a la liste lorsqu'un plus gros ennemis est détruit
        /// </summary>
        /// <param name="BE"></param>
        public void AddBasicEnemy(BasicEnemy BE)
        {
            basicEnemys.Add(BE);
        }
        /// <summary>
        /// Détruit tous les énnemis lorsqu'une bombe est largué.
        /// </summary>
        public void AllEnnemyExplode()
        {
            foreach (BasicEnemy BE in basicEnemys)
            {
                BE.Explode(this);
            }
            foreach (Character character in characters)
            {
                if (character is Enemy)
                    character.Explode(this);
            }

        }
        /// <summary>
        /// Fait disparaitre les énnemis détruit par bombe ou par tir du héro.
        /// </summary>
        /// <param name="deadEnemy">l'énnemis a détruire.</param>
        public void DeletingEnemy(Enemy deadEnemy)
        {
            enemiesToRemove.Add(deadEnemy);
        }
        /// <summary>
        /// Fait disparaitre les énnemis de base qui ont été détruit par bombe ou par tir de héro.
        /// </summary>
        /// <param name="deadBasicEnemy">L'énnemis a détruire</param>
        public void DeletingBasicEnemy(BasicEnemy deadBasicEnemy)
        {
            basicEnemiesToRemove.Add(deadBasicEnemy);
        }
        /// <summary>
        /// Détermine si un Movable est situé à l'intérieur de la surface de jeu
        /// Peut être utilisée pour déterminer si les projectiles sont sortis du jeu
        /// ou si le héros ou un ennemi s'apprête à sortir.
        /// </summary>
        /// <param name="m">Le Movable à tester</param>
        /// <returns>true si le Movable est à l'intérieur, false sinon</returns>
        public bool Contains(Movable m)
        {
            FloatRect r = new FloatRect(0, 0, GW.WIDTH, GW.HEIGHT);
            return r.Contains(m.Position.X, m.Position.Y);
        }

       /// <summary>
       /// Mise a jour du jeux et de tout les objects de celui-ci vérifie aussi la logique du jeu bref les collisions entre objects
       /// </summary>
       /// <returns>true si le joueur est en vie et false si le joueur est a 0 ou moin de point de vie.</returns>
        public bool Update()
        {
            if (nbUpdate == int.MaxValue)
            {
                nbUpdate = 0;
            }
            else
            {
                nbUpdate++;
            }

            if (spaceship.IsAlive)
            {
                //Rajoute une bombe au héro après chaque 15 seconde.
                if(nbUpdate % 900 == 0)
                {
                    spaceship.AddBomb();
                }
                //Vérification des collision et action en conséquence.
                foreach(Character character in characters)
                {
                    if(character is Enemy)
                    {
                        if (spaceship.Intersects(character))
                        {
                            spaceship.Life -= 3;
                        }
                    }
                    
                }

                foreach(BasicEnemy basicEnemy in basicEnemys)
                {
                    if (spaceship.Intersects(basicEnemy))
                    {
                        spaceship.Life--;
                    }
                }

                foreach(Projectile projectile in projectiles)
                {
                    if(projectile.Type == CharacterType.Ennemy)
                    {
                        if (spaceship.Intersects(projectile))
                        {
                            spaceship.Life--;
                        }
                        
                    }
                    else
                    {
                        foreach(Character character in characters)
                        {
                            if(character is Enemy)
                            {
                                if (character.Intersects(projectile))
                                {
                                    projectilesToRemove.Add(projectile);
                                    character.Explode(this);
                                }
                            }
                        }
                        foreach(BasicEnemy basicEnemy in basicEnemys)
                        {
                            if (basicEnemy.Intersects(projectile))
                            {
                                projectilesToRemove.Add(projectile);
                                basicEnemy.Explode(this);
                            }
                        }
                    }
                }

               //Retire les ennemie a retirer.
                foreach (Enemy enemy in enemiesToRemove)
                {

                    characters.Remove(enemy);
                }
                enemiesToRemove.Clear();

                foreach (BasicEnemy enemy in basicEnemiesToRemove)
                {
                    basicEnemys.Remove(enemy);
                }
                basicEnemiesToRemove.Clear();
                //Calcule le nombre d'énnemis actuellement dans le jeu.
                foreach (Character character in characters)
                {
                    if (character is Enemy)
                    {
                        nbEnnemy++;
                    }
                }
                //Si il y a moin de 5 énnemis en rajoute un chaque 3/4 de seconde.
                if (nbUpdate % 45 == 0)
                {
                    if (nbEnnemy < 5)
                    {
                        int random = r.Next(0, 2);
                        if (random == 0)
                        {
                            characters.Add(new Circle(r.Next(0, WIDTH), r.Next(0, HEIGHT)));
                        }
                        else
                        {
                            characters.Add(new Square(r.Next(0, WIDTH), r.Next(0, HEIGHT)));
                        }

                    }
                }
                nbEnnemy = 0;
                //Mets a jour le héro et les énnemis.
                foreach (Character character in characters)
                {
                    character.Update(DELTA_T, this, spaceship.Position);
                }
                //Mets a jour les basicEnemy.
                foreach (BasicEnemy BE in basicEnemys)
                {
                    BE.Update(DELTA_T, this, spaceship.Position);
                }


                //Mets a jour les projectiles et retire ceux a retirer.
                foreach (Projectile projectile in projectiles)
                {
                    projectile.Update(projectile.Angle, this);
                    if (Contains(projectile))
                    {
                        ;
                    }
                    else
                    {
                        projectilesToRemove.Clear();
                        projectilesToRemove.Add(projectile);
                    }
                }
                foreach (Projectile projectile in projectilesToRemove)
                {
                    projectiles.Remove(projectile);

                }
                //Variable pour afficher le temps.
                totalTime = nbUpdate / FRAME_LIMIT;
                //Mets a jour les étoiles en fond
                foreach(Star star in stars)
                {
                    star.Update(spaceship.Angle,this);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

           
    }
}
