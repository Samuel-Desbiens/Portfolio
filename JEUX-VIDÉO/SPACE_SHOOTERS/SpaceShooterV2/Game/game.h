#pragma once
#include "stdafx.h"
#include <iostream>
#include <string>
#include <vector>
#include "MainMenuView.h"
#include "GameplayView.h"
#include "Character.h"
#include "Projectile.h"
#include "Enemy.h"
#include "Player.h"
#include "random.h"
#include "BasicEnemy.h"
#include "Facer.h"
#include "Randomer.h"
#include "BasicProjectile.h"
#include "Ammo.h"
#include "BombB.h"
#include "Shield.h"

class Game
{
enum GameState
{
  GS_PLAYGAME,
  GS_MAIN_MENU,
};
public:
  static const unsigned int WIDTH;
  static const unsigned int HEIGHT;
  static const unsigned int GAME_HEIGHT;
  static const float FRAMERATE;


  Game(std::string windowName,unsigned int width, unsigned int height);
  void Run();

  /// <summary>
  /// Met à jour le jeu
  /// </summary>
  /// <returns>true si le jeu doit se terminer (partie perdue), false sinon</returns>
  bool Update();

  void Draw(sf::RenderWindow &window);
  /// <summary>
  /// Gère les entrées du joueur au clavier.
  /// </summary>
  void HandleInput();

private:
  void ShowView(GameState state);
  void ConnectEvents();

  std::map< GameState, View*> views;
  MainMenuView mainMenuView;
  GameplayView gameplayView;

	sf::RenderWindow window;


  void OnMainMenuQuit();
  void OnGameFinished();
  void OnMainMenuStartGame();

	/// <summary>
	/// La largeur du jeu en pixels.
	/// </summary>
	int gameWidth;

	/// <summary>
	/// La hauteur du jeu en pixels.
	/// </summary>
	int gameHeight;

  std::string gameName;

  GameState currentState;
  //Listes contenant les enemy/projectiles/bonus pour les mettres a jour et les afficher correctement a l'écran.
  std::vector <Enemy*> enemys;
  std::vector <int> enemysR;

  std::vector <Projectile*> projectiles;
  std::vector <int> projectilesR;

  std::vector <Bonus*> bonus;
  std::vector <int> bonusR;
  
  //Nombre d'update passer dans le jeu occupant la fonction de timer.
  int nbUpdate;
  //Nombre d'update depuis le dernier tire du joueur pour qu'il ait un certain delay entre chaque tir.
  int nbUpdateSinceLastFire;
  //Le player du jeu
  Player* player;
  //Variable s'occupant de la chance dans le jeu.
  Random rand;
};

