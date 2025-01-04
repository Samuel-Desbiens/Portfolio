#pragma once
#include "Enemy.h"
class Facer :
	public Enemy
{
public:
	//Constructeur de l'enemy Facer.
	Facer(sf::Vector2f pos);
	//Deconstructeur de l'enemy Facer.
	~Facer();
	//Mets a jour avec son propre comportement l'enemy Facer.
	bool Update(sf::Vector2f playerPos);
protected:
private:
};

