#include "stdafx.h"
#include "Facer.h"

Facer::Facer(sf::Vector2f pos)
	:Enemy(pos, sf::Color::Red, 2, 200, 45)
{

}

Facer::~Facer()
{

}

bool Facer::Update(sf::Vector2f playerPos)
{
	if (this->GetPosition().y < 450 && this->GetPosition().y + this->GetSpeed() < 700)
	{
		this->SetPosition(sf::Vector2f(this->GetPosition().x, this->GetPosition().y + this->GetSpeed()));
	}
	else if (this->GetPosition().x < playerPos.x && this->GetPosition().x + this->GetSpeed() < 1024)
	{
		this->SetPosition(sf::Vector2f(this->GetPosition().x + this->GetSpeed(), this->GetPosition().y));
	}
	else if(this->GetPosition().x - this->GetSpeed() > 0)
	{
		this->SetPosition(sf::Vector2f(this->GetPosition().x - this->GetSpeed(), this->GetPosition().y));
	}
	return false;
}