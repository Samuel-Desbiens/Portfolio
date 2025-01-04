#include "stdafx.h"
#include "view.h"
#include "game.h"

View::View(unsigned int w, unsigned int h)
  : WIDTH(w), HEIGHT(h)
{
}


View::~View()
{
}

void View::Update(float /*deltaT*/)
{
}

void View::Update()
{
}

void View::Draw(sf::RenderWindow& window) const
{
  if (isVisible)
  {
    ui.Draw(window);
  }
}

void View::CenterInScreen()
{
  ui.SetPosition(sf::Vector2f((float)(Game::WIDTH - WIDTH) / 2, (float)(Game::HEIGHT - HEIGHT) / 2));
}

void View::Close()
{
  SetVisible(false);
  NotifyObservers(VIEW_CLOSED);
}

void View::HandleEvents(sf::Window& window)
{
  if(isVisible)
    ui.HandleEvents(window);
}

void View::Show()
{
  isVisible = true;
  SetVisible(isVisible);
}

void View::AddListener(std::function<void(View&)>  o, unsigned int event)
{
  auto t = std::make_pair(event, o);
  observers.insert(t);
}

void View::NotifyObservers(unsigned int event)
{
  std::multimap<unsigned int, std::function<void(View&)> >::iterator it = observers.equal_range(event).first;
  while (it != observers.equal_range(event).second)
  {
    it->second(*this);
    it++;
  }
}

void View::SetVisible(bool visible)
{
  //if (false == visible)
  //  NotifyObservers(VIEW_CLOSED);
  isVisible = visible;
}

unsigned int View::GetWidth() const 
{ 
  return WIDTH; 
}

unsigned int View::GetHeight() const
{
  return HEIGHT;
}