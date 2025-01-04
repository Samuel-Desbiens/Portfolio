using System;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Timers;
using Tetris;
using WMPLib;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    ///   Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ;
        }
        Options option = new Options();
        
        /// <summary>
        /// Ensemble des panneaux auxquels nous changerons la couleur pour dessiner les blocs. (voir code plus
        /// bas)
        /// </summary>
        private Panel[,] visualGame = null;
        /// <summary>
        /// Nombre de colonnes
        /// </summary>
        int nbColumns = 10;
        /// <summary>
        /// Nombre de lignes
        /// </summary>
        private int nbRows = 20;
        //s'occupe de tout les random nécessaire au jeux
        Random rng = new Random();
        //créer le lecteur pour la musique
        WindowsMediaPlayer musique = new WindowsMediaPlayer();
        //variable qui garde en mémoire le nombre de colonne dans le tableau
        int NBColonneTableau = 10;
        //variable qui garde en mémoire le nombre de ligne dans le tableau
        int NBRowTableau = 20;
        //variables s'occupant de la position dans le tableau de jeu de la pièce principal des blocs
        int colonneBloc = 4;
        int rangerBloc = 0;
        //Variable s'occupant d'avoir acces au tableau de jeux peu importe ou nous somme dans le code
        int[,] globalGameBoard;
        //Variable s'occupant d'avoir acces au bloc en jeux actuel peu importe ou nous somme dans le code
        BlockType currentBlockType;
        //Variable s'occupe du timer pour déscendre la pièce automatiquement apres une seconde
        DispatcherTimer timer = new DispatcherTimer();
        //Variables s'occupant de garder les position des blocs dans la pièce actuel pour avoir accès partout dans le code
        int[] currentBlockRowConfiguration;
        int[] currentBlockColumnConfiguration;
        //Variables qui conserve le nombre de pièce qui as été jouer une pour chaque type
        int nbCarré = 0;
        int nbT = 0;
        int nbL = 0;
        int nbJ = 0;
        int nbBar = 0;
        int nbS = 0;
        int nbZ = 0;
        //Variable contenant le nombre total de pièce jouer cette parti
        int nbTotal = 0;
        //Variable vérifiant la fin du jeu et ici pour avoir un accès partout dans le code
        bool gameover = true;

        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Fonction servant a initialiser toute les choses en début de parti 
        /// et réinitialiser les variable necessaire
        /// </summary>
        private void InitGame()
        {
            //remet a zero les statistique de parti
            nbCarré = 0;
            nbT = 0;
            nbL = 0;
            nbJ = 0;
            nbBar = 0;
            nbS = 0;
            nbZ = 0;
            nbTotal = 0;
            //mets le dossier de musique dans la variable media player au début de la parti
            musique.URL = "\\TetrisETU\\DarudeSandstorm.mp3";
            // fait et s'assure que le tableaux de vérification des pièces soit vide
            globalGameBoard = InitLogicalGame(nbRows, nbColumns);
            // Le tableau visualGame est celui dont vous modifierez la propriété Background pour changer la 
            // couleur et faire afficher les blocs à l'écran
            visualGame = new Panel[nbRows, nbColumns];
            //Démarre la musique
            musique.controls.play();

            
            if (tetrisGrid.Children.Count > 1)
            {
                tetrisGrid.Children.RemoveRange(1, tetrisGrid.Children.Count - 1);
            }
            //Fait les cases du jeux
            for (int i = 0; i < nbRows; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    // Vous n'avez pas à vous soucier de comprendre le code suivant.  Je l'ai juste fourni pour vous
                    // éviter de devoir créer 300 cases à la main.
                    visualGame[i, j] = new Canvas();
                    //visualGame[i, j].SnapsToDevicePixels = true;
                    tetrisGrid.Children.Add(visualGame[i, j]);
                    Grid.SetColumn(visualGame[i, j], j);
                    // On saute une ligne pour la garder pour le menu (d'où le i+1)
                    Grid.SetRow(visualGame[i, j], i + 1);
                }
            }
            // choisi la première pièce aléatoirement
            currentBlockType = ProchainePiece();
            //Affiche les graphique au joueur
            DrawGame();
        }
        /// <summary>
        /// Fonction s'occupant de créer et initialiser a zero le tableau logique du jeu
        /// </summary>
        /// <param name="nbRows">nombre de ranger dans le tableau</param>
        /// <param name="nbColums">nombre de colonne dans le tableau de jeu</param>
        /// <returns>Le tableau de jeux logique</returns>
        public int[,] InitLogicalGame(int nbRows, int nbColums)
        {
            int[,] gameBoard = new int[nbRows, nbColumns];
            // Initialiser le tableau de jeu à "vide"
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = 0;
                }
            }

            return gameBoard;
        }
        /// <summary>
        /// Toutes les couleurs associées aux types de blocs
        /// </summary>
        private readonly Color[] allBlockColors =
    {
      Color.FromRgb(0, 0, 0),       // None
      Color.FromRgb(127, 127, 127), // Frozen
      Color.FromRgb(0, 0, 255),     // Square
      Color.FromRgb(255, 255, 0),   // Bar
      Color.FromRgb(255, 0, 0),     // T              
      Color.FromRgb(255, 192, 203), // L
      Color.FromRgb(255, 235, 205), // J
      Color.FromRgb(165, 42, 42),   // S
      Color.FromRgb(240, 128, 128)  // S_i
    };
        /// <summary>
        /// Événement généré lorsque le joueur appuie sur une touche pour déplacer un bloc.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                case Key.W:
                case Key.S:
                case Key.D:
                    {
                        HandleUserInput(e.Key);
                        e.Handled = true;
                        DrawGame();
                        break;
                    }
            }
        }
        /// <summary>
        /// S'occupe d'afficher le jeux aux joueur
        /// </summary>
        private void DrawGame()
        {
            // Afficher les pièces "gelées"
            for (int i = 0; i < globalGameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < globalGameBoard.GetLength(1); j++)
                {
                    visualGame[i, j].Background = new SolidColorBrush(allBlockColors[globalGameBoard[i, j]]);
                }
            }
            int currentBlockRowPosition = rangerBloc;
            int currentBlockColumnPosition = colonneBloc;
            for (var i = 0; i < 4; i++)
                visualGame[currentBlockRowPosition + currentBlockRowConfiguration[i], currentBlockColumnPosition + currentBlockColumnConfiguration[i]].Background =
                  new SolidColorBrush(allBlockColors[(int)currentBlockType]);
        }
        /// <summary>
        /// S'occupe de bouger le bloc en jeux vers le bas après le temps impatie
        /// </summary>
        void CallSeconde()
        {
            bool mp = MouvementPossible(rangerBloc+1,colonneBloc);
            if (mp == true)
            {
                rangerBloc++;
                DrawGame();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    globalGameBoard[rangerBloc + currentBlockRowConfiguration[i], colonneBloc + currentBlockColumnConfiguration[i]] = 1;
                }
                GenererProchainePièce();
            }
        }
        /// <summary>
        /// Choisie la pièce aléatoire qui sera jouer prochainement
        /// </summary>
        /// <returns>un type de bloc qui sera jouer</returns>
        private BlockType ProchainePiece()
        {
            int nextPiece = rng.Next(0, 7);
            switch (nextPiece)
            {
                case 0:
                    currentBlockColumnConfiguration = new int[4] { 0, 0, 1, 1 };
                    currentBlockRowConfiguration = new int[4] { 0, 1, 0, 1 };
                    nbCarré++;
                    nbTotal++;
                    return BlockType.Square;
                case 1:
                    currentBlockColumnConfiguration = new int[4] { 0, 1, 2, 3 };
                    currentBlockRowConfiguration = new int[4] { 0, 0, 0, 0 };
                    nbBar++;
                    nbTotal++;
                    return BlockType.Bar;
                case 2:
                    currentBlockColumnConfiguration = new int[4] { 0, 0, 0, 1 };
                    currentBlockRowConfiguration = new int[4] { 0, 1, 2, 1 };
                    nbT++;
                    nbTotal++;
                    return BlockType.T;
                case 3:
                    currentBlockColumnConfiguration = new int[4] { 0, 0, 0, 1 };
                    currentBlockRowConfiguration = new int[4] { 0, 1, 2, 2 };
                    nbL++;
                    nbTotal++;
                    return BlockType.L;
                case 4:
                    currentBlockColumnConfiguration = new int[4] { 1, 1, 1, 0 };
                    currentBlockRowConfiguration = new int[4] { 0, 1, 2, 2 };
                    nbJ++;
                    nbTotal++;
                    return BlockType.J;
                case 5:
                    currentBlockColumnConfiguration = new int[4] { 0, 1, 1, 2 };
                    currentBlockRowConfiguration = new int[4] { 1, 0, 1, 0 };
                    nbS++;
                    nbTotal++;
                    return BlockType.S;
                case 6:
                    currentBlockColumnConfiguration = new int[4] { 0, 1, 1, 2 };
                    currentBlockRowConfiguration = new int[4] { 0, 0, 1, 1 };
                    nbZ++;
                    nbTotal++;
                    return BlockType.Z;
            }
            return BlockType.Square;
        }
        /// <summary>
        /// Gère le déplacement souhaité par l'utilisateur.
        /// </summary>
        /// <param name="key">La touche choisie par le joueur</param>
        private void HandleUserInput(Key key)
        {
            switch (key)
            {
                case Key.A:
                    {
                        bool faitMouvementA = MouvementPossible(rangerBloc, colonneBloc-1);

                        if (faitMouvementA == true)
                        {
                            colonneBloc--;
                        }

                        break;
                    }
                case Key.D:
                    {
                        bool faitMouvementD = MouvementPossible(rangerBloc , colonneBloc+ 1);
                        if (faitMouvementD == true)
                        {
                            colonneBloc++;
                        }
                        break;
                    }
                case Key.S:
                    {
                        bool faitMouvementS = MouvementPossible(rangerBloc+ 1, colonneBloc );
                        if (faitMouvementS == true)
                        {
                            rangerBloc++;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                globalGameBoard[rangerBloc + currentBlockRowConfiguration[i], colonneBloc + currentBlockColumnConfiguration[i]] = 1;
                            }
                            GenererProchainePièce();
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// S'occupe de réinitialiser la position de la pièce et appelle la méthode pour choisir une
        /// pièce aléatoirement
        /// </summary>
        void GenererProchainePièce()
        {
            if (gameover)
            {
                currentBlockType = ProchainePiece();
                
                colonneBloc = 4;
                rangerBloc = 0;
                gameover = MouvementPossible(rangerBloc, colonneBloc);
                if (gameover == true)
                {
                    VérifierLignePleine();
                    DrawGame();
                }
                else
                {
                    MessageBox.Show("Vous avez perdu :(", "You Are Lose");
                    MessageBox.Show("Nombre de Bar recue: " + nbBar + " (" + moyennePiece(nbBar, nbTotal) + "%)"
                        + Environment.NewLine + "Nombre de carré recue : " + nbCarré + " (" + moyennePiece(nbCarré, nbTotal) + "%)"
                        + Environment.NewLine + "Nombre de L recue : " + nbL + " (" + moyennePiece(nbL, nbTotal) + "%)"
                        + Environment.NewLine + "Nombre de J recue : " + nbJ + " (" + moyennePiece(nbJ, nbTotal) + "%)"
                        + Environment.NewLine + "Nombre de S recue : " + nbS + " (" + moyennePiece(nbS, nbTotal) + "%)"
                        + Environment.NewLine + "Nombre de Z recue : " + nbZ + " (" + moyennePiece(nbZ, nbTotal) + "%)"
                        + Environment.NewLine + "Nombre de T recue : " + nbT + " (" + moyennePiece(nbT, nbTotal) + "%)","Statistique");
                    gameover = false;
                }
            }
            float moyennePiece(int nbP,int nbT)
            {
                float NBPF =Convert.ToInt32(nbP);
                float NBTF = Convert.ToInt32(nbT);
                if (NBTF == 0)
                {
                    return 0f;
                }
                else
                {
                    float moyene = (NBPF / NBTF) *100;
                    return moyene;
                }
               
            }   
        }
        //Code modifier\fusionner par Pierre Poulin
        /// <summary>
        /// Vérification de quel mouvement la pièce peut faire
        /// </summary>
        /// <param name="row">ranger de la pièce actuelle</param>
        /// <param name="col">colonne de la pièce actuelle</param>
        /// <returns>(true):si le movement demander est possible (false):si le mouvement n'est pas possible</returns>
        bool MouvementPossible(int row, int col)
        {
            int NBCarréQuiOntPasserLeTest = 0;
            for (int i = 0; i < 4; i++)
            {
                if (row + currentBlockRowConfiguration[i] < globalGameBoard.GetLength(0)
                    && col + currentBlockColumnConfiguration[i] < globalGameBoard.GetLength(1)
                    && col + currentBlockColumnConfiguration[i] >= 0
                    && globalGameBoard[row + currentBlockRowConfiguration[i], col + currentBlockColumnConfiguration[i]] != 1)
                {
                    NBCarréQuiOntPasserLeTest++;
                }
            }
            if (NBCarréQuiOntPasserLeTest >= 4)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Vérifie l'entièreter du tableau pour savoir si une ligne est pleine et appelle la fonction de
        /// décendre les ligne si il en as au dessu de celle qui est pleine
        /// </summary>
        void VérifierLignePleine()
        {  
            for(int i=0;i < globalGameBoard.GetLength(0); i++)
            {
                int carréDeLignePleine = 0;
                for (int u = 0; u < globalGameBoard.GetLength(1); u++)
                {
                   if(globalGameBoard[i, u] == 1)
                    {
                        carréDeLignePleine++;
                    }
                }
                if (carréDeLignePleine >= globalGameBoard.GetLength(1))
                {
                    DescendreLigne(i);    
                }
            
            }
        }
        /// <summary>
        /// s'occuope de déscendre des lignes au dessus de ranger maintenant éffacer
        /// </summary>
        /// <param name="ligneVide">l'indice du tableau 2d de la ligne maintenant vide</param>
        void DescendreLigne(int ligneVide)
        {
            //L'ider proposer par david(méthode appeler dans la méthode)
            //Code que j'avais fait marchait mais était plus long
           
            if (ligneVide == 0)
                return;
            for(int x = 0; x < globalGameBoard.GetLength(1); x++)
            {
                globalGameBoard[ligneVide, x] = globalGameBoard[ligneVide - 1, x];
            }
            DescendreLigne(ligneVide - 1);
        }
    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
        /// <summary>
        /// S'occupe de démarer la partie quand le bouton est cliquer
        /// </summary>
        private void MenuItem_Click_StartGame(object sender, RoutedEventArgs e)
        {
            int colonneBloc = 4;
            int rangerBloc = 0;
            InitGame();
            Start.Header = "Redémarer Parti";
            //ProchainePiece();
            DrawGame();
            //code fait par David
            timer.Tick += delegate { CallSeconde(); };
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        /// <summary>
        /// Affiche le menu des options quand le bouton est appuyer
        /// </summary>
        public void MenuItem_Click_Options(object sender, RoutedEventArgs e)
        {
            option.Show();
            bool accept = Options.accept();
            if (accept)
            {
                bool song = Options.MusicOrNot();
                if (song)
                {
                    if (musique.playState == WMPPlayState.wmppsPlaying)
                    {
                        ;
                    }
                    else
                    {
                        musique.controls.play();
                    }
                }
                else
                {
                    musique.controls.stop();
                }
                //int NBRowTableau = Options.NombreRanger();
                //int NBColumnTableau = Options.NombreColonne();
            }
        }  
    }
}