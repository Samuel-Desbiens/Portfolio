#include "stdafx.h"
#include "Bullet.h"
#include "game.h"
#include "GameContentManager.h"

Bullet::Bullet()
	: GameObject()
	,type(CharacterType::CNONE)
	,bulletSpeed(1)
{
	setPosition(sf::Vector2f(0,0));
}

Bullet::Bullet(const Bullet& src)
	: GameObject(src)
{
	this->type = src.type;
}

bool Bullet::update()
{
	if (this->type == CharacterType::PLAYER)
	{
		move(sf::Vector2f(0, -10));
	}
	else if (this->type == CharacterType::ENEMY)
	{
		move(sf::Vector2f(0, 10));
	}
	float dpos = this->getPosition().y;
	if (this->getPosition().y < -1 || this->getPosition().y > Game::GAME_HEIGHT + 1)
	{
		this->deactivate();
	}
	return false;
}

bool Bullet::init(CharacterType ctype,const GameContentManager& contentManager)
{
	GameObject::init(contentManager);
	this->type = ctype;
	setTexture(contentManager.getMainCharacterTexture());
	this->setScale(2, 2);
	if (this->type == CharacterType::ENEMY)
	{
		setTextureRect(sf::IntRect(287,106,16,5));
		rotate(-90);
	}
	else if (this->type == CharacterType::PLAYER)
	{
		setTextureRect(sf::IntRect(264, 106, 16, 5));
		rotate(90);
	}
	return true;
}

bool Bullet::collidesWith(const Enemy& enemy) const
{
	return getGlobalBounds().intersects(enemy.getGlobalBounds());
}

bool Bullet::collidesWith(const Boss& boss) const
{
	return getGlobalBounds().intersects(boss.getGlobalBounds());
}
 
CharacterType Bullet::getType()
{
	return this->type;
}
