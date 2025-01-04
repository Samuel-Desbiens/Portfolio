#pragma once
#include "Label.h"


class Textbox : public Label
{
public:
  Textbox(const std::string& fontName, unsigned int fontSize, unsigned int nbCharMax, const std::string& backgroundPath="");
  void Draw(sf::RenderWindow& window, const sf::Transform& t = sf::Transform::Identity) const override;
  virtual bool CanHaveFocus() const override;
  // Events
  bool OnMouseMoved(const sf::Event::MouseMoveEvent& e) override;
  bool OnMouseButtonPressed(const sf::Event::MouseButtonEvent& e) override;
  bool OnMouseButtonReleased(const sf::Event::MouseButtonEvent& e) override;
  bool OnTextEntered(const sf::Event::TextEvent& e) override;
protected:
  
  sf::FloatRect boundingRect;
  sf::Texture texture;

  unsigned int numCharMax;
  
};