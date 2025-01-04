#include "stdafx.h"
#include "BossAnimation.h"
#include "GameContentManager.h"

BossAnimation::BossAnimation(sf::Sprite& bossSprite)
	:CyclicAnimation(bossSprite, 3, true)
{

}

bool BossAnimation::init(const GameContentManager& contentManager)
{	
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(32,  1906 ,84, 123)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(132, 1906, 84, 125)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(232, 1906, 77, 138)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(329, 1906, 75, 142)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(425, 1906, 76, 143)));
    frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(522, 1906, 73, 146)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(32,  2070, 71, 151)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(140, 2070, 69, 155)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(236, 2070, 70, 155)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(237, 2070, 68, 156)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(334, 2070, 65, 170)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(432, 2070, 65, 176)));
	frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(531, 2070, 64, 178)));

	for (int i = 10; i > 0; i--)
	{
		frames.push_back(frames[i]);
	}

	return true;
}
