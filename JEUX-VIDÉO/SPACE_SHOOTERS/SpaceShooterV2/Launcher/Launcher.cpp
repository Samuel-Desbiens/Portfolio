#include "pch.h"
#include "game.h"
int main()
{
    Game aGame("A Space Shooter",
               Game::WIDTH,
               Game::HEIGHT);
    aGame.Run();
}
