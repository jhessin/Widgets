//Widget_Animation: Animation State Manager for Widget.
//controls layers, blends, and play cues for all imported animations

private var nextPlayIdle = 0.0;
var waitTime = 10.0;

var playerController: Widget_Controller; 
playerController = GetComponent(Widget_Controller) ;

//Initialize and set up all imported animations with proper layers-------------------------------
function Start(){

//set up layers - high numbers receive priority when blending
	GetComponent.<Animation>()["Widget_Idle"].layer = -1;
	GetComponent.<Animation>()["Idle"].layer = 0;
	
	//we want ot make sure that the rolls are synced together
	GetComponent.<Animation>()["SlowRoll"].layer = 1;
	GetComponent.<Animation>()["FastRoll"].layer = 1;
	GetComponent.<Animation>()["Duck"].layer = 1;
	GetComponent.<Animation>().SyncLayer(1);
	
	GetComponent.<Animation>()["Taser"].layer = 3;
	GetComponent.<Animation>()["Jump"].layer = 5;
		
	//these should take priority over all others
	GetComponent.<Animation>()["FallDown"].layer = 7;
	GetComponent.<Animation>()["GotHit"].layer = 8;
	GetComponent.<Animation>()["Die"].layer = 10;
	
	GetComponent.<Animation>()["Widget_Idle"].wrapMode = WrapMode.PingPong;
	GetComponent.<Animation>()["Duck"].wrapMode = WrapMode.Loop;
	GetComponent.<Animation>()["Jump"].wrapMode = WrapMode.ClampForever;
	GetComponent.<Animation>()["FallDown"].wrapMode = WrapMode.ClampForever;
	
	//Make sure nothing is playing by accident, then start with a default idle.
	GetComponent.<Animation>().Stop();
	GetComponent.<Animation>().Play("Idle");
	
}

//Check for which animation to play-----------------------------------------------------------------
function Update(){

	//on the ground animations
	if(playerController.IsGrounded()){
	
		GetComponent.<Animation>().Blend("FallDown", 0, 0.2);
		GetComponent.<Animation>().Blend("Jump", 0, 0.2);

		//if boosting
		if (playerController.IsBoosting())
		{
			GetComponent.<Animation>().CrossFade("FastRoll", 0.5);
			nextPlayIdle = Time.time + waitTime;
		}
		
		else if(playerController.IsDucking()){
			
			GetComponent.<Animation>().CrossFade("Duck", 0.2);
			nextPlayIdle = Time.time + waitTime;
		}

		// Fade in normal roll
		else if (playerController.IsMoving())
		{
			GetComponent.<Animation>().CrossFade("SlowRoll", 0.5);
			nextPlayIdle = Time.time + waitTime;
		}
		// Fade out walk and run
		else
		{
			GetComponent.<Animation>().Blend("FastRoll", 0.0, 0.3);
			GetComponent.<Animation>().Blend("SlowRoll", 0.0, 0.3);
			GetComponent.<Animation>().Blend("Duck", 0.0, 0.3);
			if(Time.time > nextPlayIdle){
				nextPlayIdle= Time.time + waitTime;
				PlayIdle();
			}
			else
				GetComponent.<Animation>().CrossFade("Widget_Idle", 0.2);
		}
		
	}
	
	//in air animations
	else{
		if(Input.GetButtonDown("Jump")){
		
			GetComponent.<Animation>().CrossFade("Jump");
		}
		
		if(!playerController.IsGrounded()){
		
			GetComponent.<Animation>().CrossFade("FallDown", 0.5);	
		}
	}
	
	//test for idle
	if(Input.anyKey){
	
		nextPlayIdle = Time.time + waitTime;
	}
}

//Other functions----------------------------------------------------------------------------------
function PlayTaser(){

	GetComponent.<Animation>().CrossFade("Taser", 0.2);
}

function PlayIdle(){

	GetComponent.<Animation>().CrossFade("Idle", 0.2);
}

function GetHit(){

	GetComponent.<Animation>().CrossFade("GotHit", 0.2);
}

function PlayDie(){

	GetComponent.<Animation>().CrossFade("Die", 0.2);
}
	


@script AddComponentMenu("Player/Widget'AnimationManager")