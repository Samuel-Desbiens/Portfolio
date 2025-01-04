#include "stdafx.h"
#include "SlidingButton.h"


SlidingButton::SlidingButton(const std::string& fontName, unsigned int fontSize, const std::string& backgroundPath, unsigned int width, unsigned int height)
:Button(fontName, fontSize,backgroundPath, width, height)
, offset(0,0)
{
}


SlidingButton::~SlidingButton()
{
}

bool SlidingButton::OnMouseMoved(const sf::Event::MouseMoveEvent & e)
{
  bool retval = Button::OnMouseMoved(e);

  bool isVerticallyOver = ((float)e.y - GetPosition().y > 0) && ((float)e.y - GetPosition().y < boundingRect.height);

  if (mouseWasOver == true && isVerticallyOver == false)
  {
    // Enlever le décalage
    offset = sf::Vector2f(0,0);
  }
  else if(isVerticallyOver)
  {
    mouseWasOver = isVerticallyOver;
    offset = sf::Vector2f(25,0);
  }
  
  return retval;
}

sf::Vector2f SlidingButton::GetPosition() const
{
  return Button::GetPosition() + offset;
}
