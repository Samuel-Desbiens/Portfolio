#pragma once
#include "SFML/Graphics.hpp"
#include <string>
#include "Widget.h"
class View;

class Label : public Widget
{
public:
  Label(const std::string& fontName="", unsigned int fontSize=0);
  void Draw(sf::RenderWindow& window, const sf::Transform& t = sf::Transform::Identity) const override;
  void SetPosition(const sf::Vector2f& newPos) override;
  sf::Vector2f GetPosition() const override;
  virtual void SetText(const std::string& text);
  virtual const std::string GetText() const;
  
  void SetTextColor(const sf::Color& color);
  void SetFontName(const std::string& fontName);
  void SetFontSize(const unsigned int fontSize);
  virtual sf::FloatRect GetBounds() const;
  void CenterInScreen(const View& view);
protected:
  sf::Font font;
  sf::Text text;
};
