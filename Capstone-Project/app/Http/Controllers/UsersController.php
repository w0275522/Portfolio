<?php namespace App\Http\Controllers;

use App\Http\Requests;
use App\Http\Controllers\Controller;
use App\User;
use App\Http\Requests\RegisterRequest;
use Auth;
use Illuminate\Http\Request;
use App\Http\Requests\UserRequest;

class UsersController extends Controller {

	/**
	 * Display a listing of the resource.
	 *
	 * @return Response
	 */
	public function index()
	{
		return view('auth.login');
	}

	/**
	 * Show the form for creating a new resource.
	 *
	 * @return Response
	 */
	public function create()
	{
		if(Auth::user()->guest)
            return view('users.register');
        else
            return view('users.edit');
	}
    public function createUser(RegisterRequest $request)
    {
        if(Auth::user()->guest)
        {
            $user = User::create([
                'name' => $request['name'],
                'email' => $request['email'],
                'password' => bcrypt($request['password']),
                'birthday' => $request['birthday'],
                'admin' => false,
            ]);
            $user->save();
        }
        else
            return view('users.edit');
    }
	/**
	 * Store a newly created resource in storage.
	 *
	 * @return Response
	 */
	public function store(RegisterRequest $request)
	{
		if(Auth::user()->guest)
        {
            $this->createUser($request);
            return redirect('users');
        }
        else
            return view('users.edit');
	}

	/**
	 * Display the specified resource.
	 *
	 * @param  int  $id
	 * @return Response
	 */
	public function show($id)
    {
        if (Auth::user())
        {
            $user = User::find($id);
            return view('users.show', compact('user'));
        }
        else
            return view('auth.login');
    }

	/**
	 * Show the form for editing the specified resource.
	 *
	 * @param  int  $id
	 * @return Response
	 */
	public function edit($id)
	{
		if(Auth::user())
        {
            $user = User::find($id);
            return view('users.edit', compact('user'));
        }
        else
            return view('auth.login');
	}

	/**
	 * Update the specified resource in storage.
	 *
	 * @param  int  $id
	 * @return Response
	 */
	public function update($id, UserRequest $request)
	{
		if(Auth::user())
        {
            $user = User::find($id);
            $user->update([
                'name' => $request['name'],
                'email' => $request['email'],
                'password' => bcrypt($request['password']),
                'birthday' => $request['birthday'],
            ]);

            //if(!$user->isValid($user->id))
            //{
            //    return Redirect::back()->withInput()->withErrors($user->messages);
            //}
            $user->save();
            return Redirect('/');
        }
        else
            return view('auth.login');
	}

	/**
	 * Remove the specified resource from storage.
	 *
	 * @param  int  $id
	 * @return Response
	 */
	public function destroy($id)
	{
		//
	}

}
