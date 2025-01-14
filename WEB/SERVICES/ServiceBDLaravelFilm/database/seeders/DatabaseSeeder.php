<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use App\Models\Critic;
use App\Models\User;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {
        $this->call([            
            LanguageSeeder::class,
            FilmSeeder::class,
            ActorSeeder::class,
            FilmActorSeeder::class,
            RoleSeeder::class,
            StatSeeder::class
        ]);
    }
}
