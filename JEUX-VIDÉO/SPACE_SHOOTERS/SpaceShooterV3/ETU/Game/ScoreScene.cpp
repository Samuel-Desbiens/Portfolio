#include "stdafx.h"
#include "ScoreScene.h"

#include "game.h"


ScoreScene::ScoreScene()
	: Scene(SceneType::SCOREBOARD)
{

}

ScoreScene::~ScoreScene()
{
}

SceneType ScoreScene::update()
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

void ScoreScene::draw(sf::RenderWindow& window) const
{
    window.draw(score);
    window.draw(options);
}

bool ScoreScene::init()
{
    this->contentManager.init();

    score.setFont(contentManager.getMainFont());
    score.setString(std::to_string(result.gameSceneResult.score));
    score.setCharacterSize(36);
    score.setFillColor(sf::Color(255, 255, 255));
    score.setPosition(Game::GAME_WIDTH / 2.0f, Game::GAME_HEIGHT / 2.0f);


    options.setFont(contentManager.getMainFont());
    options.setString("Appuyer sur une touche pour commencer la partie \n Appuyer sur ESC pour quitter le jeu");
    options.setCharacterSize(12);
    options.setFillColor(sf::Color(255, 255, 255));
    options.setPosition(Game::GAME_WIDTH / 2.0f - options.getLocalBounds().width / 2.0f, Game::GAME_HEIGHT / 1.25f);

    if (!scoreMusic.openFromFile("Assets\\Music\\Scoreboard\\A-Night-Without-a-Lover.ogg"))
    {
        return false;
    }
    scoreMusic.setLoop(true);
    scoreMusic.play();
    return true;
}

bool ScoreScene::uninit()
{
    scoreMusic.pause();
    return false;
}

bool ScoreScene::handleEvents(sf::RenderWindow& window)
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
            this->nextScene = SceneType::NONE;
        }

    }
    return retval;

}