//DamageTrigger.js: A simple, variable damage trigger which can be applied to any kind of object.  
//Change the damage amount in the Inspector. 

var damage: float = 20.0;
var playerStatus : Widget_Status;

function OnTriggerEnter(){
	print("ow!");
	playerStatus = GameObject.FindWithTag("Player").GetComponent(Widget_Status);
	playerStatus.ApplyDamage(damage);
}

@script AddComponentMenu("Environment Props/DamageTrigger")