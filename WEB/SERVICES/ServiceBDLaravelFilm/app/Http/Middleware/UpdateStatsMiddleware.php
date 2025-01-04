<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use App\Models\Stat;

class UpdateStatsMiddleware
{
    /**
     * Handle an incoming request.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \Closure(\Illuminate\Http\Request): (\Illuminate\Http\Response|\Illuminate\Http\RedirectResponse)  $next
     * @return \Illuminate\Http\Response|\Illuminate\Http\RedirectResponse
     */


    //StatController est aussi ok (update)
    public function handle(Request $request, Closure $next)
    {
        //$film_id = $request["film"];
        //$score = $request["score"];

        //echo($film_id . "<br>");
        //echo($score . "<br>");
               
        try
        {
            $stat = Stat::findOrFail($film_id);
            $current_votes = $stat->votes;
            $current_score = $stat->average_score;

            $current_score += $score;
            $current_votes += $votes;
            $stat->average_score = round($current_score / $current_votes, 1);
            $stat->votes = $current_votes;
            $stat->save();
        }

        catch(QueryException $ex)
        {
            abort(NOT_FOUND, "the movie is not found");
        }

        catch(Exception $ex)
        {
            abort(SERVER_ERROR, "Server error");
        }
    }
}
