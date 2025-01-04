#pragma once
#include <functional>
#include <list>
#include "Composite.h"
#include <SFML/Graphics.hpp>
#define UI_ESCAPE_PRESSED 1
class UI final : public Composite
{
public:
  UI(unsigned int posX=0, unsigned int posY=0);
  void HandleEvents(sf::Window& window);

private:
  void DispatchEvent(sf::Event event);
};

