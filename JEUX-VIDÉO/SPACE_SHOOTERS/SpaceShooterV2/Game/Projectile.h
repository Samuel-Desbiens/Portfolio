#pragma once
#include "Movable.h"
class Projectile :
	public Movable
{
public:
	//Constructeur des projectiles.
	Projectile(bool playerO,sf::Vector2f pos,sf::Color color,int projSpeed,int damage,float size);
	//Déconstructeur des projectiles.
	~Projectile();
	//Mise a jour des projectile selon a qui ils appartiennent.
	bool Update();
	//Permet de recevoir a qui appartient le projectile (joueur ou enemy).
	bool GetPlayerOwned()const;
	//Permet d'avaoir acces a la position de l'enemy a partir de game.cpp.
	sf::Vector2f GetPos()const;
	//Permet de savoir le nombre de dommage causer par un certain projectile.
	int GetDMG()const;
protected:
	//Dommage que le projectile fait en touchant le joueur.
	int damage;
private:
	//Appartenance au joueur ou non.
	bool playerOwned;
};

