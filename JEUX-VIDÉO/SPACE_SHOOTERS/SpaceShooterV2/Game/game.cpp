#include "stdafx.h"
#include "game.h"
#include "textbox.h"

const unsigned int Game::WIDTH = 1024;
const unsigned int Game::HEIGHT = 768;
const unsigned int Game::GAME_HEIGHT = Game::HEIGHT-100;
const float Game::FRAMERATE = 60.0f;
Game::Game(std::string windowName, unsigned int width, unsigned int height)
  : gameWidth(width)
  , gameHeight(height)
  , gameName(windowName)
  , currentState(GS_PLAYGAME)
{
     player = new Player(sf::Vector2f(500, 600));
}


void Game::Run()
{
  window.create(sf::VideoMode(gameWidth, gameHeight, 32), gameName);
  window.setFramerateLimit(60);
  
  ConnectEvents();
  
  // Initialiser toutes les vues
  views.insert(std::pair(GS_MAIN_MENU, &mainMenuView));
  views.insert(std::pair(GS_PLAYGAME, &gameplayView));

	  ShowView(GS_MAIN_MENU);
  while (window.isOpen())
  {
    if (views.find(currentState) != views.end())
      (views.find(currentState)->second)->HandleEvents(window);
    
    // Si fin de partie atteinte,
    if (true == Update())
    {
      // On termine "normalement" l'application
      break;
    }
    else
    {
      window.clear();
      
      if (views.find(currentState) != views.end())
        (views.find(currentState)->second)->Draw(window);

      //window.display();
    }

  }
}

bool Game::Update()
{
  static const float DELTA_T = 1.0f / Game::FRAMERATE;
  bool gameMustEnd = false;
  if (nullptr != views[currentState] )
  {    
    views[currentState]->Update(DELTA_T);
  }
  if (currentState == GS_PLAYGAME)
  {
	  if (nbUpdate == INT_MAX)
	  {
		  nbUpdate = 0;
	  }
	  else
	  {
		  nbUpdate++;
	  }
	  if (nbUpdateSinceLastFire == INT_MAX)
	  {
		  nbUpdateSinceLastFire = 0;
	  }
	  else
	  {
		  nbUpdateSinceLastFire++;
	  }
	 
	  gameMustEnd = player->Update();
	  for (int i = 0; i<enemys.size();i++)
	  {
          enemys[i]->Update(player->GetPos());
		  if (enemys[i]->GetLife() <= 0 || enemys[i]->Intersects(player))
		  {
			  enemysR.push_back(i);
			  if (enemys[i]->Intersects(player))
			  {
				  player->SetLife(25);
			  }
		  }
		  else
		  {
			  int r = rand.Next(0, 60);
			  if (r == 0)
			  {
				  BasicProjectile* BP = new BasicProjectile(false, enemys[i]->GetPos() + sf::Vector2f(0.5f, 0),sf::Color::Red);
				  projectiles.push_back(BP);
			  }
		  }
		 
	  }

	  for (int i = 0; i < enemysR.size(); i++)
	  {
  		   delete enemys[enemysR[i]];
		   enemys.erase(enemys.begin() + enemysR[i]);
	  }
	  enemysR.clear();

	  for (int i = 0; i < projectiles.size(); i++)
	  {
		  projectiles[i]->Update();
		  if (projectiles[i]->GetPos().x < 0 || projectiles[i]->GetPos().y < 0 || projectiles[i]->GetPos().x > 1000 || projectiles[i]->GetPos().y >780)
		  {
			  projectilesR.push_back(i);
		  }
		  if (projectiles[i]->GetPlayerOwned())
		  {
			  for (int o = 0; o < enemys.size(); o++)
			  {
				  if (projectiles[i]->Intersects(enemys[o]))
				  {
					  enemys[o]->Damaged(projectiles[i]->GetDMG());
					  projectilesR.push_back(i);
				  }
			  }
		  }
		  else
		  {
			  if (projectiles[i]->Intersects(player))
			  {
				  player->SetLife(projectiles[i]->GetDMG());
				  projectilesR.push_back(i);

			  }
		  }
		   
		 
	  }
	  for (int i = 0; i < projectilesR.size(); i++)
	  {
	       delete projectiles[projectilesR[i]];
		   projectiles.erase(projectiles.begin() + projectilesR[i]);
	  }
	  projectilesR.clear();
	 
	  for (int i = 0; i < bonus.size(); i++)
	  {
		  bonus[i]->Update();
	  }
	  HandleInput();
	  if (enemys.size() < 5)
	  {
		  int r=rand.Next(0, 3);
		  switch(r)
		  {
		  case 0:
		  {
			  BasicEnemy* enemyB = new BasicEnemy(sf::Vector2f(rand.Next(0, 700), 0));
			  enemys.push_back(enemyB);
			  break;
		  }
		  case 1:
		  {
			  Facer* enemyF = new Facer(sf::Vector2f(rand.Next(0, 700), 0));
			  enemys.push_back(enemyF);
			  break;
		  }
		  case 2:
		  {
			  Randomer* enemyR = new Randomer(sf::Vector2f(rand.Next(0, 700), 0));
			  enemys.push_back(enemyR);
			  break;
		  }
		  }	  
	  }
	  if (nbUpdate % 900 == 0)
	  {
		  int r = rand.Next(0, 3);
		  switch (r)
		  {
		  case 0:
		  {
			  Ammo* b = new Ammo(sf::Vector2f(rand.Next(0, 700), 0));
			  bonus.push_back(b);
			  break;
		  }
		  case 1:
		  {
			  BombB* b = new BombB(sf::Vector2f(rand.Next(0, 700), 0));
			  bonus.push_back(b);
			  break;
		  }
		  case 2:
		  {
			  Shield* b = new Shield(sf::Vector2f(rand.Next(0, 700), 0));
			  bonus.push_back(b);
			  break;
		  }
		  }
	  }
  }
  Draw(window);
  return gameMustEnd;
}

void Game::Draw(sf::RenderWindow &window)
{
	if (currentState == GS_PLAYGAME)
	{
		player->Draw(window);
		for (int i = 0; i < enemys.size(); i++)
		{
			enemys[i]->Draw(window);
		}
		for (int i = 0; i < projectiles.size(); i++)
		{
			if (projectiles[i] != NULL)
			{
				projectiles[i]->Draw(window);
			}
		}
	}
	
	window.display();
}

void Game::ShowView(GameState state)
{
  if (nullptr != views[state] && state != currentState)
  {
    if(views[currentState] != nullptr)
      views[currentState]->SetVisible(false);
    currentState= state;
    views[currentState]->Show();
  }
}

void Game::ConnectEvents()
{
  mainMenuView.AddListener(std::bind(&Game::OnMainMenuStartGame, this), MAIN_MENU_START_GAME);
  mainMenuView.AddListener(std::bind(&Game::OnMainMenuQuit, this), MAIN_MENU_QUIT);
}

void Game::OnMainMenuStartGame()
{
  ShowView(GS_PLAYGAME);
}

void Game::OnMainMenuQuit()
{
  window.close(); 
}


void Game::OnGameFinished()
{
	ShowView(GS_MAIN_MENU);
}

void Game::HandleInput()
{
	if (sf::Keyboard::isKeyPressed(sf::Keyboard::A) || sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
	{
		for (int i = 0; i < 3; i++)
		{
			player->MoveLeft();
		}

	}
	else if (sf::Keyboard::isKeyPressed(sf::Keyboard::D) || sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
	{
		for (int i = 0; i < 3; i++)
		{
			player->MoveRight();
		}
		
	}
	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Space) && nbUpdateSinceLastFire > 10)
	{
		nbUpdateSinceLastFire = 0;
		BasicProjectile* BP = new BasicProjectile(true, player->GetPos()+sf::Vector2f(0.5f,0),sf::Color::Yellow);
		projectiles.push_back(BP);
	}
}
