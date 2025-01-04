#include "stdafx.h"
#include "Textbox.h"
#include <filesystem>
#include <sstream>

Textbox::Textbox(const std::string& fontName, unsigned int fontSize, unsigned int nbCharMax, const std::string& backgroundPath)
  : Label(fontName, fontSize)
  , numCharMax(nbCharMax)
{
  std::string s="";
  for (unsigned int i = 0; i < nbCharMax; i++)
  {
    s+= (char)('H');
  }
  SetText(s);
  boundingRect = text.getGlobalBounds();
  //text.setOutlineThickness(2.0f);
  //text.setOutlineColor(sf::Color::White);
  text.setFillColor(sf::Color(255,255,255,64));
  
  SetText("");
  if(std::filesystem::exists(backgroundPath))
    texture.loadFromFile(backgroundPath);
}

void Textbox::Draw(sf::RenderWindow & window, const sf::Transform& t ) const
{
  if (IsVisible())
  {
    sf::RectangleShape boundingShape;
    boundingShape.setSize(sf::Vector2f(boundingRect.width, boundingRect.height));
    boundingShape.setOutlineThickness(2);
    if(texture.getSize().x == 0)
      boundingShape.setFillColor(sf::Color(0, 0, 0, 0));
    else
      boundingShape.setFillColor(sf::Color(IsEnabled() ? 255 : 64, IsEnabled() ? 255 : 64, IsEnabled() ? 255 : 64, IsEnabled() ? 255 : 64));
    boundingShape.setOutlineColor(sf::Color(255, 255, 255, HasFocus() ? 255 : 64));
    boundingShape.setPosition(sf::Vector2f(GetPosition().x+boundingRect.left, GetPosition().y+boundingRect.top));
    boundingShape.setTexture(&texture);
    window.draw(boundingShape,t);
    Label::Draw(window,t);
  }  
}

bool Textbox::CanHaveFocus() const
{
  return true;
}


bool Textbox::OnMouseMoved(const sf::Event::MouseMoveEvent & e)
{
  bool retval = IsEnabled() && boundingRect.contains((float)e.x-GetPosition().x, (float)e.y-GetPosition().y);
  
  return retval;
}

bool Textbox::OnMouseButtonPressed(const sf::Event::MouseButtonEvent & e)
{
sf::Vector2f pos = GetPosition();
  hasFocus = IsEnabled() && boundingRect.contains((float)e.x -pos.x, (float)e.y - pos.y);
  
  if(HasFocus())
    NotifyObservers(sf::Event::MouseButtonPressed);
  return hasFocus;
}

bool Textbox::OnMouseButtonReleased(const sf::Event::MouseButtonEvent & e)
{
  bool previousFocus= HasFocus();
  bool retval = IsEnabled() && boundingRect.contains((float)e.x - GetPosition().x, (float)e.y - GetPosition().y);
  if (retval == previousFocus && previousFocus==true)
    NotifyObservers(sf::Event::MouseButtonReleased);
  return retval;
}

bool Textbox::OnTextEntered(const sf::Event::TextEvent & e)
{
  if (IsEnabled() && HasFocus())
  {
    switch (e.unicode)
    {
    case 0x8:
      if(GetText().length() > 0)
      {
        std::string t = GetText();
        SetText(t.erase(GetText().length()-1,1));
        break;
      }
      default:
      {
        char newChar = (char)e.unicode;
        if (newChar >= ' ')
        {
          // Vous pourriez faire beaucoup mieux ici pour gérer les caractères  non affichables
          if (GetText().length() < numCharMax)
          {
            Textbox::SetText(Textbox::GetText() + newChar);
            SetText(GetText());
          }
          
        }
        
      }
      break;
    }
    NotifyObservers(sf::Event::TextEntered);
  }

  return HasFocus();
}
