@extends('app')

@section('content')
    <div class="backDiv">
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

            <br/>

            <div class="row row-centered">
                <div class="panel panel-body col-sm-5 col-centered">
                    <h3>{{  $video->title }}</h3>
                </div>
            </div>

            <div class="col-sm-7"> <!-- Begin Main Page -->

                <!-- Begin Video Display -->
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel panel-body">
                            <video controls>
                                <source src="../{{$video->filename}}" type="video/mp4">
                                Your browser does not support the video tag.
                            </video>
                        </div>
                    </div>


                    <hr/>

                    <div class="panel panel-default">
                        <div class="panel panel-body col-sm-8">
                            {{ $video->description }}
                            <p>
                            Tags:&nbsp
                            @if(!$video->tags->isEmpty())
                            
                                    @foreach($video->tags as $tag)
                                        {{ $tag->name }}  
                                    @endforeach
                                
                            @else
                                None
                            @endif  
                            </p>
                        </div>

                        <div class="panel panel-body">


                            <div class="pull-right">
                                <h4>Views: {{$video->views}}</h4>

                                <button type="button" class="btn btn-md btn-success">
                                    <span class="glyphicon glyphicon-thumbs-up"></span>

                                        <?php
                                            $count = 0;
                                        ?>
                                    @foreach($video->users_liked as $like)
                                        <?php
                                            $count++;
                                            echo $count;
                                        ?>
                                    @endforeach

                                </button>

                                <button type="button" class="btn btn-md btn-danger">
                                    <span class="glyphicon glyphicon-thumbs-down"></span>
                                    <?php
                                    $count2 = 0;
                                    ?>
                                    @foreach($video->users_disliked as $dislike)
                                        <?php
                                        $count2++;
                                        echo $count2;
                                        ?>
                                    @endforeach
                                </button>
                            </div>



                        </div>

                    </div>

                </div>
                <!-- End Video Display -->




                <!-- Begin Comments -->
                <div class="row">
                    <div class="panel panel-default">

                        <div class="panel panel-body">
                            <br/>

                            {!! Form::model($comment = new \App\Comment, ['url' => 'comments']) !!}
                            <div class="input-group">
                                {!! Form::hidden('video_id',$video->id) !!}
                                {!! Form::text('text', null, ['class' => 'form-control']) !!}
                                <div class="input-group-btn">
                                    {!! Form::submit('Enter', ['class' => 'btn btn-md ']) !!}
                                    <!--button type="button" class="btn btn-md">
                                        Enter
                                    </button-->
                                </div>
                            </div>
                            {!! Form::close() !!}

                            <br/>

                            <ul class="list-group">
                                @foreach($video->comments as $comment)
                                    <button type="button" class="list-group-item">{{ $comment->user->name }} <br/> {{ $comment->text }}</button>
                                @endforeach
                            </ul>

                        </div>

                    </div>
                </div>
                <!-- End Comments -->


            </div> <!-- End Main Page -->

            <div class="col-sm-1"></div>  <!--Spacer Do Not Put Web Elements Here-->

            <div class="col-sm-4"> <!-- Begin Suggested Videos -->
                <div class="panel panel-default">
                    <div class="panel panel-body">
                        <ul class="list-group">
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                            <button type="button" class="list-group-item">Title <br/> Description </button>
                        </ul>
                    </div>

                </div>
            </div> <!-- End Suggested Videos -->
        </div>
    </div>

    <img src="../Ads/adOne.png" alt="" id="ad">
@stop
