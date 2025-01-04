#pragma once
#include "GameObject.h"
#include "bonustype.h"
#include "Player.h"
class Bonus :
    public GameObject
{
public:
    Bonus();
    Bonus(const Bonus& src);
    virtual bool init(bonusType btype,const GameContentManager& contentManager);
    bool update();
    void collidesWith(const Player& p) ;
    
private:
    bonusType type;
    Bonus& operator=(const Bonus& b);
};

