#pragma once
class GameContentManager
{
public:
	GameContentManager();
	void init();
	virtual bool loadContent();

	const sf::Texture& getMainCharacterTexture() const;
	const sf::Texture& getBackgroundTexture() const;
	const sf::Texture& getEnemiesTexture() const;
	const sf::Texture& getBonusTexture() const;
	
	const sf::Font& getMainFont() const;

	const sf::SoundBuffer& getPlayerGunSoundBuffer() const;
	const sf::SoundBuffer& getEnemyGunSoundBuffer() const;
	const sf::SoundBuffer& getEnemyKilledSoundBuffer() const;
	const sf::SoundBuffer& getGunBonusSoundBuffer() const;
	const sf::SoundBuffer& getHealthSoundBuffer() const;
	

private:
	sf::Texture characterTexture;
	sf::Texture backgroundTexture;
	sf::Texture enemiesTexture;
	sf::Texture bonusTextures;

	sf::Font mainFont;

	sf::SoundBuffer playerGunSoundBuffer;
	sf::SoundBuffer enemyGunSoundBuffer;
	sf::SoundBuffer enemyKilledSoundBuffer;
	sf::SoundBuffer gunBonusSoundBuffer;
	sf::SoundBuffer healthBonusSoundBuffer;

};

