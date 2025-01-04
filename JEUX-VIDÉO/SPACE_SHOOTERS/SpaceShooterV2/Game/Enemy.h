#pragma once
#include "Character.h"
class Enemy :
	public Character
{
public:
	//Constructeur d'enemy.
	Enemy(sf::Vector2f pos, sf::Color color, float speed, int life,float size);
	//Déconstructeur d'enemy.
	~Enemy();
	//Permet de mettre a jour l'enemy contient l'update du basic enemy (le reste redéfinisse la fonction dans leur class).
	virtual bool Update(sf::Vector2f playerPos);
	//Permet d'avoir la position de l'enemy avec un vector qui contient x et y.
	sf::Vector2f GetPos()const;
	//Permet d'avoir la vie de l'enemy a partir de game.cpp.
	int GetLife()const;
	//Permet de modifier la vie de l'enemy a partir de game.cpp.
	void Damaged(int dmg);
protected:
	
private:
	//Variable qui détermine la direction du basic enemy.
	bool goingLeft;
};

