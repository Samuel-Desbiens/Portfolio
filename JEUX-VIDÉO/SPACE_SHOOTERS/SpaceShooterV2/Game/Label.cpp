#include "stdafx.h"
#include "Label.h"
#include <filesystem>
#include "View.h"
Label::Label(const std::string& fontName, unsigned int fontSize)
{
  SetFontName(fontName);
  SetFontSize(fontSize);  
}

void Label::Draw(sf::RenderWindow & window, const sf::Transform& t) const
{
  if (IsVisible())
  {
    sf::Transform ts=t;
    if (GetParent() != nullptr)
    {
      ts.translate(GetParent()->GetPosition());
    }
    window.draw(text, ts);
  }
    
}

void Label::SetPosition(const sf::Vector2f & newPos)
{
  text.setPosition(newPos);
}

sf::Vector2f Label::GetPosition() const
{
  sf::Vector2f retval = text.getPosition();
  if (GetParent() != nullptr)
  {
    retval += GetParent()->GetPosition();
  }
  return retval;

}

void Label::SetText(const std::string& newText)
{
  text.setString(newText);
}

const std::string Label::GetText() const
{
  return text.getString();
}

void Label::SetTextColor(const sf::Color & color)
{
  text.setFillColor(color);
}

void Label::SetFontName(const std::string & fontName)
{
  if (std::filesystem::exists(fontName) && font.loadFromFile(fontName))
  {
    text.setFont(font);    
  }
}

void Label::SetFontSize(const unsigned int fontSize)
{
  text.setCharacterSize(fontSize);
}

sf::FloatRect Label::GetBounds() const
{
  return text.getGlobalBounds();
}

void Label::CenterInScreen(const View& view)
{
float textW = GetBounds().width;
float textY = text.getPosition().y;
  SetPosition(sf::Vector2f((view.GetWidth()-textW)*0.5f, textY));
}

