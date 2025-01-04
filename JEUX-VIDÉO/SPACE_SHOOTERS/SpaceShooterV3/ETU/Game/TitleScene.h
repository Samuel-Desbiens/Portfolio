#pragma once
#include "Scene.h"
#include "TitleScreenContentManager.h"

class TitleScene :
    public Scene
{
    static const std::string MENU_OPTION_TEXT;
public:
  // Héritées via Scene
  TitleScene();
  ~TitleScene();
  virtual SceneType update() override;
  virtual void draw(sf::RenderWindow& window) const override;
  virtual bool init() override;
  virtual bool uninit() override;
  virtual bool handleEvents(sf::RenderWindow& window) override;
private:
  sf::Texture menuImageTexture;
  sf::Sprite menuImage;
  sf::Text menuOption;

  sf::Music menuMusic;

  SceneType nextScene;
  TitleScreenContentManager contentManager;
};

