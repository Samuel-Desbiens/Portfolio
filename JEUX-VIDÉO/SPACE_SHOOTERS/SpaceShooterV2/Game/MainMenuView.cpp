#include "stdafx.h"
#include "MainMenuView.h"
#include "game.h"

MainMenuView::MainMenuView()
: View(Game::WIDTH, Game::HEIGHT)
, imgBackground("Assets/Bitmaps/MainMenuBackground.tga", 1, 1)
, title("Assets/Fonts/DSDIGI.TTF", 80)
, btnQuit("Assets/Fonts/DSDIGI.TTF", 40, "Assets/Bitmaps/MainMenuQuit.jpg", 300, 75)
, btnStartGame("Assets/Fonts/DSDIGI.TTF", 40, "Assets/Bitmaps/MainMenuStartGame.jpg", 300, 75)
, controller(model, *this)
{
  ui.AddWidget(imgBackground);
  CenterInScreen();
  // Titre
  title.SetPosition(sf::Vector2f(0, 0));  
  title.SetText("A Space Shooter");
  title.CenterInScreen(*this);
  ui.AddWidget(title);

  // btnQuit
  btnQuit.SetPosition(sf::Vector2f(362, 300));
  btnQuit.CenterInScreen(*this);
  btnQuit.AddListener(std::bind(&MainMenuView::OnBtnQuit, this, std::placeholders::_1), sf::Event::MouseButtonPressed);
  ui.AddWidget(btnQuit);

  // btnStartGame
  btnStartGame.SetPosition(sf::Vector2f(362, 200));
  btnStartGame.CenterInScreen(*this);
  btnStartGame.AddListener(std::bind(&MainMenuView::OnBtnStartGame, this, std::placeholders::_1), sf::Event::MouseButtonPressed);
  ui.AddWidget(btnStartGame);
}


MainMenuView::~MainMenuView()
{
}

void MainMenuView::OnBtnQuit(const Widget& /*w*/)
{
  NotifyObservers(MAIN_MENU_QUIT);
}

void MainMenuView::OnBtnStartGame(const Widget& /*w*/)
{
  NotifyObservers(MAIN_MENU_START_GAME);
}
