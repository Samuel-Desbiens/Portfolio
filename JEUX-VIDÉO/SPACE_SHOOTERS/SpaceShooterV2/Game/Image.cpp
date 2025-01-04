#include "stdafx.h"
#include "Image.h"


Image::Image(const std::string& path, const unsigned int scaleX, const unsigned int scaleY)
  : Widget()
  , texturePath(path)
{
  bool b= texture.loadFromFile(path);
  if(b)
    sprite.setTexture(texture);

  sprite.setScale((float)scaleX, (float)scaleY);

  
}

Image::Image(const Image & src)
  : Widget(src)
  , texturePath(src.texturePath)
{
  texture.loadFromFile(src.texturePath);
  sprite.setTexture(texture);
  sprite.setScale(src.sprite.getScale());

}

void Image::Draw(sf::RenderWindow & window, const sf::Transform& t) const
{
  sf::Transform ts=t;
  if (GetParent() != nullptr)
  {
    ts.translate(GetParent()->GetPosition());
  }
  window.draw(sprite, ts);
}

void Image::SetPosition(const sf::Vector2f & newPos)
{
  sprite.setPosition(newPos);
}

sf::Vector2f Image::GetPosition() const
{
  sf::Vector2f retval = sprite.getPosition();
  if (GetParent() != nullptr)
  {
    retval += GetParent()->GetPosition();
  }
  return retval;

}

Image & Image::operator=(const Image & rhs)
{
  if (this != &rhs)
  {
    Widget::operator=(rhs);
    texturePath = rhs.texturePath;
    texture.loadFromFile(texturePath);
    sprite.setTexture(texture);
    sprite.setScale(rhs.sprite.getScale());
  }
  return *this;
}

unsigned int Image::GetWidth() const
{
  return texture.getSize().x;
}

unsigned int Image::GetHeight() const
{
  return texture.getSize().y;
}

