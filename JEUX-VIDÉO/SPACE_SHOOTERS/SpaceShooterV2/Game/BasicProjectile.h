#pragma once
#include "Projectile.h"

class BasicProjectile :public Projectile
{
public:
	//Constructeur du basicProjectile.
	BasicProjectile(bool playerO, sf::Vector2f pos,sf::Color color);
	//D�constructeur du basicProjectile.
	~BasicProjectile();
protected:
private:
};

