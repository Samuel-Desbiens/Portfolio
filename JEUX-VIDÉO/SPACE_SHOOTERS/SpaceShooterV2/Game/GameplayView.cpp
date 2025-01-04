#include "stdafx.h"
#include "GameplayView.h"
#include "game.h"
GameplayView::GameplayView()
	: View(Game::WIDTH, Game::HEIGHT)
	, score("Assets/Fonts/DSDIGI.TTF", 40)
    , arme1("Assets/BitMaps/AmmoBonus.JPG", 1, 1)
    , arme2("Assets/BitMaps/AmmoBonus.JPG", 1, 1)
    , arme3("Assets/BitMaps/AmmoBonus.JPG", 1, 1)
{
	arme1.SetPosition(sf::Vector2f(0, 700));
	arme2.SetPosition(sf::Vector2f(0, 700));
	arme3.SetPosition(sf::Vector2f(0, 700));
	ui.AddWidget(arme1);
	ui.AddWidget(arme2);
	ui.AddWidget(arme3);

	score.SetPosition(sf::Vector2f(0, 0));
	score.SetText("Score:");
	ui.AddWidget(score);
}

GameplayView::~GameplayView()
{

}

void GameplayView::Update()
{

}