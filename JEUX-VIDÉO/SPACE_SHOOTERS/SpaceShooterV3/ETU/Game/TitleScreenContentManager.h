#pragma once
class TitleScreenContentManager
{
public:
	TitleScreenContentManager();
	void init();
	virtual bool loadContent();

	const sf::Texture& getTitleScreenTexture() const;

	const sf::Font& getMainFont() const;
private:
	sf::Texture titleScreenTexture;

	sf::Font mainFont;
};

