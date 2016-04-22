@extends('app')

@section('content')
    <div class="container">
        <h1>Admin page</h1>

        <hr/>



        <div class="row">
            <div class="panel panel-default">

                <div class="panel panel-body"><!-- Img tag sizing for video is in app.blade.php if need to edit it -->
                    <h1>Top 5 most viewed videos</h1>
                    <?php $counter = 0 ?>
                    @foreach($videos as $video)
                        <div class="col-sm-4">
                            <?php $counter += 1; ?>
                            <strong>Rank: {{$counter}}</strong>
                            <a href="{{ url('/videos', $video->id) }}" class="btn btn-secondary list-group-item">
                                <br/>
                                {{ $video->title }}
                                <br/>
                                Total Views: {{ $video->views }}
                                <br/>

                                {{str_limit($value = $video->views / $totalViews * 100, $limit = 5, $end = '')}}% of total views.

                            </a>
                        </div>
                    @endforeach

                </div>

            </div>
        </div>


        <div class="row">
            <div class="panel panel-default">

                <div class="panel panel-body"><!-- Img tag sizing for video is in app.blade.php if need to edit it -->
                    <h1>Top 5 most commented on videos</h1>
                    <?php $counter = 0 ?>
                    @foreach($comments as $comments)
                        <div class="col-sm-4">
                            <?php $counter += 1; ?>
                            <strong>Rank: {{$counter}}</strong>
                            <a href="{{ url('/videos', $comments->id) }}" class="btn btn-secondary list-group-item">
                                <br/>
                                {{ $comments->title }}
                                <br/>
                                Total Comments: {{ $comments->numComments }}
                                <br/>

                                {{str_limit($value = $comments->numComments / $totalComments * 100, $limit = 5, $end = '')}}% of total comments.

                            </a>
                        </div>
                    @endforeach

                </div>

            </div>
        </div>


        <div class="row">
            <div class="panel panel-default">

                <div class="panel panel-body"><!-- Img tag sizing for video is in app.blade.php if need to edit it -->
                    <h1>Unique Visitors</h1>
                    Number of visitors for {{\Carbon\Carbon::today()->format('m/d/Y')}}:<br/>
                    <h2>{{ $totalTrackers }}</h2>

                </div>

            </div>
        </div>
    </div>



@stop
