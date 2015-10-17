//Widget_Controller: Handles Widget's movement and player input

//Widget's Movement Variables------------------------------------------------------------------
//These can be changed in the Inspector
var rollSpeed = 6.0;
var fastRollSpeed = 2.0;
var jumpSpeed = 8.0;
var gravity = 20.0;
var rotateSpeed = 4.0;
var duckSpeed = .5;

//private, helper variables-----------------------------------------------------------------------
private var moveDirection = Vector3.zero;
private var grounded : boolean = false;
private var moveHorz = 0.0;
private var normalHeight = 2.0;
private var duckHeight = 1.0;
private var rotateDirection = Vector3.zero;

private var isDucking : boolean = false;
private var isBoosting : boolean = false;

var isControllable : boolean = true;

//cache controller so we only have to find it once----------------------------------------------
var controller : CharacterController ;
controller = GetComponent(CharacterController);
var widgetStatus : Widget_Status;
widgetStatus = GetComponent(Widget_Status);

//Move the controller during the fixed frame updates------------
function FixedUpdate() {

	//check to make sure the character is controllable and not dead
	if(!isControllable)
		Input.ResetInputAxes();
	
	else{
		if (grounded) {
			// Since we're touching something solid, like the ground, allow movement
			//Calculate movement directly from Input Axes
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= rollSpeed;
			
			//Find rotation based upon axes if need to turn
			moveHorz = Input.GetAxis("Horizontal");
			if (moveHorz > 0)                                           //right turn
				rotateDirection = new Vector3(0, 1, 0);
			else if (moveHorz < 0)                                   //left turn
				rotateDirection = new Vector3(0, -1, 0);
			else                                                             //not turning
				rotateDirection = new Vector3 (0, 0, 0);
							
			//Jump Controls
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
			
			//Apply any Boosted Speed
			if(Input.GetButton("Boost")){
				if(widgetStatus){
					if(widgetStatus.energy > 0)
					{
						moveDirection *= fastRollSpeed;
						widgetStatus.energy -= widgetStatus.widgetBoostUsage *Time.deltaTime;
						isBoosting = true;
					}
				}
			}
			
			//Duck the controller
			if(Input.GetButton("Duck")){
				controller.height = duckHeight;
				controller.center.y = controller.height/2 + .25;
				moveDirection *= duckSpeed;
				isDucking = true;
			}
			
			if(Input.GetButtonUp("Duck")){
				controller.height = normalHeight; //reset for after ducks
				controller.center.y = controller.height/2;  //recenter for after ducks
				isDucking = false;
			}
			
			if(Input.GetButtonUp("Boost")){
				isBoosting = false;
			}
			
		}
	
		// Apply gravity to end Jump, enable falling, and make sure he's touching the ground
		moveDirection.y -= gravity * Time.deltaTime; 
		
		// Move and rotate the controller
		var flags = controller.Move(moveDirection * Time.deltaTime);  
		controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed);
		grounded = ((flags & CollisionFlags.CollidedBelow) != 0 );
		}
	} 
//---------------------------------------------------------------------------------------------------------------------------


function IsMoving(){

	return moveDirection.magnitude > 0.5;
}

function IsDucking(){
	
	return isDucking;
}

function IsBoosting(){

	return isBoosting;
}

function IsGrounded(){

	return grounded;
}


////Make the script easy to find 
@script AddComponentMenu("Player/Widget'sController")