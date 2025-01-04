#pragma once
#include "Textbox.h"
class Button : public Textbox
{
public:
  Button(const std::string& fontName, unsigned int fontSize, const std::string& backgroundPath, unsigned int width, unsigned int height);
  bool OnTextEntered(const sf::Event::TextEvent& e) override;
  virtual sf::FloatRect GetBounds() const override;
private:
  
};
