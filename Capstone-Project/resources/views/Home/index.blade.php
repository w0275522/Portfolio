@extends('app')

@section('content')
    <div class="container">
        <style>
            #ad {
                width:680px;
                height: auto;
                display: block;
                margin-left: auto;
                margin-right: auto

            }
        </style>

        <img src="../Ads/adOne.png" alt="" id="ad">

        <h1>Home Page - Video List</h1>

        <hr/>


        <div class="row">
            <div class="panel panel-default">

                <div class="panel panel-body"><!-- Img tag sizing for video is in app.blade.php if need to edit it -->

                    @foreach($videos as $video)
                        <div class="col-sm-4">


                            <a href="{{ url('/videos', $video->id) }}" class="btn btn-secondary list-group-item">
                                <img src="../{{$video->thumbnail}}" alt="Thumbnail Missing">
                                <br/>
                                {{ $video->title }}
                                <br/>
                                {{ $video->description }}
                                @unless($video->tags->isEmpty())

                                    <br/>
                                    Tags:&nbsp

                                        @foreach($video->tags as $tag)
                                            {{ $tag->name }}
                                        @endforeach


                                @endunless
                            </a>
                        </div>
                    @endforeach

                </div>

            </div>
        </div>

        <img src="../Ads/adOne.png" alt="" id="ad">

    </div>
@stop
