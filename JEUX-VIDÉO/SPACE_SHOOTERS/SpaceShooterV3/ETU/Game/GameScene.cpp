#include "stdafx.h"
#include "GameScene.h"
#include "Publisher.h"

#include "game.h"

GameScene::GameScene()
	: Scene(SceneType::GAME)
	, nextScene(this->getSceneType())
{

}

GameScene::~GameScene()
{
	
}

SceneType GameScene::update()
{
#pragma region EnemyRandSpawn
	if (randSpawnFrame == NULL || randSpawnFrame <= 0)
	{
		randSpawnFrame = (rand() % 180) + 120;

		for (Enemy& enemy : enemies)
		{
			if (!enemy.isActive() && !enemy.getIsDead())
			{
				enemy.activate();
				break;
			}
		}
	}
	else
	{
		randSpawnFrame--;
	}
#pragma endregion

#pragma region FireCooldown
	if (fireCooldown > 0)
	{
		fireCooldown--;
	}
#pragma endregion

#pragma region BackScroll
	if (backScroll >= 1000000)
	{
		backScroll = 0;
	}
	else
	{
		backScroll++;
	}
	backgroundTexture.setTextureRect(sf::IntRect(0, (int)(-1 * backScroll), Game::GAME_WIDTH, Game::GAME_HEIGHT));
#pragma endregion
	
#pragma region PlayerUpdate
	player.update(1.0f / (float)Game::FRAME_RATE, inputs);
	if (inputs.fireBullet)
	{
		if (fireCooldown <= 0)
		{
			fireBullet(player.getBonusTime());
		}
	}
#pragma endregion

#pragma region EnemyUpdate
	int enemyActive = 0;
	for (Enemy& enemy : enemies)
	{
		if (enemy.isActive())
		{
			enemyActive++;
		}
		enemy.update(1.0f / (float)Game::FRAME_RATE, inputs);
		if (enemy.shouldAttack() && enemy.isActive())
		{
			for (Bullet& bullet : enemiesBullets)
			{
				if (!bullet.isActive())
				{
					bullet.setPosition(enemy.getPosition());
					bullet.activate();
					enemyGun.play();
					break;
				}
			}
		}
	}
	if (enemyDeathCount >= enemies.size())
	{
		boss.init(contentManager);
		enemyDeathCount = 0;
	}
	else if (enemyActive == 0)
	{
		randSpawnFrame = 0;
	}

	if (boss.isActive())
	{
		boss.bUpdate(1.0f / (float)Game::FRAME_RATE, inputs,player.getPosition());
		if (boss.shouldAttack())
		{
			for (Bullet& bullet : enemiesBullets)
			{
				if (!bullet.isActive())
				{
					bullet.setPosition(boss.getPosition());
					bullet.activate();
					enemyGun.play();
					break;
				}
			}
		}
	}
#pragma endregion

#pragma region BonusUpdate
	for (Bonus& bonus : gunBonuses)
	{
		bonus.update();
	}
	for (Bonus& bonus : healthBonuses)
	{
		bonus.update();
	}
#pragma endregion

#pragma region HudUpdate
	hud.update(std::to_string(player.getVie()), std::to_string(player.getBonusTime()), std::to_string(this->score));
#pragma endregion	

#pragma region BulletsUpdate
	for (Bullet& bullet : playerBullets)
	{
		if (bullet.isActive())
		{
			bullet.update();
		}
	}
	for (Bullet& bullet : enemiesBullets)
	{
		if (bullet.isActive())
		{
			bullet.update();
		}
	}
#pragma endregion

#pragma region Collisions
	for (Enemy& e : enemies)
	{
		for (Bullet& b : playerBullets)
		{
			if (e.isActive() && b.isActive() && b.collidesWith(e))
			{
				b.deactivate();
				e.takeDmg();
			}

			if (b.isActive() && boss.isActive() && b.collidesWith(boss))
			{
				boss.takeDmg();
				b.deactivate();
			}
		}

		if (e.isActive() && player.collidesWith(e))
		{
			e.destroyed();
			player.takeDamage(PLAYER_ENEMY_DMG);
		}
	}

	for (Bullet& b : enemiesBullets)
	{
		if (b.isActive() && player.collidesWith(b))
		{
			b.deactivate();
			player.takeDamage(PLAYER_BULLET_DMG);
		}
	}

	for (Bonus& b : gunBonuses)
	{
		if (b.isActive())
		{
			b.collidesWith(player);
		}
	}

	for (Bonus& b : healthBonuses)
	{
		if (b.isActive())
		{
       		b.collidesWith(player);
		}
		
	}

	if (player.collidesWith(boss))
	{
		player.takeDamage(9999999);
	}
#pragma endregion

#pragma region EndGameCheck
	if (boss.getIsDead() || !player.isActive())
	{
		nextScene = SceneType::SCOREBOARD;
	}
#pragma endregion

#pragma region SceneCheck
	if (nextScene == NULL || nextScene != this->getSceneType())
	{
		SceneType retval = nextScene;
		nextScene = this->getSceneType();
		return retval;
	}
	else
	{
		return nextScene = this->getSceneType();
	}
#pragma endregion
	
}

void GameScene::draw(sf::RenderWindow& window) const
{
	window.draw(backgroundTexture);

#pragma region CharacterDraw
	player.draw(window);
	for (const Enemy& enemy : enemies)
	{
		enemy.draw(window);	
	}
	boss.draw(window);

#pragma endregion

#pragma region BonusDraw
	for (const Bonus& bonus : gunBonuses)
	{
		bonus.draw(window);
	}
	for (const Bonus& bonus : healthBonuses)
	{
		bonus.draw(window);
	}
#pragma endregion

#pragma region BulletDraw
	for (const Bullet& bullet : playerBullets)
	{
		bullet.draw(window);
	}
	for (const Bullet& bullet : enemiesBullets)
	{
		bullet.draw(window);
	}
#pragma endregion

	hud.draw(window);
}       

bool GameScene::init()
{
	this->contentManager.init();
	inputs.reset();

	fireCooldown = 0;
	score = 0;
	enemyDeathCount = 0;

#pragma region Publish-Subscriber
	Publisher::addSubscriber(*this, Event::ENEMY_KILLED);
	Publisher::addSubscriber(*this, Event::GUN_PICKED_UP);
	Publisher::addSubscriber(*this, Event::HEALTH_PICKED_UP);
#pragma endregion

#pragma region Background&Music
	backgroundTexture.setTexture(contentManager.getBackgroundTexture());
	if (!gameMusic.openFromFile("Assets\\Music\\Game\\DazzlingSpace.ogg"))
	{
		return false;
	}
	gameMusic.setLoop(true);
	gameMusic.play();
#pragma endregion

#pragma region SoundFX
	playerGunSoundBuffer = contentManager.getPlayerGunSoundBuffer();
	enemyGunSoundBuffer = contentManager.getEnemyGunSoundBuffer();
	enemyKilledSoundBuffer = contentManager.getEnemyKilledSoundBuffer();
	gunBonusSoundBuffer = contentManager.getGunBonusSoundBuffer();
	healthBonusSoundBuffer = contentManager.getHealthSoundBuffer();

	playerGun.setBuffer(playerGunSoundBuffer);
	enemyGun.setBuffer(enemyGunSoundBuffer);
	enemyKill.setBuffer(enemyKilledSoundBuffer);
	gunBonus.setBuffer(gunBonusSoundBuffer);
	healthBonus.setBuffer(healthBonusSoundBuffer);
#pragma endregion


	hud.init(contentManager);

#pragma region BulletInit
	for (int i = 0; i < 40; i++)
	{
		Bullet bullet;
		bullet.init(CharacterType::PLAYER, this->contentManager);
		playerBullets.push_back(bullet);
	}
	for (int i = 0; i < 40; i++)
	{
		Bullet bullet;
		bullet.init(CharacterType::ENEMY, this->contentManager);
		enemiesBullets.push_back(bullet);
	}
#pragma endregion

#pragma region BonusInit
	for (int i = 0; i < 5; i++)
	{
		Bonus bonus;
		bonus.init(bonusType::GUN, this->contentManager);
		gunBonuses.push_back(bonus);
	}
	for (int i = 0; i < 5; i++)
	{
		Bonus bonus;
		bonus.init(bonusType::HEALTH, this->contentManager);
		healthBonuses.push_back(bonus);
	}
#pragma endregion


#pragma region CharacterInit
	for (int i = 0; i < 20; i++)
	{
		Enemy enemy;
		enemy.init(this->contentManager);
		enemies.push_back(enemy);
	}
	return player.init(contentManager);
#pragma endregion

}

bool GameScene::uninit()
{
	result.gameSceneResult.score = this->score;
	gameMusic.pause();
	return false;
}

bool GameScene::handleEvents(sf::RenderWindow& window)
{
	bool retval = false;
	inputs.reset();

#pragma region EventPoll
	sf::Event event;
	while (window.pollEvent(event))
	{
		if (event.type == sf::Event::Closed)
		{
			window.close();
			retval = true;
		}
	}
#pragma endregion

#pragma region Inputs
	if (sf::Joystick::isConnected(0))
	{
		inputs.moveFactorX = handleControllerDeadZone(sf::Joystick::getAxisPosition(0, sf::Joystick::Axis::X)) * GAMEPAD_MULTIPLIER;
		inputs.moveFactorY = -handleControllerDeadZone(sf::Joystick::getAxisPosition(0, sf::Joystick::Axis::Y)) * GAMEPAD_MULTIPLIER;
		inputs.fireBullet = sf::Joystick::isButtonPressed(0, 0);
	}
	else
	{
		inputs.moveFactorY += sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Up) ? 3.0f : 0.0f;
		inputs.moveFactorY += sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Down) ? -3.0f : 0.0f;
		inputs.moveFactorX += sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Right) ? 3.0f : 0.0f;
		inputs.moveFactorX += sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Left) ? -3.0f : 0.0f;
		inputs.fireBullet = sf::Keyboard::isKeyPressed(sf::Keyboard::Space);
	}
#pragma endregion

		return retval;
}

void GameScene::fireBullet(int bonusTime)
 {
	if (bonusTime > 0)
	{
#pragma region BonusFire
		bool firstBulletFound = false;
		bool secondBulletFound = false;
		bool thirdBulletFound = false;


		for (Bullet& bullet : playerBullets)
		{
			if (!bullet.isActive() && !firstBulletFound)
			{
				bullet.setPosition(sf::Vector2f(player.getPosition().x - OUTER_CANNON_SPACING, player.getPosition().y));
				firstBulletFound = true;
				bullet.activate();
			}
			else if (!bullet.isActive() && !secondBulletFound)
			{
				bullet.setPosition(sf::Vector2f(player.getPosition().x - INNER_CANNON_SPACING, player.getPosition().y - OUT_CANNON_SPACING));
				secondBulletFound = true;
				bullet.activate();
			}
			else if (!bullet.isActive() && !thirdBulletFound)
			{
				bullet.setPosition(sf::Vector2f(player.getPosition().x + INNER_CANNON_SPACING, player.getPosition().y - OUT_CANNON_SPACING));
				thirdBulletFound = true;
				bullet.activate();
			}
			else if (!bullet.isActive())
			{
				bullet.setPosition(sf::Vector2f(player.getPosition().x + OUTER_CANNON_SPACING, player.getPosition().y));
				bullet.activate();
				break;
			}
		}
#pragma endregion

		
	}
	else 
	{
#pragma region NormalFire
		bool firstBulletFound = false;
		for (Bullet& bullet : playerBullets)
		{
			if (!bullet.isActive() && !firstBulletFound)
			{
				bullet.setPosition(sf::Vector2f(player.getPosition().x - INNER_CANNON_SPACING, player.getPosition().y - OUT_CANNON_SPACING));
				firstBulletFound = true;
				bullet.activate();
			}
			else if (!bullet.isActive())
			{
				bullet.setPosition(sf::Vector2f(player.getPosition().x + INNER_CANNON_SPACING, player.getPosition().y - OUT_CANNON_SPACING));
				bullet.activate();
				break;
			}
		}
	}
#pragma endregion

	playerGun.play();
	fireCooldown = 20;
}

void GameScene::notify(Event event, const void* data)
{
	if (event == Event::ENEMY_KILLED)
	{
		score += ENEMY_DEATH_SCORE;
		if (bonusChance())
		{
			const Enemy* deadEnemy = static_cast<const Enemy*>(data);
			if (rand() % 2 + 1 == 1)
			{
				for (Bonus& bonus : gunBonuses)
				{
					if (!bonus.isActive())
					{
						bonus.setPosition(deadEnemy->getPosition());
						bonus.activate();
						break;
					}
				}
			}
			else
			{
				for (Bonus& bonus : healthBonuses)
				{
					bonus.setPosition(deadEnemy->getPosition());
					bonus.activate();
					break;
				}
			}
			
		}
		enemyDeathCount++;
		enemyKill.play();
	}
	else if(event == Event::GUN_PICKED_UP)
	{
		score += BONUS_SCORE;
		player.addBonusTime();
		gunBonus.play();
	}
	else if (event == Event::HEALTH_PICKED_UP)
	{
		score += BONUS_SCORE;
		player.addVie();
		healthBonus.play();
	}
	
}

float GameScene::handleControllerDeadZone(float analogInput)
{
	if (fabs(analogInput) < inputs.CONTROLLER_DEAD_ZONE)
	{
		analogInput = 0.0f;
	}
	return analogInput;
}

bool GameScene::bonusChance()
{
	return 1 == (rand() % BONUS_CHANCE) + 1;
}