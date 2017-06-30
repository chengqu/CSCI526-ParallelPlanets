@script ExecuteInEditMode;

var BlurSprite:SpriteRenderer;//AlphaChanel=0;
var StarsGray:GameObject;
private var noloop:int=0;

function FixedUpdate(){
if(noloop==0){
if(BlurSprite.color.a<1){
BlurSprite.color.a=BlurSprite.color.a+0.017;
}else{
noloop=1;
StarsGray.SetActive(false);
}
}
}