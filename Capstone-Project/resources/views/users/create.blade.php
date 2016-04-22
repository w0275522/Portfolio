@extends('app')

@section('content')
    <h1>Make a new User</h1>

    <hr>

    {!! Form::model($user = new \App\User, ['url' => 'users']) !!}
    @include('users/form', ['submitButtonText' => 'Add User'])
    {!! Form::close() !!}

@stop