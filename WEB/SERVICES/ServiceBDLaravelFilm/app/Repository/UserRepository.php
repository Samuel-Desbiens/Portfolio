<?php

namespace App\Repository;

use App\Repository\Interfaces\UserRepositoryInterface;
use App\Models\User;

class UserRepository implements UserRepositoryInterface
{

    public function create(array $content)
    {
        return User::create([
            'login' => $content['login'],
            'email' => $content['email'],
            'password' => bcrypt($content['password']),
            'last_name' => $content['last_name'],
            'first_name' => $content['first_name'],
            'role_id' => $content['role_id'],
        ]);
    }

    public function getAll($perPage = 0)
    {
        return User::paginate($perPage);
    }

    public function getById($id)
    {
        return User::findOrFail($id);
    }

    public function update($id,array $content)
    {
        return $this->getById($id)->fill($content)->save();   
    }

    public function delete($id)
    {
        User::destroy($id);
    }
}

?>