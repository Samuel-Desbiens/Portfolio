#pragma once
#include "SFML/Graphics.hpp"
#include <functional>
#include <list>

class Widget
{
public:
  virtual ~Widget()=0;
  virtual void Draw(sf::RenderWindow& window, const sf::Transform& t=sf::Transform::Identity) const = 0;
  virtual void SetPosition(const sf::Vector2f& newPos)=0;
  virtual sf::Vector2f GetPosition() const=0;
  virtual bool HasFocus() const;
  virtual bool CanHaveFocus() const;
  virtual void SetFocus(bool newFocus);
  virtual bool IsVisible() const;
  virtual bool IsEnabled() const;
  virtual void SetEnabled( bool enabled ) ;
  
  void AddListener(std::function<void(const Widget&)>, sf::Event::EventType event);
  void NotifyObservers(sf::Event::EventType event);

  

  // Events
  virtual bool OnMouseMoved(const sf::Event::MouseMoveEvent& e);
  virtual bool OnMouseButtonPressed(const sf::Event::MouseButtonEvent& e);
  virtual bool OnMouseButtonReleased(const sf::Event::MouseButtonEvent& e);
  virtual bool OnTextEntered(const sf::Event::TextEvent& e);
  
  // Pour l'implémentation composite  
  void SetParent(Widget* parent);
  const Widget* GetParent() const;
  virtual void AddWidget(Widget& w);
  virtual void RemoveWidget(Widget& w);

protected:
  Widget();
  bool hasFocus = false;
private:
  Widget* parent;
  bool isVisible= true;  
  bool isEnabled= true;

  std::multimap<sf::Event::EventType, std::function<void(const Widget&)> >  observers;
};

