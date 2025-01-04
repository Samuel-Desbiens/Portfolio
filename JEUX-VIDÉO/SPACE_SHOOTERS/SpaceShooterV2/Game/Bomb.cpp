#include "stdafx.h"
#include "Bomb.h"

Bomb::Bomb(bool playerO, sf::Vector2f pos) :
	Projectile(true, pos, sf::Color::Black, 7,25,1)
{

}

Bomb::~Bomb()
{

}

bool Bomb::Update()
{
	this->SetPosition(sf::Vector2f(GetPosition().x, GetPosition().y - GetSpeed()));
	return false;
}