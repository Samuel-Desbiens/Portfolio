#include "stdafx.h"
#include "Randomer.h"

Randomer::Randomer(sf::Vector2f pos)
	:Enemy(pos,sf::Color::Blue,1,50,20)
	,hasPoint(false)
	,destination(sf::Vector2f(0,0))
{
	Random rand;
}

Randomer::~Randomer()
{

}

bool Randomer::Update(sf::Vector2f playerPos)
{
	if (!hasPoint)
	{
		float x = rand.Next(0, 1000);
		float y = rand.Next(0, 780);
		destination = sf::Vector2f(x, y);
		hasPoint = true;
	}
	else
	{
		if (this->GetPosition() == destination )
		{
			hasPoint = false;
		}
		else 
		{
			sf::Vector2f nextMove = sf::Vector2f(0, 0);
			if (this->GetPosition().x < destination.x)
			{
				nextMove += sf::Vector2f(GetSpeed(), 0);
			}
			else if ((this->GetPosition().x > destination.x))
			{
				nextMove += sf::Vector2f(-GetSpeed(), 0);
			}
			if ((this->GetPosition().y < destination.y))
			{
				nextMove += sf::Vector2f(0, GetSpeed());
			}
			else if ((this->GetPosition().y > destination.y))
			{
				nextMove += sf::Vector2f(0, -GetSpeed());
			}
			SetPosition(this->GetPosition() +nextMove);
		}
	}
	return false;
}