//Widget_Status: Handles Widget's state machine.
//Keep track of health, energy and all the chunky stuff

//vitals---------------------------------------------------------------------------
var health: float = 10.0;
var maxHealth: float= 10.0;
var energy: float = 10.0;
var maxEnergy: float = 10.0;
var energyUsageForTransform: float = 3.0;
var widgetBoostUsage :float = 5.0;

//Sound Effects-----------------------------------------------------------------
var hitSound: AudioClip;
var deathSound: AudioClip;


//Cache Controllers---------------------------------------------------------------------------
var playerController: Widget_Controller; 
playerController = GetComponent(Widget_Controller) ;
var controller : CharacterController;
controller = GetComponent(CharacterController);



//Helper Controller Functions---------------------------------------------------
function ApplyDamage(damage: float){
	
	health -= damage;
	
	//play hit sound if it exists
	if(hitSound)
	{
		GetComponent.<AudioSource>().clip = hitSound;
		GetComponent.<AudioSource>().Play();
	}
	//check health and call Die if need to
	if(health <= 0){
		health = 0; //for GUI
		Die();	
	}

}

function AddHealth(boost: float){
	//add health and set to min of (current health+boost) or health max
	health += boost;
	if(health >= maxHealth){
		health = maxHealth;
	}
	print("added health: " + health);
}

function AddEnergy(boost: float){
	//add energy and set to nim of (current en + boost) or en max
	energy += boost;
	if(energy >= maxEnergy){
		energy = maxEnergy;
	}
	print("added energy: " + energy);
}

function Die(){
	//play death sound if it exists
	if(deathSound)
	{
		GetComponent.<AudioSource>().clip = deathSound;
		GetComponent.<AudioSource>().Play();
	}
	print("dead!");
	playerController.isControllable = false;
	
	animationState = GetComponent(Widget_Animation);
	animationState.PlayDie();
	yield WaitForSeconds(GetComponent.<Animation>()["Die"].length -0.2);
	HideCharacter();
	
	yield WaitForSeconds(1);
	
	//restart player at last respawn check point and give max life
	if(CheckPoint.isActivePt){
		controller.transform.position = CheckPoint.isActivePt.transform.position;
		controller.transform.position.y += 0.5;   //so not to get stuck in the platform itself
	}
	ShowCharacter();
	health = maxHealth;
}

function HideCharacter(){
	GameObject.Find("Body").GetComponent(SkinnedMeshRenderer).enabled = false;
	GameObject.Find("Wheels").GetComponent(SkinnedMeshRenderer).enabled = false;
	playerController.isControllable = false;
	
}

function ShowCharacter(){
	GameObject.Find("Body").GetComponent(SkinnedMeshRenderer).enabled = true;
	GameObject.Find("Wheels").GetComponent(SkinnedMeshRenderer).enabled = true;
	playerController.isControllable = true;
	
}

@script AddComponentMenu("Player/Widget'sStateManager")