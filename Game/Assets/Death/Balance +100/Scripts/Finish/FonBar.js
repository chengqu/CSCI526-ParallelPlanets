@script ExecuteInEditMode;

var FonStart:SpriteRenderer;


function FixedUpdate () {
if(FonStart.color.a>0){
FonStart.color.a=FonStart.color.a-0.037;
}
}

