#include "stdafx.h"
#include "Enemy.h"

Enemy::Enemy(sf::Vector2f pos, sf::Color color, float speed, int life, float size)
	:Character(pos, color, speed, life, false, size)
	, goingLeft(false)
{

}

Enemy::~Enemy() 
{

}

sf::Vector2f Enemy::GetPos() const
{
	return GetPosition();
}

int Enemy::GetLife() const
{
	return GetLifeLeft();
}

void Enemy::Damaged(int dmg)
{
	SetLifeLeft(dmg);
}

bool Enemy::Update(sf::Vector2f playerPos)
{
	if (this->GetPosition().y < 100 && this->GetPosition().y + this->GetSpeed() < 700)
	{
		this->SetPosition(sf::Vector2f(this->GetPosition().x, this->GetPosition().y + this->GetSpeed()));
		if (this->GetPosition().y >= 100)
		{
			goingLeft = true;
		}
	}
	else if (goingLeft)
	{
		this->SetPosition(sf::Vector2f(this->GetPosition().x - this->GetSpeed(), this->GetPosition().y ));
		if (this->GetPosition().x <= 0)
		{
			goingLeft = false;
		}
	}
	else
	{
		this->SetPosition(sf::Vector2f(this->GetPosition().x + this->GetSpeed(), this->GetPosition().y));
		if (this->GetPosition().x >= 1000)
		{
			goingLeft = true;
		}
	}
	
	return false;
}