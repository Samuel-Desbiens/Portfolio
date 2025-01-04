#pragma once
#include "Projectile.h"
class Bomb :
	public Projectile
{
public:
	//Constructeur du projectile bomb.
	Bomb(bool playerO, sf::Vector2f pos);
	//Déconstruteur du projectile bomb.
	~Bomb();
	//Mise a jour de la bomb lorsque lancé.
	bool Update();
protected:
private:
	
};

