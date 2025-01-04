#include "stdafx.h"
#include "MainMenuController.h"
#include "MainMenuModel.h"
#include "MainMenuView.h"

MainMenuController::MainMenuController(MainMenuModel& model, MainMenuView& view)
: model(model)
, view(view)
{
}


MainMenuController::~MainMenuController()
{
}
