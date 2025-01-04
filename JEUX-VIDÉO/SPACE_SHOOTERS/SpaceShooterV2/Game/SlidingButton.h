#pragma once
#include "Button.h"
class SlidingButton :
  public Button
{
public:
  SlidingButton(const std::string& fontName, unsigned int fontSize, const std::string& backgroundPath, unsigned int width, unsigned int height);
  ~SlidingButton();

  bool OnMouseMoved(const sf::Event::MouseMoveEvent& e) override;
  virtual sf::Vector2f GetPosition() const override;
private:
  bool mouseWasOver = false;
  sf::Vector2f offset;
};

