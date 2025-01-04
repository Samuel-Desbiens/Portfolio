#include "stdafx.h"
#include "Movable.h"

Movable::Movable(sf::Vector2f pos, sf::Color color,float size, float _speed):
	Drawable(pos,color,size)
	,alive(true)
	,speed(_speed)
{

}

Movable::~Movable()
 {
	
}

float Movable::GetSize() const 
{
	return this->size;
}

bool Movable::GetIsAlive() const
{
	return alive;
}
float Movable::GetSpeed() const
{
	return this->speed;
}

void Movable::ChangeAlive() 
{
	if (alive) 
	{
		alive = false;
	}
	else
	{
		alive = true;
	}
}
void Movable::SetSpeed(float _speed)
{
	this->speed = _speed;
}

void Movable::Move(sf::Vector2f posAdd)
{
	this->SetPosition(this->GetPosition() + posAdd);
}