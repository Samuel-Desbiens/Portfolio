#pragma once
class MainMenuModel;
class MainMenuView;
class MainMenuController
{
public:
  MainMenuController(MainMenuModel& model, MainMenuView& view);
  ~MainMenuController();
private:
  MainMenuModel& model;
  MainMenuView& view;
};

