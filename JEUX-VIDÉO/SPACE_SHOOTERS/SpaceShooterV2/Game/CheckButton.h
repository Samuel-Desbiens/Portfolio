#pragma once
#include "Button.h"
class CheckButton :
  public Button
{
public:
  CheckButton(unsigned int width, unsigned int height);
  ~CheckButton();

  bool OnMouseButtonPressed(const sf::Event::MouseButtonEvent& e) override;
private:
  sf::Texture checkedTexture;
  sf::Texture uncheckedTexture;
  bool isChecked = false;
};

