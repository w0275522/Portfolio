@extends('app')

@section('content')
    <div class="container">
        <h1>Articles</h1>

        <hr/>

        @if (Auth::guest())
            <h2>Not Logged In</h2>
        @else
            <h2>Watched Videos</h2>
            <ul>
                @foreach(Auth::user()->videos_watched as $video)
                    <p>{{ $video->title }}</p>
                @endforeach
            </ul>

            <h2>Liked Videos</h2>
            <ul>
                @foreach(Auth::user()->videos_liked as $video)
                    <p>{{ $video->title }}</p>
                @endforeach
            </ul>

            <h2>Disliked Videos</h2>
            <ul>
                @foreach(Auth::user()->videos_disliked as $video)
                    <p>{{ $video->title }}</p>
                @endforeach
            </ul>

            <h2>Comments</h2>
            <ul>
                @foreach(Auth::user()->comments as $comment)
                    <p>{{ $comment->text }}</p>
                @endforeach
            </ul>
        @endif
    <div class="row">
        <div class="panel panel-default">

            <div class="panel panel-body"><!-- Img tag sizing for video is in app.blade.php if need to edit it -->

                    @foreach($videos as $video)
                        <div class="col-sm-4">
                            <button type="button" class="list-group-item">
                                <img src="../{{$video->thumbnail}}" alt="Thumbnail Missing">
                                <br/>
                                <a href="{{ url('/videos', $video->id) }}">{{ $video->title }}</a>
                                <br/>
                                {{ $video->description }}
                                <p>Comments</p>
                                <ul>
                                    @foreach($video->comments as $comment)
                                        <p>{{ $comment->text }}</p>
                                    @endforeach
                                </ul>
                            </button>
                        </div>
                    @endforeach

            </div>

        </div>
    </div>





    </div>
@stop
