#pragma once
#include "View.h"
#include "Image.h"
#include "Label.h"
#include "Button.h"
#include "GameplayModel.h"
#include "GameplayController.h"

class GameplayView :
  public View
{
public:
    GameplayView();
	~GameplayView();
	void Update();
protected:
	Label score;
	Image arme1;
	Image arme2;
	Image arme3;
};

