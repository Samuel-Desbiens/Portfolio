#pragma once
#include "Drawable.h"
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include <SFML\System.hpp>
#include <Windows.h>
#include <string>
#include "stdafx.h"
class Movable :
	public Drawable
{
public:
protected:
	//Constructeur du movable.
	Movable(sf::Vector2f pos,sf::Color color,float size,float _speed);
	//Déconstructer du movable.
	~Movable();
	//Permet de retourner la grosseur du movable.
	float GetSize() const;
	//Permet de savoir si le mouvable est en vie ou non.
    bool GetIsAlive() const;
	//Permet de retourner la vitesse du movable.
	float GetSpeed() const;
	//Permet de modifier si le movable est en vie ou non.
	void ChangeAlive();
	//Permet de modifier la vitesse d'un movable.
	void SetSpeed(float _speed);
	//Permet de bouger un mouvable avec un vector additionner.
	void Move(sf::Vector2f posAdd);

private:
	//La grosseur du movable.
	float size;
	//La vitesse du mouvable.
	float speed;
	//si le movable est en vie.
	bool alive;
	
};

