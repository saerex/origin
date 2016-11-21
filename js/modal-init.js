(function(){
    var $content = $('share-options').detach();
    $('#click-image').on('click', function(e){
      var path = $(this).attr('src');
      console.log(path);
        e.preventDefault();
        modal.open({content: path, width:340, height: 300});
    });
}());