#include "stdafx.h"
#include "Enemy.h"
#include "game.h"
#include "GameContentManager.h"
#include "StandartEnemyAttackAnimation.h"
#include "Publisher.h"

#include "Inputs.h"

Enemy::Enemy()
	: isDead(false)
	,vie(5)
{

}

Enemy::Enemy(const Enemy& src)
	: AnimatedGameObject(src),
	isDead(src.isDead)
{
	init(*contentManager);
	setPosition(src.getPosition());
	if (src.isActive())
	{
		activate();
	}
	else
	{
		deactivate();
	}
	currentState = src.currentState;
}

bool Enemy::init(const GameContentManager& contentManager)
{
	this->vie = 5;
	setPosition(randSpawnPos());
	currentState = State::STANDARD_ENEMY;
	Animation* StdEnemyAtkAnimation = new StandartEnemyAttackAnimation(*this);
	bool retval = StdEnemyAtkAnimation->init(contentManager);
	if (retval)
	{
		animations[State::STANDARD_ENEMY] = StdEnemyAtkAnimation;
	}
	return retval && AnimatedGameObject::init(contentManager);
}

bool Enemy::update(float deltaT,const Inputs& inputs)
{
	if (active)
	{
		if (cooldown > 0)
		{
			cooldown--;
		}
		move(sf::Vector2f(0, 2));
		if (getPosition().y > Game::GAME_HEIGHT)
		{
			setPosition(sf::Vector2f(getPosition().x, 0));
		}
	}
	return AnimatedGameObject::update(deltaT,inputs);
}

void Enemy::takeDmg()
{
	this->vie--;
	if (vie <= 0)
	{
		this->destroyed();
	}
}

void Enemy::destroyed()
{
	isDead = true;
	this->deactivate();
	Publisher::notifySubscribers(Event::ENEMY_KILLED, this);
}

bool Enemy::getIsDead()
{
	return isDead;
}

bool Enemy::shouldAttack()
{
	if (cooldown <= 0)
	{
		cooldown = BASE_ATTACK_COOLDOWN;
		return 18 == animations[State::STANDARD_ENEMY]->getNextFrame();
	}
	else
	{
		return false;
	}
}

sf::Vector2f Enemy::randSpawnPos()
{
	float Y = 0.0f;
	float X = rand() % Game::GAME_WIDTH;

	return sf::Vector2f(X, Y);

}