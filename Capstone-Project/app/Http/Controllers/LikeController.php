<?php namespace App\Http\Controllers;

use App\Comment;
use App\Http\Requests;
use App\Http\Controllers\Controller;
use App\Http\Requests\CommentRequest;
use App\Video;

use Illuminate\Support\Facades\Auth;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Session;

class LikeController extends Controller {


    /**
     * Store a newly created resource in storage.
     *
     * @return Response
     */
    public function store(LikeRequest $request)
    {
        $input = $request->all();

        if (Auth::guest())
        {
            \Session::flash('flash_message','You need to be logged in to like/dislike!');
        }
        else
        {
            $input['user_id'] = Auth::id();
            Like::create($input);
        }


        $video = Video::findOrFail($input['video_id']);
        return view('videos/show', compact('video'));
    }
}
