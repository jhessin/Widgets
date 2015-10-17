//Widget_Animation: Animation State Manager for Widget.
//controls layers, blends, and play cues for all imported animations

private var nextPlayIdle = 0.0;
var waitTime = 10.0;

var playerController: Widget_Controller; 
playerController = GetComponent(Widget_Controller) ;

//Initialize and set up all imported animations with proper layers-------------------------------
function Start(){

//set up layers - high numbers receive priority when blending
	animation["Widget_Idle"].layer = -1;
	animation["Idle"].layer = 0;
	
	//we want ot make sure that the rolls are synced together
	animation["SlowRoll"].layer = 1;
	animation["FastRoll"].layer = 1;
	animation["Duck"].layer = 1;
	animation.SyncLayer(1);
	
	animation["Taser"].layer = 3;
	animation["Jump"].layer = 5;
		
	//these should take priority over all others
	animation["FallDown"].layer = 7;
	animation["GotHit"].layer = 8;
	animation["Die"].layer = 10;
	
	animation["Widget_Idle"].wrapMode = WrapMode.PingPong;
	animation["Duck"].wrapMode = WrapMode.Loop;
	animation["Jump"].wrapMode = WrapMode.ClampForever;
	animation["FallDown"].wrapMode = WrapMode.ClampForever;
	
	//Make sure nothing is playing by accident, then start with a default idle.
	animation.Stop();
	animation.Play("Idle");
	
}

//Check for which animation to play-----------------------------------------------------------------
function Update(){

	//on the ground animations
	if(playerController.IsGrounded()){
	
		animation.Blend("FallDown", 0, 0.2);
		animation.Blend("Jump", 0, 0.2);

		//if boosting
		if (playerController.IsBoosting())
		{
			animation.CrossFade("FastRoll", 0.5);
			nextPlayIdle = Time.time + waitTime;
		}
		
		else if(playerController.IsDucking()){
			
			animation.CrossFade("Duck", 0.2);
			nextPlayIdle = Time.time + waitTime;
		}

		// Fade in normal roll
		else if (playerController.IsMoving())
		{
			animation.CrossFade("SlowRoll", 0.5);
			nextPlayIdle = Time.time + waitTime;
		}
		// Fade out walk and run
		else
		{
			animation.Blend("FastRoll", 0.0, 0.3);
			animation.Blend("SlowRoll", 0.0, 0.3);
			animation.Blend("Duck", 0.0, 0.3);
			if(Time.time > nextPlayIdle){
				nextPlayIdle= Time.time + waitTime;
				PlayIdle();
			}
			else
				animation.CrossFade("Widget_Idle", 0.2);
		}
		
	}
	
	//in air animations
	else{
		if(Input.GetButtonDown("Jump")){
		
			animation.CrossFade("Jump");
		}
		
		if(!playerController.IsGrounded()){
		
			animation.CrossFade("FallDown", 0.5);	
		}
	}
	
	//test for idle
	if(Input.anyKey){
	
		nextPlayIdle = Time.time + waitTime;
	}
}

//Other functions----------------------------------------------------------------------------------
function PlayTaser(){

	animation.CrossFade("Taser", 0.2);
}

function PlayIdle(){

	animation.CrossFade("Idle", 0.2);
}

function GetHit(){

	animation.CrossFade("GotHit", 0.2);
}

function PlayDie(){

	animation.CrossFade("Die", 0.2);
}
	


@script AddComponentMenu("Player/Widget'AnimationManager")