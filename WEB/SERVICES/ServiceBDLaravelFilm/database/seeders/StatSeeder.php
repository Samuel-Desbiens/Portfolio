<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use App\Models\Film;
use App\Models\Stat;

class StatSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $json = file_get_contents(database_path() . '/seeders/data_source.json');
        $films = json_decode($json);

        //$films est l'objet json au complet représenté par la datasource.json

        //TODO : Mettre à jour les statistiques 
        //RAPPEL : Le seed donné n'insère pas de critiques, on a seulement les critiques de la data source pour le moment
        //         Ceci facilite le calcul

        //Pour avoir un user a link les critiques
        $linkId = DB::table('users')->insertGetId([
            'login' => 'admin',
            'password' => bcrypt('admin'),
            'email' => 'admin@admin.com',
            'last_name' => 'min',
            'first_name' => 'ad',
            'role_id' => 2,
        ]);

        foreach ($films->data as $film)
        {
            $NBCritic = 0;
            $ScoreTotal = 0;
            foreach ($film->reviews as $review)
            {
                $NBCritic++;
                $ScoreTotal += $review->score;

                DB::table('critics')->insert([
                    'user_id' => $linkId,
                    'film_id' => $film->id,
                    'score' => $review->score,
                    'comment' => 'source: '.$review->source,
                ]);
            }
            $LastInserted = DB::table('statistics')->insertGetId([
                'average_score' => $ScoreTotal/$NBCritic,
            ]);

            DB::table('films')->where('id', $film->id)->update(['statistic_id' => $LastInserted]);
        }
    }
}
