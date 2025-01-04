#include "stdafx.h"
#include "BasicProjectile.h"

BasicProjectile::BasicProjectile(bool playerO,sf::Vector2f pos,sf::Color color):
	Projectile(playerO,pos,color,10,10,10)
{

}

BasicProjectile::~BasicProjectile()
{

}