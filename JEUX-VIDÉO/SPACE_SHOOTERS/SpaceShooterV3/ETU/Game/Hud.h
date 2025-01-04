#pragma once
#include "GameContentManager.h"
class Hud
{
public:
	Hud();
	void init(const GameContentManager& contentmanager);
	void update(std::string vie, std::string bonusTime, std::string score);
	void draw(sf::RenderWindow& window) const;
private:
	sf::Font mainFont;

	sf::Text score;
	sf::Text vitalite;
	sf::Text bonusTime;

	sf::View hudView;

	sf::Sprite vie;
	sf::Sprite bonus;

	const std::string BASE_SCORE_STRING = "Score: ";
	const std::string BASE_NUMBER = "0";
	


};

