#pragma once
#include "AnimatedGameObject.h"
class Boss :
    public AnimatedGameObject
{
public:
    Boss();
    virtual bool init(const GameContentManager& contentManager) override;
    bool bUpdate(float deltaT, const Inputs& inputs,sf::Vector2f playerPos);
    void takeDmg();
    void destroyed();
    bool getIsDead();
    bool shouldAttack();
private:
    int vie;
    bool isDead;

    int cooldown;
    const int BASE_ATTACK_COOLDOWN = 5;
};

