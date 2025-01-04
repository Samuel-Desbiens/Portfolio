#include "stdafx.h"
#include "Character.h"

class Game;

Character::Character(sf::Vector2f pos, sf::Color color, float speed,int life,bool isplayer,float size) :
	Movable(pos, color,size, speed)
	,lifeLeft(life)
	,isPlayer(isplayer)
{

}

Character::~Character()
{
}
bool Character::GetIsPlayer()const
{
	return this->isPlayer;
}

int Character::GetLifeLeft()const
{
	return this->lifeLeft;
}

void Character::SetIsPlayer(bool _isPlayer)
{
	this->isPlayer = _isPlayer;
}

void Character::SetLifeLeft(int modifier) 
{
	this->lifeLeft -= modifier;
}

bool Character::Update()
{
	return false;
}
