#include "stdafx.h"
#include "Bonus.h"

Bonus::Bonus(sf::Vector2f pos, sf::Color color) :
	Movable(pos, color, 5,1)
{

}

Bonus::~Bonus()
{

}

bool Bonus::Update()
{
	this->SetPosition(sf::Vector2f(GetPosition().x, GetPosition().y + GetSpeed()));
	return false;
}