#pragma once
#include "View.h"
#include "MainMenuModel.h"
#include "MainMenuController.h"
#include "SlidingButton.h"
#include "Image.h"
//#include "CheckButton.h"

#define MAIN_MENU_START_GAME (LAST_VIEW_EVENT+1)
#define MAIN_MENU_QUIT (MAIN_MENU_START_GAME+1)
#define MAIN_MENU_CONNECT (MAIN_MENU_QUIT+1)
class MainMenuView :
  public View
{
public:
  MainMenuView();
  ~MainMenuView();
  
protected:
  // Elements d'interface utilisateur
  Image imgBackground;
  Label title;
  SlidingButton btnStartGame;
  SlidingButton btnQuit;
  // Modèle associé à la vue
  MainMenuModel model;

  // Contrôleur associé à la vue
  MainMenuController controller;
private:
  void OnBtnQuit(const Widget& w);
  void OnBtnStartGame(const Widget& w);
};

