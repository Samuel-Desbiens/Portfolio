#include "stdafx.h"
#include "BasicEnemy.h"

BasicEnemy::BasicEnemy(sf::Vector2f pos) :
	Enemy(pos, sf::Color::Green, 10, 30,30)
{

}

BasicEnemy::~BasicEnemy()
{

}