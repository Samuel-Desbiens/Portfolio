#include "stdafx.h"
#include "Player.h"
#include "Inputs.h"
#include "game.h"
#include "ShipAnimation.h"

Player::Player()
	:vie(50),bonusTime(0),alpha(255)
{

}
bool Player::init(const GameContentManager& contentManager)
{
	activate();
	setPosition(sf::Vector2f(Game::GAME_WIDTH * 0.5f, Game::GAME_HEIGHT * 0.5f));
	currentState = State::SHIP;
	this->setScale(3, 3);
	Animation* shipAnimation = new ShipAnimation(*this);
	bool retval = shipAnimation->init(contentManager);
	if (retval)
	{
		animations[State::SHIP] = shipAnimation;
	}
	return retval && AnimatedGameObject::init(contentManager);
}

bool Player::update(float deltaT, const Inputs& inputs)
{
	if (bonusTime > 0)
	{
		bonusTime--;
	}
	if (alpha < 255)
	{
		alpha += 5;
		if (alpha >= 255)
		{
			alpha = 255;
		}	
	}
	this->setColor(sf::Color(alpha, alpha, alpha));

	move(sf::Vector2f(inputs.moveFactorX, -inputs.moveFactorY));
	if (this->getPosition().x > Game::GAME_WIDTH + getGlobalBounds().width/2.0f)
	{
		move(sf::Vector2f(-getGlobalBounds().width / 2, 0));
	}
	else if (this->getPosition().x < -getGlobalBounds().width * 0.25f)
	{
		move(sf::Vector2f(0.5f * getGlobalBounds().width,0));
	}

	if (this->getPosition().y > Game::GAME_HEIGHT - getGlobalBounds().height - HUD_SIZE)
	{
		move(sf::Vector2f(0,-getGlobalBounds().height / 2));
	}
	else if (this->getPosition().y < -getGlobalBounds().height * 0.25f)
	{
		move(sf::Vector2f(0,0.5f * getGlobalBounds().height));
	}
	return AnimatedGameObject::update(deltaT, inputs);
}

bool Player::collidesWith(const Bullet& bullet) const
{
	if (this->getColor().g < 255)
	{
		return false;
	}
	return this->getGlobalBounds().intersects(bullet.getGlobalBounds());
}

bool Player::collidesWith(const Enemy& enemy) const
{
	if (this->getColor().g < 255)
	{
		return false;
	}
	return this->getGlobalBounds().intersects(enemy.getGlobalBounds());
}

bool Player::collidesWith(const Boss& boss) const
{
	if (this->getColor().g < 255)
	{
		return false;
	}
	return this->getGlobalBounds().intersects(boss.getGlobalBounds());
}

void Player::takeDamage(int dmg)
{
	if (this->bonusTime > 0)
	{
		bonusTime = 0;
	}
	else
	{
		this->vie -= dmg;
		if (vie <= 0)
		{
			this->deactivate();
		}
	}
	this->alpha = 0;
}

void Player::addBonusTime()
{
	bonusTime += BONUS_TIME_ADD;
}

void Player::addVie()
{
	vie += HEALTH_ADD;
}

int Player::getBonusTime()
{
	return this->bonusTime;
}

int Player::getVie()
{
	return this->vie;
}