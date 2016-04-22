<?php namespace App\Http\Controllers;

use App\Http\Requests;
use App\Video;
use App\Tracker;
use Carbon\Carbon;
use App\Http\Controllers\Controller;
use App\Http\Requests\RegisterRequest;
use App\Http\Requests\UserRequest;
use Illuminate\Http\Request;

class AdminController extends Controller {

    public function __construct()
    {
        //$this->middleware('AdminAuth');
    }

    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index()
    {
        //
        $videos = Video::orderBy('views', 'desc')->limit(5)->get();
        $totalViews = Video::all()->sum('views');
        $comments = Video::orderBy('numComments', 'desc')->limit(5)->get();
        $totalComments = Video::all()->sum('numComments');
        $tracker = Tracker::all()->where('date', Carbon::today()->format('m/d/Y'));
        $totalTrackers = Tracker::all()->where('date', Carbon::today()->format('m/d/Y'))->count();


        return view('admin.stats', compact('videos', 'totalViews', 'comments', 'totalComments', 'tracker', 'totalTrackers'));
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create(RegisterRequest $request)
    {
        $tracker = Tracker::create([
            'key' => $request->session()->get('key'),
            'date' => Carbon::today()->format('m/d/Y'),
        ]);
        $tracker->save();

    }

    /**
     * Store a newly created resource in storage.
     *
     * @return Response
     */
    public function store()
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function show($id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function update($id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($id)
    {
        //
    }

}
