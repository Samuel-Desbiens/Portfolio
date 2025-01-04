#pragma once
#include "InputBasedAnimation.h"
class ShipAnimation :
    public InputBasedAnimation
{
    static const int DEFAULT_FRAME_STATE;
public:
    ShipAnimation(sf::Sprite& shipSprite);
    virtual bool init(const GameContentManager& contentManager) override;
protected:
    virtual void adjustNextFrame(const Inputs& inputs) override;

    int nbFrameState;
};

