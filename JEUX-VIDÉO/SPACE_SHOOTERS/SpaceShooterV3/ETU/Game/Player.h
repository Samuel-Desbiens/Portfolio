#pragma once
#include "AnimatedGameObject.h"
#include "Enemy.h"
#include "Bullet.h"
class Bonus;
class Player :
    public AnimatedGameObject
{
public:
    Player();
    virtual bool init(const GameContentManager& contentManager) override;
    bool update(float deltaT, const Inputs& inputs) override;
    bool collidesWith(const Enemy& enemy) const;
    bool collidesWith(const Bullet& bullet) const;
    bool collidesWith(const Boss& boss) const;
    void takeDamage(int dmg);
    void addBonusTime();
    void addVie();
    int getBonusTime();
    int getVie();

private:
    int vie;
    int bonusTime;
    int alpha;

    const int HUD_SIZE = 12;
    const int BONUS_TIME_ADD =300;
    const int HEALTH_ADD = 10;
};

