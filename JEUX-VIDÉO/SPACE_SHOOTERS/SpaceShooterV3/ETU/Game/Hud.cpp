#include "stdafx.h"
#include "Hud.h"
#include "game.h"

Hud::Hud()
{

}

void Hud::init(const GameContentManager& contentManager)
{
	hudView = sf::View(sf::FloatRect(0, 0, Game::GAME_WIDTH, Game::GAME_HEIGHT));

	mainFont = contentManager.getMainFont();

	//Texte Score
	score.setFont(mainFont);
	score.setCharacterSize(32);
	score.setFillColor(sf::Color::White);
	score.setOutlineThickness(1);
	score.setOutlineColor(sf::Color::Black);
	score.setString(Hud::BASE_SCORE_STRING);
	score.setPosition(0, Game::GAME_HEIGHT - score.getLocalBounds().height);

	//Texte Vie
	vitalite.setFont(mainFont);
	vitalite.setCharacterSize(32);
	vitalite.setFillColor(sf::Color::White);
	vitalite.setOutlineThickness(1);
	vitalite.setOutlineColor(sf::Color::Black);
	vitalite.setString(BASE_NUMBER);
	vitalite.setPosition(Game::GAME_WIDTH / 1.5f, Game::GAME_HEIGHT - vitalite.getLocalBounds().height);

	//Texte Bonus
	bonusTime.setFont(mainFont);
	bonusTime.setCharacterSize(32);
	bonusTime.setFillColor(sf::Color::White);
	bonusTime.setOutlineThickness(1);
	bonusTime.setOutlineColor(sf::Color::Black);
	bonusTime.setString(BASE_NUMBER);
	bonusTime.setPosition(Game::GAME_WIDTH - bonusTime.getLocalBounds().width*3, Game::GAME_HEIGHT - bonusTime.getLocalBounds().height);

	//Sprite Vie
	vie.setTexture(contentManager.getBonusTexture());
	vie.setTextureRect(sf::IntRect(315, 109, 7, 7));
	vie.setScale(4, 4);
	vie.setPosition(Game::GAME_WIDTH / 1.65f, Game::GAME_HEIGHT - vie.getLocalBounds().height * 4);
	
	//Sprite Bonus
	bonus.setTexture(contentManager.getBonusTexture());
	bonus.setTextureRect(sf::IntRect(336, 109, 7, 7));
	bonus.setScale(4, 4);
	bonus.setPosition(Game::GAME_WIDTH / 1.25f, Game::GAME_HEIGHT - vie.getLocalBounds().height * 4);
}

void Hud::update(std::string vie, std::string bonusTime, std::string score)
{
	this->score.setString(BASE_SCORE_STRING + score);
	this->vitalite.setString(vie);
	this->bonusTime.setString(bonusTime);
}

void Hud::draw(sf::RenderWindow& window) const
{
	window.draw(score);
	window.draw(vitalite);
	window.draw(bonusTime);
	window.draw(vie);
	window.draw(bonus);
}