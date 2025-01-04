#pragma once
#include "Scene.h"
#include "ScoreContentManager.h"
class ScoreScene :
    public Scene
{
public:
    ScoreScene();
    ~ScoreScene();
    virtual SceneType update() override;
    virtual void draw(sf::RenderWindow& window) const override;
    virtual bool init() override;
    virtual bool uninit() override;
    virtual bool handleEvents(sf::RenderWindow& window) override;
private:
    sf::Text score;
    sf::Text options;

    sf::Music scoreMusic;

    SceneType nextScene;
    ScoreContentManager contentManager;
};

