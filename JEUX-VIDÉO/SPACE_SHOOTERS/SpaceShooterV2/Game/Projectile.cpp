#include "stdafx.h"
#include "Projectile.h"

Projectile::Projectile(bool playerO, sf::Vector2f pos,sf::Color color, int projSpeed,int damage,float size) :
	Movable(pos, color, projSpeed,size),playerOwned(playerO),damage(damage)
{

}

Projectile::~Projectile()
{

}

bool Projectile::GetPlayerOwned() const
{
	return playerOwned;
}

sf::Vector2f Projectile::GetPos() const
{
	return GetPosition();
}

int Projectile::GetDMG() const
{
	return damage;
}

bool Projectile::Update()
{
	if (GetPlayerOwned())
	{
		this->SetPosition(sf::Vector2f(GetPosition().x, GetPosition().y - GetSpeed()));
	}
	else
	{
		this->SetPosition(sf::Vector2f(GetPosition().x, GetPosition().y + GetSpeed()/2));
	}
	return false;
}