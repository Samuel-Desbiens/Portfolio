#pragma once
#include "Movable.h"
class Bonus :
	public Movable
{
public:
	//Constructeur des bonus.
	Bonus(sf::Vector2f pos, sf::Color color);
	//déconstructeur des bonus.
	~Bonus();
	// Mise a jour commune de tout les bonus.
	bool Update();
protected:
private:
	
};

