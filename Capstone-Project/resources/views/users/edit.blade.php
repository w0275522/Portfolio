@extends('app')

@section('content')

    <h1>Edit: {!! $user->name !!}</h1>
    <h2>current email: {!! $user->email !!}</h2>
    <h2>current birthday: {!! $user->birthday !!}</h2>
    {!! Form::model($user, ['method' => 'PATCH', 'action' => ['UsersController@update', $user->id]]) !!}
    @include ('users/form', ['submitButtonText' => 'Update User'])
    {!! Form::close() !!}

@stop