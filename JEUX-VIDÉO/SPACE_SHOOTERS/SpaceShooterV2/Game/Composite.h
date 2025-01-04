#pragma once
#include "Widget.h"
#include <list>
#include <functional>
class Composite : public Widget
{
public:
  Composite();
  ~Composite();

  void Draw(sf::RenderWindow& window, const sf::Transform& t = sf::Transform::Identity) const override;
  void SetPosition(const sf::Vector2f& newPos) override;
  sf::Vector2f GetPosition() const override;
  bool HasFocus() const;

  void AddWidget(Widget& w);
  void RemoveWidget(Widget& w);

  // Events
  bool OnMouseMoved(const sf::Event::MouseMoveEvent& e)override;
  bool OnMouseButtonPressed(const sf::Event::MouseButtonEvent& e)override;
  bool OnMouseButtonReleased(const sf::Event::MouseButtonEvent& e)override;
  bool OnTextEntered(const sf::Event::TextEvent& e)override;
protected:
  std::list<std::reference_wrapper<Widget> > children;
private:
  sf::Vector2f position;
};

