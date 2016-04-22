<?php namespace App\Http\Controllers;

use App\Video;
use App\Http\Requests;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Input;
use App\Http\Requests\VideoRequest;
use Illuminate\Http\Request;
use App\Tag;
use Illuminate\Support\Facades\Auth;

class VideosController extends Controller {

	public function index()
    {
        $videos = Video::all();

        return view('videos.index', compact('videos'));
    }

    public function show($id)
    {
        $video = Video::findOrFail($id);

        $video->increment('views');
        $video->views += 1;


        if (!Auth::guest())
        {
            //$user = User::find(Auth::user()->id);
            $watch = false;
            foreach(Auth::user()->videos_watched as $userVideo)
            {
                if ($userVideo->id == $id)
                {
                    $watch = true;
                }
            }
            if (!$watch)
            {
                Auth::user()->videos_watched()->attach($id);
            }
        }


        return view('videos/show', compact('video'));
    }

    public function create()
    {
        $tags = Tag::lists('name', 'id');
        return view('videos/create', compact('tags'));
    }

    public function store(VideoRequest $request)
    {
        $this->createVideo($request);
        return redirect('videos');
    }

    public function edit()
    {

    }

    public function update()
    {

    }

    public function syncTags(Video $video, array $tags)
    {
        $video->tags()->sync($tags);
    }
    public function createVideo(VideoRequest $request)
    {

        if(!file_exists('Videos/'))
        {
            mkdir('Videos/');
        }

        $destinationPath = 'Videos/';
        $extension = Input::file('filename')->getClientOriginalExtension();
        $fileName = rand(11111,99999).'.'.$extension;
        Input::file('filename')->move($destinationPath, $fileName);
        $video = Video::create($request->all());
        $video->filename = 'Videos/' . $fileName;

        if(!file_exists('Thumbnails/'))
        {
            mkdir('Thumbnails/');
        }

        $destinationPath = 'Thumbnails/';
        $extension = Input::file('thumbnail')->getClientOriginalExtension();
        $fileName = rand(11111,99999).'.'.$extension;
        Input::file('thumbnail')->move($destinationPath, $fileName);
        $video->thumbnail = 'Thumbnails/' . $fileName;
        $video->save();

        $this->syncTags($video, $request->input('tag_list'));
        return redirect('videos');

    }

    public function destroy()
    {

    }
}
