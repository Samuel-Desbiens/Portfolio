#include "stdafx.h"
#include "Bonus.h"
#include "game.h"
#include "Publisher.h"
#include "GameContentManager.h"

Bonus::Bonus()
	:GameObject()
	, type(bonusType::BNONE)
{
	setPosition(sf::Vector2f(0, 0));
}

Bonus::Bonus(const Bonus& src)
	:GameObject(src)
{
	this->type = src.type;
}

bool Bonus::init(bonusType btype,const GameContentManager& contentManager)
{
	GameObject::init(contentManager);
	this->type = btype;
	setScale(2, 2);
	setTexture(contentManager.getBonusTexture());
	if (this->type == bonusType::HEALTH)
	{
		setTextureRect(sf::IntRect(248,104,16,16));
	}
	else if (this->type == bonusType::GUN)
	{
		setTextureRect(sf::IntRect(290,62,15,15));
	}
	return false;
}

bool Bonus::update()
{
	if (isActive())
	{
		move(sf::Vector2f(0, 10));
		if (this->getPosition().y > Game::GAME_HEIGHT + 1)
		{
			this->deactivate();
		}
	}
	return false;
}

void Bonus::collidesWith(const Player& p)
{
	if (this->getGlobalBounds().intersects(p.getGlobalBounds()))
	{
		if (this->type == bonusType::GUN)
		{
			Publisher::notifySubscribers(Event::GUN_PICKED_UP,this);
		}
		else if (this->type == bonusType::HEALTH)
		{
			Publisher::notifySubscribers(Event::HEALTH_PICKED_UP, this);
		}
		this->deactivate();
	}
}

