#pragma once
#include "Projectile.h"
class Bomb :
	public Projectile
{
public:
	//Constructeur du projectile bomb.
	Bomb(bool playerO, sf::Vector2f pos);
	//D�construteur du projectile bomb.
	~Bomb();
	//Mise a jour de la bomb lorsque lanc�.
	bool Update();
protected:
private:
	
};

