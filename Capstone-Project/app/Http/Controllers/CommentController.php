<?php namespace App\Http\Controllers;

use App\Comment;
use App\Http\Requests;
use App\Http\Controllers\Controller;
use App\Http\Requests\CommentRequest;
use App\Video;

use Illuminate\Support\Facades\Auth;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Session;

class CommentController extends Controller {


	/**
	 * Store a newly created resource in storage.
	 *
	 * @return Response
	 */
	public function store(CommentRequest $request)
	{
        $input = $request->all();

        if (Auth::guest())
        {
            \Session::flash('flash_message','You need to be logged in to comment!');
        }
        else
        {
            $input['user_id'] = Auth::id();
            Comment::create($input);
        }


        $video = Video::findOrFail($input['video_id']);
        $test = Video::where('id', $input['video_id'])->increment('numComments');

        return view('videos/show', compact('video', 'test'));
	}
}
