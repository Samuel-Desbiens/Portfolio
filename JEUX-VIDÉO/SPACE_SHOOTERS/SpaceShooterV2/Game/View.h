#pragma once
#include "SFML/Graphics.hpp"
#include <map>
#include "UI.h"

# define VIEW_CLOSED 1
#define LAST_VIEW_EVENT VIEW_CLOSED
class View
{
protected:
  const unsigned int WIDTH;
  const unsigned int HEIGHT;
public:
  virtual ~View();  
  virtual void Update(float deltaT);
  virtual void Update();
  virtual void Draw(sf::RenderWindow& window) const;
  void HandleEvents(sf::Window& window);
  virtual void Show();
  void AddListener(std::function<void(View&)>  o, unsigned int event);
  virtual void SetVisible(bool visible);
  void CenterInScreen();
  virtual void Close();
  unsigned int GetWidth() const;
  unsigned int GetHeight() const;
protected:
  View(unsigned int w, unsigned int h);
  void NotifyObservers(unsigned int event);
  UI ui;
private:
  bool isVisible = false;
  std::multimap<unsigned int, std::function<void(View&)> >  observers;
};

