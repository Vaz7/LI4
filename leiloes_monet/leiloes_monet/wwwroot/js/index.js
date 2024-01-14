var w = 800,
    h = 1200,
    offsetX = $('#imgBox').offset().left,
    scale = 1.5,
    scaling = false;

$('#imgBox').click(function(e){ 
  scaling = true;
  (scale<3.5) ? scale=scale*1.33 : scale=1.5;
  TweenMax.to('#imgZoom', 0.1, {attr:{width:w*scale, height:h*scale, x:offsetX*(scale-1)-e.pageX*(scale-1), y:-e.pageY*(scale-1)}, onComplete:function(){scaling=false;}});
});

$('#imgBox').mousemove(function(e){
  if (!scaling){
    TweenMax.to('#mag',     0.3, {x:-offsetX+e.pageX, y:e.pageY});
    TweenMax.to('#masker', 	0.3, {attr:{cx:-offsetX+e.pageX, cy:e.pageY}});
    TweenMax.to('#imgZoom', 0.3, {attr:{x:offsetX*(scale-1)-e.pageX*(scale-1), y:-e.pageY*(scale-1)}});
  }
});

$('#imgBox').mouseenter(function(e){
  TweenMax.to(['#mag','#masker','#imgZoom'], 0.3, {alpha:1});
});

$('#imgBox').mouseleave(function(e){
  TweenMax.to(['#mag','#masker','#imgZoom'], 0.3, {alpha:0});
});

$( window ).resize(function(){
  offsetX = $('#imgBox').offset().left;
});