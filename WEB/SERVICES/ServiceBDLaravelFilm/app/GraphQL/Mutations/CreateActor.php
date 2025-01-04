<?php

namespace App\GraphQL\Mutations;

use Illuminate\Support\Facades\DB;
use App\Models\Film;
use App\Models\Actor;

final class CreateActor
{
    /**
     * @param  null  $_
     * @param  array{}  $args
     */
    public function __invoke($_, array $args)
    {
        try
        {
             $actorToAdd = Actor::create([
                'last_name' => $args["last_name"],
                'first_name' => $args["first_name"],
                'birthdate' => $args["birthdate"]
            ]);

            foreach($args["films"]["connect"] as $filmIds)
            {
                DB::table('actor_film')->insert([
                    'actor_id' => $actorToAdd->id,
                    'film_id' => $filmIds,
                ]);
            }
            
                
            foreach($args["updates"]["id"] as $ids)
            {
                Film::where('id','=',$ids)->update(['image' => $args["updates"]["image"]]);
            }
                
            return $actorToAdd;
        }
        catch(Exception $ex)
        {
            abort(SERVER_ERROR,'Server Error');
        }
    }
}
