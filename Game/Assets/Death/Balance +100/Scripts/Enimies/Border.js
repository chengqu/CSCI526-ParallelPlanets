@script ExecuteInEditMode;

private var BlokEffects:int=0;

var DelHP:float;
var ObjectCenter:GameObject;

function OnTriggerEnter(other:Collider) {
if (other.gameObject.name=="Player"){
var Delet:PlayerHPInt=ObjectCenter.GetComponent(PlayerHPInt);
Delet.HitsDeleting=DelHP;
}
}
function OnTriggerExit(other:Collider) {
if (other.gameObject.name=="Player"){
var Delet:PlayerHPInt=ObjectCenter.GetComponent(PlayerHPInt);
Delet.HitsDeleting=0;
}
}