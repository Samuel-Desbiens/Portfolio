#include "stdafx.h"
#include "Drawable.h"

class Projectile;

Drawable::Drawable(sf::Vector2f pos, sf::Color color,float size) :
	position(pos)
{
	shape = new sf::RectangleShape(sf::Vector2f(size,size));
	shape->setFillColor(color);
	shape->setPosition(pos);
}

Drawable::~Drawable() 
{
}

sf::Shape* Drawable::GetShape() const 
{
	return this->shape;
}

sf::Vector2f Drawable::GetPosition() const
{
	return this->position;
}

sf::Color Drawable::GetColor() const
{
	return this->shape->getFillColor();
}

sf::FloatRect Drawable::GetBoundingBox() const
{
	return this->shape->getGlobalBounds();
}

void Drawable::SetPosition(sf::Vector2f nextPos)
{
	this->position = nextPos;
}

void Drawable::Draw(sf::RenderWindow &window)
{
	this->shape->setPosition(this->position);
	window.draw(*shape);
}

bool Drawable::Intersects(Drawable* ot) 
{
	sf::FloatRect r = ot->GetBoundingBox();
	r.left = ot->position.x;
	r.top = ot->position.y;
	return GetBoundingBox().intersects(r);
}

