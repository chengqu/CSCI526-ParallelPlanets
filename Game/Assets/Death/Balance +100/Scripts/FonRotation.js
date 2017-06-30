@script ExecuteInEditMode;

var speed_roation:float;

function FixedUpdate(){
gameObject.transform.Rotate(0,0,speed_roation);
}