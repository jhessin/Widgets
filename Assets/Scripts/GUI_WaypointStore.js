//GUI_WaypointStore : handles the interface for the store transactions
//shows the available items for purchase, allows the player to sell his own inventory, and buy transforms

var storeBG : Texture2D;
var customSkin: GUISkin;

private var playerInventory : Widget_Inventory;
private var openStore = false;


//---------------------------------------------------------------------------
function Awake(){
	playerInventory = FindObjectOfType(Widget_Inventory);
	if (!playerInventory)
		Debug.Log("No link to player's inventory.");

}


//enable and disable script as needed--------------------------------
function OnGUI(){
	if(customSkin)
		GUI.skin = customSkin;

	if(openStore){
		GUI.Box(Rect(0,0,Screen.width, Screen.height), " ");
		GUI.Label(Rect(Screen.width/2 - storeBG.width/2, Screen.height/2- storeBG.height/2, storeBG.width, storeBG.height ), storeBG);
		
		if(GUI.Button(Rect(Screen.width/2 -126 , Screen.height - 100 , 252, 113), "Close  Store")){
			StoreFrontToggle();
		}
	 }

}


function StoreFrontToggle(){

	if(openStore == false)
		openStore = true;
	else openStore = false;
}

function GetStoreStatus(){
	return openStore;
}

@script AddComponentMenu("GUI/Store")