#pragma once
#include "Widget.h"
class Image : public Widget
{
public:

  Image(const std::string& path, const unsigned int scaleX = 1, const unsigned int scaleY = 1);
  Image(const Image& src);
  virtual void Draw(sf::RenderWindow& window, const sf::Transform& t = sf::Transform::Identity) const override;
  virtual void SetPosition(const sf::Vector2f& newPos) override;
  virtual sf::Vector2f GetPosition() const override;
  Image& operator=(const Image& rhs);
  unsigned int GetWidth() const;
  unsigned int GetHeight() const;
protected:
  sf::Texture texture;
  sf::Sprite sprite;
  std::string texturePath;
};

