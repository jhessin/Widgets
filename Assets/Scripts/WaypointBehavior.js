//WaypointBehavior:  Handles the scripts on all the waypoints
// Calls the respawn update, and give the player the option of opening the store or not. 

var customSkin: GUISkin;
private var isTriggered  = false;

function OnTriggerEnter(collider: Collider){

	//make sure that this is a player hitting the platform and not an enemy
	var playerStatus : Widget_Status = collider.GetComponent(Widget_Status);
	if(playerStatus == null) return;
	
	isTriggered = true;
	
	playerStatus.energy = playerStatus.maxEnergy;
	playerStatus.health = playerStatus.maxHealth;
	
}

function OnTriggerExit(collider: Collider){
	
	//make sure that this is a player leaving the platform and not an enemy
	var playerStatus : Widget_Status = collider.GetComponent(Widget_Status);
	if(playerStatus == null) return;
	
	isTriggered = false;

}

function OnGUI(){
	if(customSkin)
		GUI.skin = customSkin;

	if(isTriggered)
	{
		var store = this.GetComponent(GUI_WaypointStore);
		
		//Only display open button if store is currently closed
		if( !store.GetStoreStatus() ) 
		{
			if(GUI.Button(Rect(Screen.width/2 -126 , Screen.height - 100 , 252, 113), "Open  Store")){			
				store.StoreFrontToggle();
			}
		}
	}

}

@script AddComponentMenu("GUI/WaypointGUI")