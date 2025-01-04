#include "stdafx.h"
#include "ShipAnimation.h"
#include "GameContentManager.h"
#include "Inputs.h"

const int ShipAnimation::DEFAULT_FRAME_STATE = 0;

ShipAnimation::ShipAnimation(sf::Sprite& shipSprite)
	: InputBasedAnimation(shipSprite)
	, nbFrameState(DEFAULT_FRAME_STATE)
{

}

bool ShipAnimation::init(const GameContentManager& contentManager)
{
	//Middle
	frames.push_back(AnimationFrame(contentManager.getMainCharacterTexture(), sf::IntRect(270, 47, 23, 28)));
	//Gauche
	frames.push_back(AnimationFrame(contentManager.getMainCharacterTexture(), sf::IntRect(246, 47, 18, 28)));
	frames.push_back(AnimationFrame(contentManager.getMainCharacterTexture(), sf::IntRect(227, 47, 14, 27)));
	//Droite
	frames.push_back(AnimationFrame(contentManager.getMainCharacterTexture(), sf::IntRect(300, 47, 18, 28)));
	frames.push_back(AnimationFrame(contentManager.getMainCharacterTexture(), sf::IntRect(324, 47, 14, 27)));
	nextFrame = 0;

	return true;
}

void ShipAnimation::adjustNextFrame(const Inputs& inputs)
{
	if (inputs.moveFactorX > 0 && nbFrameState > -24)
	{
		nbFrameState--;
	}
	else if (inputs.moveFactorX < 0 && nbFrameState < 24)
	{
		nbFrameState++;
	}
	else if (inputs.moveFactorX == 0)
	{
		if (nbFrameState < 0)
		{
			nbFrameState++;
		}
		else if (nbFrameState > 0)
		{
			nbFrameState--;
		}
		else
		{
			//Do Nothing
		}
	}

	if (nbFrameState == 0)
	{
		InputBasedAnimation::nextFrame = 0;
	}
	else if (nbFrameState <= -14)
	{
		InputBasedAnimation::nextFrame = 4;
	}
	else if (nbFrameState >= 14)
	{
		InputBasedAnimation::nextFrame = 2;
	}
	else if (nbFrameState > 0)
	{
		InputBasedAnimation::nextFrame = 1;
	}
	else
	{
		InputBasedAnimation::nextFrame = 3;
	}
}
