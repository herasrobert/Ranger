#pragma strict

var target : Transform;
var distance : float;
	
function Start () {

}

 function Update(){ 
    //transform.position.z = target.position.z -distance;
	transform.position.y = target.position.y; // Follow Bottle Y Pos
    transform.position.x = target.position.x; // Follow Bottle X Pos 
 }