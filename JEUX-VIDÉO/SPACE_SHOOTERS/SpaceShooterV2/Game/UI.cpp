#include "stdafx.h"
#include "UI.h"


UI::UI(unsigned int posX, unsigned int posY)
{
  SetPosition(sf::Vector2f((float)posX, (float)posY));
}


void UI::HandleEvents(sf::Window & window)
{
  sf::Event event;
  //On passe l'�v�nement en r�f�rence et celui-ci est charg� du dernier �v�nement re�u!
  while (window.pollEvent(event))
  {
    //x sur la fen�tre
    if (event.type == sf::Event::Closed)
    {
      window.close();
    }
    else 
      DispatchEvent(event);    
  }
}


void UI::DispatchEvent(sf::Event event)
{
  if (event.type == sf::Event::MouseMoved)
  {
    for (Widget& w : children)
    {
      w.OnMouseMoved(event.mouseMove);
    }
  }
  else if (event.type == sf::Event::MouseButtonPressed)
  {
    for (Widget& w : children)
    {
      w.OnMouseButtonPressed(event.mouseButton);
    }
  }
  else if (event.type == sf::Event::MouseButtonReleased)
  {
    for (Widget& w : children)
    {
      w.OnMouseButtonReleased(event.mouseButton);
    }
  }
  else if (event.type == sf::Event::TextEntered)
  {
    for (Widget& w : children)
    {
      w.OnTextEntered(event.text);
    }
  }
}

