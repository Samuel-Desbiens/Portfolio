#include "stdafx.h"
#include "ScoreContentManager.h"

ScoreContentManager::ScoreContentManager()
{

}

void ScoreContentManager::init()
{
	this->loadContent();
}

bool ScoreContentManager::loadContent()
{
	if (!mainFont.loadFromFile("Assets\\Fonts\\emulogic.ttf"))
		return false;

	return true;
}

const sf::Font& ScoreContentManager::getMainFont() const
{
	return mainFont;
}
