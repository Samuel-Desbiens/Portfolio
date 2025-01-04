#include "stdafx.h"
#include "TitleScreenContentManager.h"

TitleScreenContentManager::TitleScreenContentManager()
{
}

void TitleScreenContentManager::init()
{
    this->loadContent();
}

bool TitleScreenContentManager::loadContent()
{
    if (!mainFont.loadFromFile("Assets\\Fonts\\emulogic.ttf"))
        return false;

    if (!titleScreenTexture.loadFromFile("Assets\\Sprites\\Title\\Title.png"))
        return false;

    return true;
}

const sf::Font& TitleScreenContentManager::getMainFont() const
{
    return mainFont;
}

const sf::Texture& TitleScreenContentManager::getTitleScreenTexture() const
{
    return titleScreenTexture;
}