@extends('app')

@section('content')
    <div class="container">
        <h1>Upload a Video</h1>

        <hr>

        {!! Form::model($video = new \App\Video, ['url' => 'videos', 'method' => 'POST', 'files' => true]) !!}
            @include('videos/form', ['submitButtonText' => 'Upload Video'])
        {!! Form::close() !!}
    </div>
@stop