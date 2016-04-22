<?php

use App\Video;
use App\Tag;
use App\User;
use App\Comment;
use App\Tracker;
use Illuminate\Database\Seeder;
use Carbon\Carbon;

class DatabaseSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {


        $this->call('VideoTableSeeder');

        $this->command->info('Video table seeded!');

        $this->call('TagTableSeeder');

        $this->command->info('Tag table seeded!');

        $this->call('UserTableSeeder');

        $this->command->info('User table seeded!');

        $this->call('CommentTableSeeder');

        $this->command->info('Comment table seeded!');

        $this->call('TrackerTableSeeder');

        $this->command->info('Tracker table seeded!');

        $video = Video::findOrFail(1);
        $video->tags()->attach(1);
        //$video->comments()->attach(1);

        $video = Video::findOrFail(2);
        $video->tags()->attach(2);
        $video->tags()->attach(3);

        $user = User::findOrFail(2);
        $user->videos_watched()->attach(1);
        $user->videos_watched()->attach(2);
        $user->videos_liked()->attach(1);
        $user->videos_disliked()->attach(2);
    }
}
class VideoTableSeeder extends Seeder {

    public function run()
    {
        DB::table('videos')->delete();

        Video::create([
            'id' => 1,
            'filename' => 'Videos/test.mp4',
            'title' => 'Test Video',
            'description' => 'This is a test description',
            'thumbnail' => 'Thumbnails/test.jpg',
            'views' => 0,
            'numComments' => 3,
        ]);

        Video::create([
            'id' => 2,
            'filename' => 'Videos/another_test.wav',
            'title' => 'Video 2',
            'description' => 'This is a another description',
            'thumbnail' => 'Thumbnails/another_test.png',
            'views' => 34,
            'numComments' => 2,
        ]);
    }
}

class TagTableSeeder extends Seeder {

    public function run()
    {
        DB::table('tags')->delete();

        Tag::create([
            'id' => 1,
            'name' => 'tag',
        ]);

        Tag::create([
            'id' => 2,
            'name' => 'cat',
        ]);

        Tag::create([
            'id' => 3,
            'name' => 'funny',
        ]);
    }
}

class UserTableSeeder extends Seeder {

    public function run()
    {
        DB::table('users')->delete();

        User::create([
            'id' => 1,
            'name' => 'Test User',
            'email' => 'test@user.ca',
            'password' => Hash::make('testing'),
            'birthday' => date('1999-12-31'),
            'admin' => 0,
        ]);

        User::create([
            'id' => 2,
            'name' => 'Patrick Hall',
            'email' => 'w0254442@nscc.ca',
            'password' => Hash::make('snoopy'),
            'birthday' => date('1993-4-25'),
            'admin' => 0,
        ]);

        User::create([
            'id' => 3,
            'name' => 'Marc',
            'email' => 'admin@test.ca',
            'password' => Hash::make('test'),
            'birthday' => date('1992-11-27'),
            'admin' => 1,
        ]);
    }
}

class CommentTableSeeder extends Seeder {

    public function run()
    {
        DB::table('comments')->delete();

        Comment::create([
            'id' => 1,
            'text' => 'First!',
            'video_id' => 1,
            'user_id' => 1,
        ]);

        Comment::create([
            'id' => 2,
            'text' => 'This is another comment.',
            'video_id' => 1,
            'user_id' => 2,
        ]);

        Comment::create([
            'id' => 3,
            'text' => 'This is a third comment.',
            'video_id' => 1,
            'user_id' => 3,
        ]);

        Comment::create([
            'id' => 4,
            'text' => 'This is a comment.',
            'video_id' => 2,
            'user_id' => 1,
        ]);

        Comment::create([
            'id' => 5,
            'text' => 'Second comment, this is.',
            'video_id' => 2,
            'user_id' => 2,
        ]);
    }
}

class TrackerTableSeeder extends Seeder {

    public function run()
    {
        DB::table('tracker')->delete();

        Tracker::create([
            'id' => 1,
            'key' => 'test',
            'date' => Carbon::today()->format('m/d/Y'),
        ]);

        Tracker::create([
            'id' => 2,
            'key' => 'butts',
            'date' => Carbon::today()->format('m/d/Y'),
        ]);

        Tracker::create([
            'id' => 3,
            'key' => 'testing',
            'date' => Carbon::today()->format('m/d/Y'),
        ]);

    }
}