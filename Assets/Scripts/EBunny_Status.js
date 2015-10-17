//EBunny_Status:  controls the state information of the enemy bunny

//----------------------------------------------------------------------------------
var health: float = 10.0;
var energy: float = 10.0;
private var dead = false;

var charImage : Texture2D;
var explosion : ParticleEmitter;
var smoke : ParticleEmitter;

//-------------------------------------------------------------------------------
var deathSound : AudioClip;

//PickupItems held---------------------------------------------------------------
var numHeldItemsMin = 1;
var numHeldItemsMax = 3;
var pickup1: GameObject;
var pickup2: GameObject;

//State Functions---------------------------------------------------
function ApplyDamage(damage: float){
	
	if (health <= 0)
		return;

	health -= damage;
	
	animation.Play("EBunny_Hit");

	//check health and call Die if need to
	if(!dead && health <= 0)
	{
		health = 0; //for GUI
		dead = true;
		Die();		
	}
}

function Die ()
{	
	
	animation.Stop();
	animation.Play("EBunny_Death");
	
	Destroy(gameObject.GetComponent(EBunny_AIController));
	yield WaitForSeconds(animation["EBunny_Death"].length - 0.5);
	
	//Play effects
	PlayEffects();
	if(deathSound)
	{
		audio.clip = deathSound;
		audio.Play();
	}
		
	//Cache location of dead body for pickups
	var itemLocation = gameObject.transform.position;
	
	// drop a random number of reward pickups for the player
	yield WaitForSeconds(5);
	var rewardItems = Random.Range(numHeldItemsMin, numHeldItemsMax) + 1;	
	
	for (var i = 0; i < rewardItems; i++)
	{
		var randomItemLocation = itemLocation;
		randomItemLocation.x += Random.Range(-2, 2);
		randomItemLocation.y += 1; // Keep it off the ground
		randomItemLocation.z += Random.Range(-2, 2);

		if (Random.value > 0.5)
			Instantiate(pickup1, randomItemLocation, pickup1.transform.rotation);
		else
			Instantiate(pickup2, randomItemLocation, pickup2.transform.rotation);
	}
	
	//Remove killed enemy from the scene
	Destroy(gameObject);
}

function IsDead() : boolean
{
	return dead;
}
function GetCharImage(): Texture2D
{
	return charImage;
}
function PlayEffects()
{
	explosion.emit = true;
	smoke.emit = true;
	yield WaitForSeconds(.5);
	GameObject.Find("root").GetComponent(SkinnedMeshRenderer).enabled = false;
	yield WaitForSeconds(.5);
	explosion.emit = false;
	yield WaitForSeconds(3.5);
	smoke.emit = false;
}

@script AddComponentMenu("Enemies/Bunny'sStateManager")