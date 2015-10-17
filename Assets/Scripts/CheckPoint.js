//Checkpoint.js: checkpoints in the level - active for the last selected one and first for the initial one at startup
//the static declaration makes the isActivePt variable global across all instances of this script in the game.

static var isActivePt : CheckPoint;
var firstPt : CheckPoint;

//special effects var
var activeEmitter : ParticleEmitter;

//audio var

var playerStatus : Widget_Status;
playerStatus = GameObject.FindWithTag("Player").GetComponent(Widget_Status);

function Start()
{
	//initilize first point
	isActivePt = firstPt;
	
	if(isActivePt == this){
		BeActive();
	}
}

//When the player encounters a point, this is called when the collision occurs
function OnTriggerEnter(){

	//first turn off the old respawn point if this is a newly encountered one
	if(isActivePt != this){
		isActivePt.BeInactive();
	
		//then set the new one
		isActivePt = this;
		BeActive();
	}
	playerStatus.AddHealth(playerStatus.maxHealth);
	playerStatus.AddEnergy(playerStatus.maxEnergy);
	//print("Player stepped on me");
}


//calls all the FX and audio to make the triggered point "activate" visually
function BeActive()
{
activeEmitter.emit = true;
}

//calls all the FX and audio to make any old triggered point "inactivate" visually
function BeInactive()
{
activeEmitter.emit = false;
}
@script AddComponentMenu("Environment Props/CheckPt")