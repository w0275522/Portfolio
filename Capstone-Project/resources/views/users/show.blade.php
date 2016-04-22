@extends('app')

@section('content')

    <h1>Name:{{$user->name}}</h1>
    <h3>Email:{{$user->email}}</h3>

    Created at:{{$user->created_at}}<br>
    Modified at:{{$user->updated_at}}<br>

@stop