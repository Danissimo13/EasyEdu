$(function(){
    $('.card').on('click', function(e){
        $('.hidden-content').css({
            'top':'0px',
            'opacity': '1',
        });
        e.stopPropagation();
	});

	$(".bottomBtn").click(function(e) {
        $('.hidden-content').css({
            'top': '480px',
            'opacity' : '0',
        });
        e.stopPropagation();
	});
});
