#pragma once
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include <SFML\System.hpp>
#include <Windows.h>
#include <string>
#include "stdafx.h"
class Drawable
{
public:
	//Fonction permettante de voir dans la fenetre sfml les shape.
	virtual void Draw(sf::RenderWindow &window);
	//Fonction qui permet de savoir si une shape en touche une autre.
	bool Intersects(Drawable* ot);

protected:
	//Constructeur des chose qui doivent etre visible.
	Drawable(sf::Vector2f pos,sf::Color color,float size);
	//Déconstructeur des drawable.
	~Drawable();
	//Retourne la shape du drawable actuel.
	sf::Shape* GetShape() const;
	//Retourne la position x et y avec un vector2f.
	sf::Vector2f GetPosition() const;
	//Retoune la couleur de la shape.
	sf::Color GetColor() const;
	//Retourne la hit box du drawable.
	sf::FloatRect GetBoundingBox() const;
	//Permet de mettre la position du drawable.
	void SetPosition(sf::Vector2f nextPos);
private:
	//Forme qui represente le drawable.
	sf::Shape *shape;
	//Vector representant la position x et y du drawable.
	sf::Vector2f position;
};

