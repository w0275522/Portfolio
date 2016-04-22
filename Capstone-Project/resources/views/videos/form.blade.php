<div class="form-group">
    <div class="form-group">
        {!! Form::file('filename') !!}
    </div>

    <div class="form-group">
        {!! Form::label('title', 'Title:') !!}
        {!! Form::text('title', null, ['class' => 'form-control']) !!}
    </div>
    <div class="form-group">
        {!! Form::label('description', 'Description:') !!}
        {!! Form::text('description', null, ['class' => 'form-control']) !!}
    </div>
    <div class="form-group">
        {!! Form::label('thumbnail', 'Thumbnail:') !!}
        {!! Form::file('thumbnail') !!}
    </div>
    <div class="form-group">
        {!! Form::label('tag_list', 'Tags:') !!}
        {!! Form::select('tag_list[]', $tags, null, ['id' => 'tag_list', 'class' => 'form-control', 'multiple']) !!}
    </div>
    <div class="form-group">
        {!! Form::hidden('views', 0) !!}
    </div>
    <div class="form-group">
        {!! Form::submit('Upload Video', ['class' => 'btn btn-primary form-control']) !!}
    </div>
</div>

@section('footer')
    <script>
        $('#tag_list').select2({
            placeholder: 'Choose tags'
        });
    </script>
@endsection