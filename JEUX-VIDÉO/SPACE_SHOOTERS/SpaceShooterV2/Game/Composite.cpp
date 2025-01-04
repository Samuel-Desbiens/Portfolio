#include "stdafx.h"
#include "Composite.h"


Composite::Composite()
{
}


Composite::~Composite()
{
}

void Composite::Draw(sf::RenderWindow & window, const sf::Transform& t ) const
{
  for (const Widget& widget : children)
  {
    widget.Draw(window,t);
  }
}

void Composite::SetPosition(const sf::Vector2f & newPos)
{
  position = newPos;
}

sf::Vector2f  Composite::GetPosition() const
{
  sf::Vector2f retval = position;
  if (GetParent() != nullptr)
  {
    retval += GetParent()->GetPosition();
  }
  return retval;
}

bool Composite::HasFocus() const
{
  bool retval = false;
  for (const Widget& widget : children)
  {
    retval = retval || widget.HasFocus();
  }
  return retval;
}

void Composite::AddWidget(Widget & w)
{
  w.SetParent(this);
  children.push_back(w);  
}
void Composite::RemoveWidget(Widget & w)
{
  w.SetParent(nullptr);
  std::list<std::reference_wrapper<Widget> >::iterator it = children.begin();
  while (it != children.end())
  {
    if (&(it->get()) == &w)
    {
      break;
    }
    it++;
  }
  if(children.end() != it)
    children.erase(it);

}

bool Composite::OnMouseMoved(const sf::Event::MouseMoveEvent & /*e*/)
{
  return false;
}

bool Composite::OnMouseButtonPressed(const sf::Event::MouseButtonEvent & /*e*/)
{
  return false;
}

bool Composite::OnMouseButtonReleased(const sf::Event::MouseButtonEvent & /*e*/)
{
  return false;
}

bool Composite::OnTextEntered(const sf::Event::TextEvent & /*e*/)
{
  return false;
}
