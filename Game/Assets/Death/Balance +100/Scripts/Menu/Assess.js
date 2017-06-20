@script ExecuteInEditMode;

var No:boolean=false;
var Yes:boolean=false;

function OnMouseDown(){
if(No){
Application.Quit();
}
if(Yes){
PlayerPrefs.SetFloat('Evaluate',1);
Application.OpenURL("https://play.google.com/store/apps/details?id=com.BigBeam.ClashOfEnergies");
Application.Quit();
}
}