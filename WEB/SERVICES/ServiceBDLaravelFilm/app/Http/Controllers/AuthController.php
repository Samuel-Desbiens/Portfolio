<?php

namespace App\Http\Controllers;

use Validator;
use Illuminate\Http\Request;
use App\Models\User;
use App\Repository\UserRepository;
use Auth;

class AuthController extends Controller
{

    private UserRepository $userRepository;

    public function __construct(UserRepository $userRepository)
    {
        $this->userRepository = $userRepository;
    }

    /**
     * @OA\Post(
     * path="/api/signin",
     * tags={"Users"},
     * summary="Logs in a existing user",
     * @OA\Response(
     *      response = 201,
     *      description = "Created"),
     *      @OA\RequestBody(
     *          @OA\MediaType(
     *              mediaType="application/json",
     *              @OA\Schema(
     *                  @OA\Property(
     *                      property="email",
     *                      type="string"
     *                      ),
     *                  @OA\Property(
     *                      property="password",
     *                      type="string"
     *                      ),
     *              )
     *             )
     *          )
     *      )
     */
    public function login(Request $request)
    {
        try
        {
            $validator = Validator::make($request->all(),[
                'email' => 'required|email',
                'password' => 'required', 
            ]);
            if($validator->fails())
            {
                abort(INVALID_DATA,'Invalid Data');
            }
            else
            {
                if(Auth::attempt(['email' => $request->email, 'password' => $request->password]))
                {
                    $token = Auth::user()->createToken('Token');

                    return (response()->json(['token' => $token->plainTextToken]))->setStatusCode(CREATED);
                }
                else
                {
                    abort(UNAUTHORIZED,'Unauthorized');
                }
            }
        }
        catch(Exception $ex)
        {
            abort(500,'Server Error');
        }
        
    }
	
    /**
     * @OA\Post(
     * path="/api/signup",
     * tags={"Users"},
     * summary="Creates a new user",
     * @OA\Response(
     *      response = 201,
     *      description = "Created"),
     *      @OA\RequestBody(
     *          @OA\MediaType(
     *              mediaType="application/json",
     *              @OA\Schema(
     *                  @OA\Property(
     *                      property="login",
     *                      type="string"
     *                      ),
     *                  @OA\Property(
     *                      property="email",
     *                      type="string"
     *                      ),
     *                  @OA\Property(
     *                      property="password",
     *                      type="string"
     *                      ),
     *                  @OA\Property(
     *                      property="last_name",
     *                      type="string"
     *                      ),
     *                  @OA\Property(
     *                      property="first_name",
     *                      type="string"
     *                      ),
     *              )
     *             )
     *          )
     *      )
     */
    public function register(Request $request)
    {
        try
        {
            $validator = Validator::make($request->all(),[
                'login' => 'required',
                'email' => 'required|email|unique:users,email',
                'password' => 'required|confirmed',
                'last_name' => 'required',
                'first_name' => 'required',
            ]);
            if($validator->fails())
            {
                abort(INVALID_DATA,'Invalid Data');
            }
            else
            {
                $user = $this->userRepository->create($request->all());
                
                if(Auth::attempt(['email' => $request->email, 'password' => $request->password]))
                {
                  $token = $user->createToken('Test Token');

                  return (response()->json(['token' => $token->plainTextToken]))->setStatusCode(CREATED);
                }
                else
                {
                  abort(NOT_FOUND,'Not Found');
                }
            }
        }
        catch(Exception $ex)
        {
            abort(SERVER_ERROR,'Server Error');
        }
    }

    /**
     * @OA\Post(
     * path="/api/signout",
     * tags={"Users"},
     * summary="Logs out a connected user",
     * @OA\Response(
     *      response = 204,
     *      description = "No Content"),
     *      @OA\RequestBody(
     *          @OA\MediaType(
     *              mediaType="application/json",
     *             )
     *          )
     *      )
     */
    public function logout(Request $request)
    {
        try
        {
            if(Auth::check())
            {
                    foreach(Auth::user()->tokens as $token)
                {
                    $token->delete();
                }
                return response()->noContent();
            }
            else
            {
                abort(NOT_FOUND,'Not Found');
            }
            
        }
        catch(Exception $ex)
        {
            abort(SERVER_ERROR,'Server Error');
        }
        
    }
}
