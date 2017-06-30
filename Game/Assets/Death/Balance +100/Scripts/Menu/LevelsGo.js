@script ExecuteInEditMode;

private var TapLevel:boolean=true;
var LoadThisLevel:int;

var Lelels_1:GameObject;
var Glevels_1:SpriteRenderer;
var LevelsSprGrid_1:SpriteRenderer;
var Dop_1_1:SpriteRenderer;
var Dop_1_2:SpriteRenderer;
var Dop_1_3:SpriteRenderer;
var Dop_1_4:SpriteRenderer;
var Dop_1_5:SpriteRenderer;
var Dop_1_6:SpriteRenderer;
var Dop_1_7:SpriteRenderer;
var Dop_1_8:SpriteRenderer;
var Dop_1_9:SpriteRenderer;
var Dop_1_10:SpriteRenderer;
var Dop_1_11:SpriteRenderer;
var Dop_1_12:SpriteRenderer;
var Dop_1_13:SpriteRenderer;
var Dop_1_14:SpriteRenderer;
var Dop_1_15:SpriteRenderer;
var Dop_1_16:SpriteRenderer;

var Lelels_2:GameObject;
var Glevels_2:SpriteRenderer;
var LevelsSprGrid_2:SpriteRenderer;
var Dop_2_1:SpriteRenderer;
var Dop_2_2:SpriteRenderer;
var Dop_2_3:SpriteRenderer;
var Dop_2_4:SpriteRenderer;
var Dop_2_5:SpriteRenderer;
var Dop_2_6:SpriteRenderer;
var Dop_2_7:SpriteRenderer;
var Dop_2_8:SpriteRenderer;
var Dop_2_9:SpriteRenderer;
var Dop_2_10:SpriteRenderer;
var Dop_2_11:SpriteRenderer;
var Dop_2_12:SpriteRenderer;
var Dop_2_13:SpriteRenderer;
var Dop_2_14:SpriteRenderer;
var Dop_2_15:SpriteRenderer;
var Dop_2_16:SpriteRenderer;

var Lelels_3:GameObject;
var Glevels_3:SpriteRenderer;
var LevelsSprGrid_3:SpriteRenderer;
var Dop_3_1:SpriteRenderer;
var Dop_3_2:SpriteRenderer;
var Dop_3_3:SpriteRenderer;
var Dop_3_4:SpriteRenderer;
var Dop_3_5:SpriteRenderer;
var Dop_3_6:SpriteRenderer;
var Dop_3_7:SpriteRenderer;
var Dop_3_8:SpriteRenderer;
var Dop_3_9:SpriteRenderer;
var Dop_3_10:SpriteRenderer;
var Dop_3_11:SpriteRenderer;
var Dop_3_12:SpriteRenderer;
var Dop_3_13:SpriteRenderer;
var Dop_3_14:SpriteRenderer;
var Dop_3_15:SpriteRenderer;
var Dop_3_16:SpriteRenderer;

var Lelels_4:GameObject;
var Glevels_4:SpriteRenderer;
var LevelsSprGrid_4:SpriteRenderer;
var Dop_4_1:SpriteRenderer;
var Dop_4_2:SpriteRenderer;
var Dop_4_3:SpriteRenderer;
var Dop_4_4:SpriteRenderer;
var Dop_4_5:SpriteRenderer;
var Dop_4_6:SpriteRenderer;
var Dop_4_7:SpriteRenderer;
var Dop_4_8:SpriteRenderer;
var Dop_4_9:SpriteRenderer;
var Dop_4_10:SpriteRenderer;
var Dop_4_11:SpriteRenderer;
var Dop_4_12:SpriteRenderer;
var Dop_4_13:SpriteRenderer;
var Dop_4_14:SpriteRenderer;
var Dop_4_15:SpriteRenderer;
var Dop_4_16:SpriteRenderer;

var Next:GameObject;
var GNext:SpriteRenderer;
var Back:GameObject;
var GBack:SpriteRenderer;
var Load:SpriteRenderer;

private var pusk:int;
private var coll:int;

function OnMouseDown () {
if(Glevels_1.color.a>0.97||Glevels_2.color.a>0.97||Glevels_3.color.a>0.97||Glevels_4.color.a>0.97){
if (TapLevel) {
pusk=1;
}
}
}


function FixedUpdate(){
if(pusk==1){
if(Dop_1_1.color.a>0||Dop_2_1.color.a>0||Dop_3_1.color.a>0||Dop_4_1.color.a>0){
if(coll==0){
coll=1;
Back.GetComponent(BoxCollider2D).enabled=false;
Next.GetComponent(BoxCollider2D).enabled=false;
}
Dop_1_1.color.a=Dop_1_1.color.a-0.1;
Dop_1_2.color.a=Dop_1_2.color.a-0.1;
Dop_1_3.color.a=Dop_1_3.color.a-0.1;
Dop_1_4.color.a=Dop_1_4.color.a-0.1;
Dop_1_5.color.a=Dop_1_5.color.a-0.1;
Dop_1_6.color.a=Dop_1_6.color.a-0.1;
Dop_1_7.color.a=Dop_1_7.color.a-0.1;
Dop_1_8.color.a=Dop_1_8.color.a-0.1;
Dop_1_9.color.a=Dop_1_9.color.a-0.1;
Dop_1_10.color.a=Dop_1_10.color.a-0.1;
Dop_1_11.color.a=Dop_1_11.color.a-0.1;
Dop_1_12.color.a=Dop_1_12.color.a-0.1;
Dop_1_13.color.a=Dop_1_13.color.a-0.1;
Dop_1_14.color.a=Dop_1_14.color.a-0.1;
Dop_1_15.color.a=Dop_1_15.color.a-0.1;
Dop_1_16.color.a=Dop_1_16.color.a-0.1;

Dop_2_1.color.a=Dop_2_1.color.a-0.1;
Dop_2_2.color.a=Dop_2_2.color.a-0.1;
Dop_2_3.color.a=Dop_2_3.color.a-0.1;
Dop_2_4.color.a=Dop_2_4.color.a-0.1;
Dop_2_5.color.a=Dop_2_5.color.a-0.1;
Dop_2_6.color.a=Dop_2_6.color.a-0.1;
Dop_2_7.color.a=Dop_2_7.color.a-0.1;
Dop_2_8.color.a=Dop_2_8.color.a-0.1;
Dop_2_9.color.a=Dop_2_9.color.a-0.1;
Dop_2_10.color.a=Dop_2_10.color.a-0.1;
Dop_2_11.color.a=Dop_2_11.color.a-0.1;
Dop_2_12.color.a=Dop_2_12.color.a-0.1;
Dop_2_13.color.a=Dop_2_13.color.a-0.1;
Dop_2_14.color.a=Dop_2_14.color.a-0.1;
Dop_2_15.color.a=Dop_2_15.color.a-0.1;
Dop_2_16.color.a=Dop_2_16.color.a-0.1;

Dop_3_1.color.a=Dop_3_1.color.a-0.1;
Dop_3_2.color.a=Dop_3_2.color.a-0.1;
Dop_3_3.color.a=Dop_3_3.color.a-0.1;
Dop_3_4.color.a=Dop_3_4.color.a-0.1;
Dop_3_5.color.a=Dop_3_5.color.a-0.1;
Dop_3_6.color.a=Dop_3_6.color.a-0.1;
Dop_3_7.color.a=Dop_3_7.color.a-0.1;
Dop_3_8.color.a=Dop_3_8.color.a-0.1;
Dop_3_9.color.a=Dop_3_9.color.a-0.1;
Dop_3_10.color.a=Dop_3_10.color.a-0.1;
Dop_3_11.color.a=Dop_3_11.color.a-0.1;
Dop_3_12.color.a=Dop_3_12.color.a-0.1;
Dop_3_13.color.a=Dop_3_13.color.a-0.1;
Dop_3_14.color.a=Dop_3_14.color.a-0.1;
Dop_3_15.color.a=Dop_3_15.color.a-0.1;
Dop_3_16.color.a=Dop_3_16.color.a-0.1;

Dop_4_1.color.a=Dop_4_1.color.a-0.1;
Dop_4_2.color.a=Dop_4_2.color.a-0.1;
Dop_4_3.color.a=Dop_4_3.color.a-0.1;
Dop_4_4.color.a=Dop_4_4.color.a-0.1;
Dop_4_5.color.a=Dop_4_5.color.a-0.1;
Dop_4_6.color.a=Dop_4_6.color.a-0.1;
Dop_4_7.color.a=Dop_4_7.color.a-0.1;
Dop_4_8.color.a=Dop_4_8.color.a-0.1;
Dop_4_9.color.a=Dop_4_9.color.a-0.1;
Dop_4_10.color.a=Dop_4_10.color.a-0.1;
Dop_4_11.color.a=Dop_4_11.color.a-0.1;
Dop_4_12.color.a=Dop_4_12.color.a-0.1;
Dop_4_13.color.a=Dop_4_13.color.a-0.1;
Dop_4_14.color.a=Dop_4_14.color.a-0.1;
Dop_4_15.color.a=Dop_4_15.color.a-0.1;
Dop_4_16.color.a=Dop_4_16.color.a-0.1;
}else{
if(Glevels_1.color.a>0||Glevels_2.color.a>0||Glevels_3.color.a>0||Glevels_4.color.a>0){
Glevels_1.color.a=Glevels_1.color.a-0.1;
LevelsSprGrid_1.color.a=LevelsSprGrid_1.color.a-0.1;
Glevels_2.color.a=Glevels_2.color.a-0.1;
LevelsSprGrid_2.color.a=LevelsSprGrid_2.color.a-0.1;
Glevels_3.color.a=Glevels_3.color.a-0.1;
LevelsSprGrid_3.color.a=LevelsSprGrid_3.color.a-0.1;
Glevels_4.color.a=Glevels_4.color.a-0.1;
LevelsSprGrid_4.color.a=LevelsSprGrid_4.color.a-0.1;
GNext.color.a=GNext.color.a-0.1;
GBack.color.a=GBack.color.a-0.1;
}else{
if(Load.color.a<1){
Load.color.a=Load.color.a+0.1;
}else{
if(coll==1){
coll=0;
Lelels_1.SetActive(false);
Lelels_2.SetActive(false);
Lelels_3.SetActive(false);
Lelels_4.SetActive(false);
}
pusk=0;
Application.LoadLevel(LoadThisLevel);
}
}
}
}
}