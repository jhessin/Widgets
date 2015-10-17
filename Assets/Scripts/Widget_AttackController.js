//Widget_AttackController:  handles the player's attack input and deals damage to the targeted enemy

//----------------------------------------------------------------
var attackHitTime = 0.2;
var attackTime = 0.5;
var attackPosition = new Vector3 (0, 1, 0);
var attackRadius = 2.0;
var damage= 1.0;

var attackEmitter : ParticleSystem;
var attackSound : AudioClip;

//---------------------------------------------------------------
private var busy = false; 
private var ourLocation;
private var enemies : GameObject[] ;

var controller : Widget_Controller;
controller = GetComponent(Widget_Controller); 

//Allow the player to attack if he is not busy and the attack button was pressed
function Update ()
{
	if(!busy && Input.GetButtonDown ("Attack") && controller.IsGrounded() && !controller.IsMoving())
	{	
		DidAttack();
		busy = true;
	}
}

function DidAttack ()
{
	//Play the animation regardless of whether we hit something or not.
	animation.CrossFadeQueued("Taser", 0.1, QueueMode.PlayNow);
	yield WaitForSeconds(attackHitTime);
	
	//Play effects
	PlayParticles();
	if(attackSound)
	{
		audio.clip = attackSound;
		audio.Play();
	}
	ourLocation = transform.TransformPoint(attackPosition);
	enemies = GameObject.FindGameObjectsWithTag("Enemy");
	//See if any enemies are within range of the attack.  This will hit all in range.
	for (var enemy : GameObject in enemies)
	{
		var enemyStatus = enemy.GetComponent(EBunny_Status);
		if (enemyStatus == null)
		{
			continue;
		}
			
		if (Vector3.Distance(enemy.transform.position, ourLocation) < attackRadius)
		{
			//apply damage for hitting
			enemyStatus.ApplyDamage(damage);
		}
	}
	yield WaitForSeconds(attackTime - attackHitTime);
	busy = false;
}

function GetClosestEnemy() : GameObject
{
	enemies = GameObject.FindGameObjectsWithTag("Enemy");
	var distanceToEnemy = Mathf.Infinity;
	var wantedEnemy;

	for (var enemy : GameObject in enemies)
		{
			newDistanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
			if (newDistanceToEnemy < distanceToEnemy)
			{
				distanceToEnemy = newDistanceToEnemy;
				wantedEnemy = enemy;
			}
		}
	return wantedEnemy;
}

function PlayParticles()
{
	attackEmitter.Play();
	yield WaitForSeconds(attackEmitter.duration );
	attackEmitter.Stop();
}

@script AddComponentMenu("Player/Widget's Attack Controller")