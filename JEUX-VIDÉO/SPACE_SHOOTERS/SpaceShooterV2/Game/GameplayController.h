#pragma once
class GameplayModel;
class GameplayView;
class GameplayController
{
public:
  GameplayController(GameplayModel& model, GameplayView& view);
  
private:
  GameplayModel& model;
  GameplayView& view;
};

