@script ExecuteInEditMode;

var Stars:GameObject;
private var SaveEffects:float;
var ObjectsLoop:boolean=false;


function FixedUpdate(){
if(ObjectsLoop){
SaveEffects=(PlayerPrefs.GetFloat('SaveEffects') );
if(SaveEffects==0){
Stars.SetActive(false);
}else{
Stars.SetActive(true);
}
}else{
SaveEffects=(PlayerPrefs.GetFloat('SaveEffects') );
if(SaveEffects==0){
Stars.layer=8;
}else{
Stars.layer=0;
}
}
}