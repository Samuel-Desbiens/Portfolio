#include "stdafx.h"
#include "StandartEnemyAttackAnimation.h"
#include "GameContentManager.h"

const int StandartEnemyAttackAnimation::FRAME_STATE = 0;

StandartEnemyAttackAnimation::StandartEnemyAttackAnimation(sf::Sprite& EnemySprite)
	: CyclicAnimation(EnemySprite,5,true)
	, nbFrame(FRAME_STATE)
{

}

bool StandartEnemyAttackAnimation::init(const GameContentManager& contentManager)
{
	for (int i = 0; i < 2; i++)
	{
		for (int o = 0; o < 9; o++)
		{
			frames.push_back(AnimationFrame(contentManager.getEnemiesTexture(), sf::IntRect(BasePosX + (DecalageX * o), BasePosY + (DecalageY * i), DimensionX, DimensionY)));
		}
	}
	for (int p = 16; p > 0; p--)
	{
		frames.push_back(frames[p]);
	}

	return true;
}

