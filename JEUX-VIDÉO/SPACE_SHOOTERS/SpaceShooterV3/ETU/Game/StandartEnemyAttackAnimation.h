#pragma once
#include "CyclicAnimation.h"
class StandartEnemyAttackAnimation :
    public CyclicAnimation
{
    static const int FRAME_STATE;
public:
    StandartEnemyAttackAnimation(sf::Sprite& shipSprite);
    virtual bool init(const GameContentManager& contentManager) override;

    int nbFrame;
private:
    const int BasePosX = 27;
    const int BasePosY = 916;
    const int DimensionX = 66;
    const int DimensionY = 97;
    const int DecalageX = 73;
    const int DecalageY = 114;
};

