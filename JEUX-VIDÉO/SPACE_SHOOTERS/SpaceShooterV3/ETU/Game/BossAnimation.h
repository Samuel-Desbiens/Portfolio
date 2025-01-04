#pragma once
#include "CyclicAnimation.h"
class BossAnimation :
    public CyclicAnimation
{
public:
    BossAnimation(sf::Sprite& bossSprite);
    virtual bool init(const GameContentManager& contentManager);
};