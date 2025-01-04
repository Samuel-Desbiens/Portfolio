<?php

namespace App\GraphQL\Mutations;

use App\Models\Critic;

final class CreateCritic
{
    /**
     * @param  null  $_
     * @param  array{}  $args
     */
    public function __invoke($_, array $args)
    {
        try
        {
            $criticToAddStat = Critic::create([
                'user_id' => $args["user_id"],
                'film_id' => $args["film"]["connect"],
                'score' => $args["score"],
                'comment' => $args["comment"]
            ]);

            $StatTU = $criticToAddStat->film->statistic;

            $NBCritic = $StatTU->film->critics->count();
            $ScoreTotal = 0;
            foreach ($StatTU->film->critics as $critic)
            {
                $ScoreTotal += $critic->score;
            }
            
            $StatTU->update(['average_score'=>$ScoreTotal/$NBCritic]);

            return $StatTU;
        }
        catch(Exception $ex)
        {
            abort(SERVER_ERROR,'Server Error');
        }
    }
}
