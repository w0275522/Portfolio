<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Video extends Model
{
    protected $fillable = [
        'filename',
        'title',
        'description',
        'thumbnail',
        'views',
        'numComments'
    ];

    public function tags()
    {
        return $this->belongsToMany('App\Tag')->withTimestamps();
    }

    public function users_liked()
    {
        return $this->belongsToMany('App\User','likes');
    }

    public function users_disliked()
    {
        return $this->belongsToMany('App\User','dislikes');
    }

    public function users_watched()
    {
        return $this->belongsToMany('App\User','watched');
    }

    public function comments()
    {
        return $this->hasMany('App\Comment');
    }

}
