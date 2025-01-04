#include "stdafx.h"
#include "GameplayController.h"
#include "GameplayModel.h"
#include "GameplayView.h"

GameplayController::GameplayController(GameplayModel& model, GameplayView& view)
: model(model)
, view(view)
{
  
}
