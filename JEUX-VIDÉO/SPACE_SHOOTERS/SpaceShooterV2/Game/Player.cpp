#include "stdafx.h"
#include "Player.h"

class Game;

Player::Player(sf::Vector2f pos)
	: Character(pos,sf::Color::Green,5,PLAYER_LIFE_AT_START,true,30)
{
}

Player::~Player()
{

}

bool Player::Update()
{
	if (this->GetLifeLeft() <= 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

sf::Vector2f Player::GetPos() const
{
	return GetPosition();
}

void Player::SetLife(int dmg)
{
	this->SetLifeLeft(dmg);
}

void Player::MoveLeft()
{
	if (GetPosition().x - 1 > 0)
	{
		SetPosition(sf::Vector2f(GetPosition().x - 1, GetPosition().y));
	}
}

void Player::MoveRight()
{
	if (GetPosition().x - 1 < 1000)
	{
		SetPosition(sf::Vector2f(GetPosition().x + 1, GetPosition().y));
	}
}