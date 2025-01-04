#include "stdafx.h"
#include "TitleScene.h"

#include "game.h"

const std::string TitleScene::MENU_OPTION_TEXT = "Appuyer sur une touche pour commencer la partie \n Appuyer sur ESC pour quitter le jeu";

TitleScene::TitleScene()
  : Scene(SceneType::TITLE_SCENE)
{

}

TitleScene::~TitleScene()
{

}
SceneType TitleScene::update()
{
    if (nextScene == NULL || nextScene != this->getSceneType())
    {
        SceneType retval = nextScene;
        nextScene = this->getSceneType();
        return retval;
    }
    else
    {
        return nextScene = this->getSceneType();
    }
}

void TitleScene::draw(sf::RenderWindow& window) const
{
  window.draw(menuImage);
  window.draw(menuOption);
}

bool TitleScene::init()
{
  this->contentManager.init();
  menuImage.setTexture(contentManager.getTitleScreenTexture());
  menuImage.setOrigin(menuImage.getTexture()->getSize().x / 2.0f, menuImage.getTexture()->getSize().y / 2.0f);
  menuImage.setPosition(Game::GAME_WIDTH / 2.0f, Game::GAME_HEIGHT / 2.0f);

  menuOption.setFont(contentManager.getMainFont());
  menuOption.setString(MENU_OPTION_TEXT);
  menuOption.setCharacterSize(12);
  menuOption.setFillColor(sf::Color(255, 255, 255));
  menuOption.setPosition(Game::GAME_WIDTH / 2.0f - menuOption.getLocalBounds().width / 2.0f, Game::GAME_HEIGHT / 1.25f - menuOption.getLocalBounds().height / 2.0f);

  if (!menuMusic.openFromFile("Assets\\Music\\Title\\intro_theme.ogg"))
  {
      return false;
  }
  menuMusic.setLoop(true);
  menuMusic.play();
  return true;
}

bool TitleScene::uninit()
{
    menuMusic.pause();
    return false;
}

bool TitleScene::handleEvents(sf::RenderWindow& window)
{
  bool retval = false;
  sf::Event event;
  while (window.pollEvent(event))
  {
    //x sur la fenêtre
    if (event.type == sf::Event::Closed || sf::Keyboard::isKeyPressed(sf::Keyboard::Escape))
    {
      window.close();
      retval = true;
    }
    if (event.type == sf::Event::KeyPressed || event.type == sf::Event::JoystickButtonPressed)
    {
        this->nextScene = SceneType::GAME;
    }
    
  }
  return retval;

}
