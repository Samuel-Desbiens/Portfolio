#pragma once
#include "AnimatedGameObject.h"
class Enemy :
    public AnimatedGameObject
{
public:
    Enemy();
    Enemy(const Enemy& src);
    virtual bool init(const GameContentManager& contentManager) override;
    bool update(float deltaT,const Inputs& inputs) override;
    void takeDmg();
    void destroyed();
    bool getIsDead();
    bool shouldAttack();
private:
    sf::Vector2f randSpawnPos();
    bool isDead;
    int vie;
    int cooldown;
    const int BASE_ATTACK_COOLDOWN = 5;
};

