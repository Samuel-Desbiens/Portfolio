#pragma once
#include "Movable.h"
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include <SFML\System.hpp>
#include <Windows.h>
#include <string>
#include "stdafx.h"
class Character :
	public Movable
{
public:
	//Constructeur des character de la partie.
	Character(sf::Vector2f pos, sf::Color color, float speed, int life, bool isplayer,float size);
	//Déconstructeur des character de la partie.
	~Character();
	//Mise a Jour pour faire le lien vers les mise a jour des personnage plus bas ne contient rien.
	virtual bool Update();
protected:
	//Renvoie si le character actuelle est le joueur.
	bool GetIsPlayer()const;
	//Renvoie le nombre de vie restante de personnage.
	int GetLifeLeft()const;
	//Diminue la vie du personnage avec le modifier.
	void SetLifeLeft(int modifier);
	//Permet de mettre si le character est le joueur ou non.
	void SetIsPlayer(bool _isPlayer);
	
private:
	//Si le character est le joueur.
	bool isPlayer;
	//Nombre de vie restante au character.
	int lifeLeft;
};

