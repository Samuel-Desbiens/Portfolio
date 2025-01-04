#pragma once
class ScoreContentManager
{
public:
	ScoreContentManager();
	void init();
	virtual bool loadContent();

	const sf::Font& getMainFont() const;
private:
	sf::Font mainFont;
};

