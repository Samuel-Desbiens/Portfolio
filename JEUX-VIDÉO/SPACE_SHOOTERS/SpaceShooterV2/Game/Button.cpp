#include "stdafx.h"
#include "Button.h"



Button::Button(const std::string& fontName, unsigned int fontSize, const std::string& backgroundPath, unsigned int width, unsigned int height)
  : Textbox(fontName, fontSize, 0, backgroundPath)
{
  
  
  
  boundingRect = sf::FloatRect(0.0f,0.0f, (float)width, (float)height);

  
}


bool Button::OnTextEntered(const sf::Event::TextEvent & e)
{
  if (HasFocus())
  {
    // Si appuyé sur ENTER on génère un click
    if (e.unicode == 13)
    {
      NotifyObservers(sf::Event::MouseButtonPressed);
      return true;
    }
  }
  
  return false;
}

sf::FloatRect Button::GetBounds() const
{
  return boundingRect;
}


