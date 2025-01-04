#pragma once
#include "Scene.h"
#include "Inputs.h"
#include "Player.h"
#include "Boss.h"
#include "Enemy.h"
#include "Bullet.h"
#include "Bonus.h"
#include "Hud.h"
#include "Subscriber.h"
#include "GameContentManager.h"
#include <list>
class GameScene :
    public Scene, public Subscriber
{
public:
    GameScene();
    ~GameScene();
    virtual SceneType update() override;
    virtual void draw(sf::RenderWindow& window) const override;
    virtual bool init() override;
    virtual bool uninit() override;
    virtual bool handleEvents(sf::RenderWindow& window) override;

private:
    void fireBullet(int bonusTime);
    virtual void notify(Event event, const void* data) override;
    float handleControllerDeadZone(float analogInput);
    bool bonusChance();

    int fireCooldown;


    int randSpawnFrame;
    int backScroll = 0;

    int score;
    const int ENEMY_DEATH_SCORE = 100;
    const int BONUS_SCORE = 50;

    //Aucune idée si c'est ma manette mais il fallait que je le mette aussi bas pour être a peu prêt égal au clavier
    const float GAMEPAD_MULTIPLIER = 0.03f;

    int enemyDeathCount;
    const int PLAYER_BULLET_DMG = 1;
    const int PLAYER_ENEMY_DMG = 10;

    // 1 sur BONUS_CHANCE
    const int BONUS_CHANCE = 3;

    Inputs inputs;
    Player player;

    Boss boss;

    Hud hud;

    std::list<Enemy> enemies;
    std::list<Bullet> playerBullets;
    const int INNER_CANNON_SPACING = 7;
    const int OUT_CANNON_SPACING = 25;
    const int OUTER_CANNON_SPACING = 20;
    std::list<Bullet> enemiesBullets;
    std::list<Bonus> gunBonuses;
    std::list<Bonus> healthBonuses;

    sf::Sprite backgroundTexture;
    sf::Texture enemiesTexture;
    sf::Texture bonusTextures;

    sf::Font mainFont;

    sf::Music gameMusic;

    sf::SoundBuffer playerGunSoundBuffer;
    sf::SoundBuffer enemyGunSoundBuffer;
    sf::SoundBuffer enemyKilledSoundBuffer;
    sf::SoundBuffer gunBonusSoundBuffer;
    sf::SoundBuffer healthBonusSoundBuffer;

    sf::Sound playerGun;
    sf::Sound enemyGun;
    sf::Sound enemyKill;
    sf::Sound gunBonus;
    sf::Sound healthBonus;

    SceneType nextScene;
    GameContentManager contentManager;

    
    
};

