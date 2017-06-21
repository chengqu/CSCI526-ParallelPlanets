@script ExecuteInEditMode;

var ApLevels:int;
var NawSave:int;
var Finish:GameObject;
private var SaveAp:int;

private var stop:int=0;
function Start(){
SaveAp=ApLevels;
gameObject.layer=0;
}

function FixedUpdate () {
if(stop==0){
ApLevels=SaveAp-NawSave;
if(ApLevels==0){
stop=1;
var CreatEnd:PortFinish=Finish.GetComponent(PortFinish);CreatEnd.InformatorLoad=1;
}
}
}


