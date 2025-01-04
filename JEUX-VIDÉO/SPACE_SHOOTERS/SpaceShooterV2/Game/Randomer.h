#pragma once
#include "Enemy.h"
#include "random.h"
class Randomer :
	public Enemy
{
public:
	//Constructeur de l'enemy Randomer.
	Randomer(sf::Vector2f pos);
	//Destructeur de l'enemy Randomer.
	~Randomer();
	//Mise a jour de l'enemy Randomer.
	bool Update(sf::Vector2f playerPos);

protected:
private:
	//Variable permettant au évenement aléatoire dans la class.
	Random rand;
	//Si le Randomer a un point comme objectif.
	bool hasPoint;
	//La destination du Randomer.
	sf::Vector2f destination;
};

