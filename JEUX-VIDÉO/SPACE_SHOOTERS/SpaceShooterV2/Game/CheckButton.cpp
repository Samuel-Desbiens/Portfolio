#include "stdafx.h"
#include "CheckButton.h"


CheckButton::CheckButton(unsigned int width, unsigned int height)
: Button("", 12, "Assets/Bitmaps/unchecked.jpg", width, height)
{
uncheckedTexture.loadFromFile("Assets/Bitmaps/unchecked.jpg");
checkedTexture.loadFromFile("Assets/Bitmaps/checked.jpg");
}


CheckButton::~CheckButton()
{
}

bool CheckButton::OnMouseButtonPressed(const sf::Event::MouseButtonEvent & e)
{
  bool retval = Button::OnMouseButtonPressed(e);
  if(retval)
  {
	isChecked= !isChecked;
    texture = isChecked?checkedTexture:uncheckedTexture;
  }

  return retval;
}
