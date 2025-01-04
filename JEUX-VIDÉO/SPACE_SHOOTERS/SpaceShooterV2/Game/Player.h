#pragma once
#include "Character.h"
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include <SFML\System.hpp>
#include <Windows.h>
#include <string>
#include "stdafx.h"
#include "Constante.h"

class Player :
	public Character
{
public:
	//Constructeur du joueur.
	Player(sf::Vector2f pos);
	//Déconstructeur du joueur.
	~Player();
	//Mise a jour du joueur (peu de chose game s'occupe des input).
	bool Update();
	//Permet d'avoir la position du joueur avec le game.cpp.
	sf::Vector2f GetPos() const;
	//Permet de diminuer la vie du joueur a partir de game.cpp.
	void SetLife(int dmg);
	//Vérifie et bouge si il est possible le joueur a gauche.
	void MoveLeft();
	//Vérifie et bouge si il est possible le joueur a droite.
	void MoveRight();
protected:
	
private:
	
};

