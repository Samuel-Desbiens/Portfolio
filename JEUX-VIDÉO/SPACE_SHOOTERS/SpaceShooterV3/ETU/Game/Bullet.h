#pragma once
#include "GameObject.h"
#include "charactertype.h"
#include "Enemy.h"
#include "Boss.h"
class Bullet :
    public GameObject
{
public:
    Bullet();
    Bullet(const Bullet& src);
    virtual bool init(CharacterType ctype,const GameContentManager& contentManager);
    bool update();
    CharacterType getType();
    bool collidesWith(const Enemy& enemy) const;
    bool collidesWith(const Boss& boss) const;


private:
    CharacterType type;
    float bulletSpeed;
    Bullet& operator=(const Bullet& b);
};

