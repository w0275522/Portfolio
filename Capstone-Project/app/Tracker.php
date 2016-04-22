<?php

namespace App;

use App\Http\Requests\RegisterRequest;
use Illuminate\Database\Eloquent\Model;

class Tracker extends Model{

    protected $table = 'tracker';

    protected $fillable = ['key', 'date'];


    public function createNewTracker(RegisterRequest $request)
    {
        $tracker = Tracker::create([
            'key' => $request->session()->get('key'),
            'date' => Carbon::today()->format('m/d/Y'),
        ]);
        $tracker->save();
    }

}