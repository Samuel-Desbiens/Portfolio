#include "stdafx.h"
#include "GameContentManager.h"
GameContentManager::GameContentManager()
{
}

void GameContentManager::init()
{
    this->loadContent();
}

bool GameContentManager::loadContent()
{

    if (!backgroundTexture.loadFromFile("Assets\\Sprites\\Level01\\SpaceBackground.png"))
        return false;
    else
        backgroundTexture.setRepeated(true);
    if (!characterTexture.loadFromFile("Assets\\Sprites\\Level01\\NES - The Guardian Legend - The Guardian Alyssa.bmp"))
        return false;
    if (!bonusTextures.loadFromFile("Assets\\Sprites\\Level01\\NES - The Guardian Legend - Miscellaneous.bmp"))
        return false;
    if (!enemiesTexture.loadFromFile("Assets\\Sprites\\Level01\\NES - The Guardian Legend - Bosses.bmp"))

        return false;
    if (!mainFont.loadFromFile("Assets\\Fonts\\emulogic.ttf"))

        return false;
    if (!playerGunSoundBuffer.loadFromFile("Assets\\SoundFX\\Level01\\playerGun.wav"))
        return false;
    if (!enemyGunSoundBuffer.loadFromFile("Assets\\SoundFX\\Level01\\enemyGun.wav"))
        return false;

    if (!healthBonusSoundBuffer.loadFromFile("Assets\\SoundFX\\Level01\\healthBonus.wav"))
        return false;
    if (!gunBonusSoundBuffer.loadFromFile("Assets\\SoundFX\\Level01\\gunBonus.wav"))
        return false;
    if (!enemyKilledSoundBuffer.loadFromFile("Assets\\SoundFX\\Level01\\enemyKilled.wav"))
        return false;
    return true;
}
const sf::Texture& GameContentManager::getEnemiesTexture() const
{
    return enemiesTexture;
}

const sf::Texture& GameContentManager::getBackgroundTexture() const
{
    return backgroundTexture;
}
const sf::Texture& GameContentManager::getBonusTexture() const
{
    return bonusTextures;
}

const sf::Texture& GameContentManager::getMainCharacterTexture() const
{
    return characterTexture;
}

const sf::Font& GameContentManager::getMainFont() const
{
    return mainFont;
}
const sf::SoundBuffer& GameContentManager::getPlayerGunSoundBuffer() const
{
    return playerGunSoundBuffer;
}

const sf::SoundBuffer& GameContentManager::getEnemyGunSoundBuffer() const
{
    return enemyGunSoundBuffer;
}

const sf::SoundBuffer& GameContentManager::getHealthSoundBuffer() const
{
    return healthBonusSoundBuffer;
}

const sf::SoundBuffer& GameContentManager::getGunBonusSoundBuffer() const
{
    return gunBonusSoundBuffer;
}
const sf::SoundBuffer& GameContentManager::getEnemyKilledSoundBuffer() const
{
    return enemyKilledSoundBuffer;
}