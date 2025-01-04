#include "stdafx.h"
#include "Widget.h"
#include "NotImplementedException.h"

bool Widget::OnMouseMoved(const sf::Event::MouseMoveEvent & /*e*/)
{
  return false;
}

bool Widget::OnMouseButtonPressed(const sf::Event::MouseButtonEvent & /*e*/)
{
  return false;
}

bool Widget::OnMouseButtonReleased(const sf::Event::MouseButtonEvent & /*e*/)
{
  return false;
}

bool Widget::OnTextEntered(const sf::Event::TextEvent & /*e*/)
{
  return false;
}

Widget::Widget()
  : parent(nullptr)
{
}

void Widget::SetParent(Widget * newParent)
{
  parent = newParent;
}

const Widget * Widget::GetParent() const
{
  return parent;
}


Widget::~Widget()
{
}

bool Widget::HasFocus() const
{
  return hasFocus && IsEnabled();
}

bool Widget::CanHaveFocus() const
{
  return false;
}

void Widget::SetFocus(bool newFocus)
{
  if(CanHaveFocus())
    hasFocus=newFocus;
}

bool Widget::IsVisible() const
{
  return isVisible;
}

bool Widget::IsEnabled() const
{
  return isEnabled;
}

void Widget::SetEnabled(bool enabled)
{
  isEnabled = enabled;
}

void Widget::AddListener(std::function<void(const Widget&)>  o, sf::Event::EventType e)
{
  auto t = std::make_pair(e, o);
  observers.insert(t);
}
void Widget::NotifyObservers(sf::Event::EventType event)
{
  std::multimap<sf::Event::EventType, std::function<void(const Widget&)> >::iterator it = observers.equal_range(event).first;
  while (it != observers.equal_range(event).second)
  {
    it->second(*this);
    it++;
  }
}

void Widget::AddWidget(Widget & /*w*/)
{
  // Ne devrait JAMAIS être appelée
  throw NotImplementedException();
}

void Widget::RemoveWidget(Widget & /*w*/)
{
  // Ne devrait JAMAIS être appelée
  throw NotImplementedException();
}
