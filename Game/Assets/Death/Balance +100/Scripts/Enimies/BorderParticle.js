@script ExecuteInEditMode;

private var BlokEffects:int=0;
var ThisLine:boolean=false;
var DelHP:float;
var ObjectCenter:GameObject;
var ObjectPlayer:GameObject;

function OnParticleCollision(other: GameObject) {
	if (other.gameObject.name=="Player"){
		if (BlokEffects==0){
			OffEffects();
			if(ThisLine){
				var Freeze:PlayerTrans=ObjectPlayer.GetComponent(PlayerTrans);
				Freeze.SpeedZero=1;
				var Delet:PlayerHPInt=ObjectCenter.GetComponent(PlayerHPInt);
				Delet.HitsDeleting=Delet.HitsDeleting+DelHP;
			}else{
				var Delete:PlayerHPInt=ObjectCenter.GetComponent(PlayerHPInt);
				Delete.HitsDeleting=DelHP;
			}
		}
	}
}



function OffEffects() {
	BlokEffects=1;
	yield WaitForSeconds(0.03);
	BlokEffects=0;
	if(ThisLine==false){
		var Delet:PlayerHPInt=ObjectCenter.GetComponent(PlayerHPInt);
		Delet.HitsDeleting=0;
	}
}



