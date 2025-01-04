#include "stdafx.h"
#include "Boss.h"
#include "game.h"
#include "BossAnimation.h"
Boss::Boss()
	:vie(50)
{

}

bool Boss::init(const GameContentManager& contentManager)
{
	isDead = false;
	cooldown = 0;
	activate();
	setPosition(sf::Vector2f(Game::GAME_WIDTH / 2, -10));
	currentState = State::BOSS;
	Animation* bossAnimation = new BossAnimation(*this);
	bool retval = bossAnimation->init(contentManager);
	if (retval)
	{
		animations[State::BOSS] = bossAnimation;
	}
	return retval && AnimatedGameObject::init(contentManager);
}

bool Boss::bUpdate(float deltaT, const Inputs& inputs,sf::Vector2f playerPos)
{
	if (cooldown > 0)
	{
		cooldown--;
	}
	if (this->getPosition().y <= 50)
	{
		move(sf::Vector2f(0, 5));
	}
	else
	{
		if (this->getPosition().x > playerPos.x)
		{
			move(sf::Vector2f(-3, 0));
		}
		else
		{
			move(sf::Vector2f(3, 0));
		}
	}

	return AnimatedGameObject::update(deltaT, inputs);
}

void Boss::takeDmg()
{
	this->vie--;
	if (vie <= 0)
	{
		this->destroyed();
	}
}

void Boss::destroyed()
{
	isDead = true;
	this->deactivate();
}

bool Boss::getIsDead()
{
	return this->isDead;
}

bool Boss::shouldAttack()
{
	if (cooldown <= 0)
	{
		cooldown = BASE_ATTACK_COOLDOWN;
		return 12 == animations[State::BOSS]->getNextFrame();
	}
	else
	{
		return false;
	}
}

