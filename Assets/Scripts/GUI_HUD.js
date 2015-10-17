//GUI_HUD: displays the pertinant information for Widget, his items, and any current enemy

//Set up Textures-------------------------------------------------------------------
//For larger games, this should be done programmatically
var customSkin: GUISkin;

var screwImage : Texture2D;
var gearImage : Texture2D;
var repairkitImage : Texture2D;
var energykitImage : Texture2D;

//Left Vital Tex
var lbarImage: Texture2D;
var lhbar :Texture2D;
var lebar : Texture2D;
var widgetImage : Texture2D;

//Right Vital Tex
var rbarImage: Texture2D;
var rhbar :Texture2D;
var rebar : Texture2D;
var enemyImage : Texture2D;
var circBackImage : Texture2D;

//---------------------------------------------------------------------------------

private var customControls : GUI_CustomControls;
private var playerInfo : Widget_Status;
private var playerInvo : Widget_Inventory;
private var playerAttack : Widget_AttackController;
private var closestEnemyStatus ;
private var player;

var closestEnemy;
var enemyDistance;

//Initialize Player info-------------------------------------------------------------
function Start()
{
	playerInfo = FindObjectOfType(Widget_Status);
	customControls = FindObjectOfType(GUI_CustomControls);
	playerInvo = FindObjectOfType(Widget_Inventory);
	playerAttack = FindObjectOfType(Widget_AttackController);
	player = GameObject.FindWithTag("Player");
}

//Dispay----------------------------------------------------------------------------
function OnGUI()
{ 
	if(customSkin)
		GUI.skin = customSkin;

	//Widget's Vitals
	customControls.LeftStatusMeter(widgetImage, playerInfo.health, playerInfo.energy, lbarImage, lhbar, lebar);

	//Inventory Buttons-------------------------------------------------------
	if(customControls.InvoHudButton(Rect(10, Screen.height - 100, 93, 95), playerInvo.GetItemCount(InventoryItem.ENERGYPACK), energykitImage, "Click to use an Energy Pack."))
	{
		playerInvo.UseItem(InventoryItem.ENERGYPACK, 1);
	}
	if(customControls.InvoHudButton(Rect(110, Screen.height - 100, 93, 95), playerInvo.GetItemCount(InventoryItem.REPAIRKIT), repairkitImage, "Click to use a Repair Kit."))
	{
		playerInvo.UseItem(InventoryItem.REPAIRKIT, 1);
	}
	//Non-Usable Inventory Buttons-----------------------------------------
	customControls.InvoHudButton(Rect(Screen.width - 210, Screen.height - 100, 93, 95), playerInvo.GetItemCount(InventoryItem.SCREW), screwImage, "Number of screws you've collected.");
	customControls.InvoHudButton(Rect(Screen.width - 110 , Screen.height - 100, 93, 95), playerInvo.GetItemCount(InventoryItem.NUT), gearImage, "Number of gears you've collected.");


	//Enemy Vitals
	closestEnemy = playerAttack.GetClosestEnemy();
	if (closestEnemy != null)
	{
		enemyDistance = Vector3.Distance(closestEnemy.transform.position, player.transform.position);
		if(enemyDistance < 20.0)
		{
			closestEnemyStatus = closestEnemy.GetComponent(EBunny_Status);
			enemyImage = closestEnemyStatus.GetCharImage();
			customControls.RightStatusMeter(enemyImage, closestEnemyStatus.health, closestEnemyStatus.energy, rbarImage, rhbar, rebar, circBackImage);
		}
	}
	
	
}

@script ExecuteInEditMode()
@script AddComponentMenu("GUI/HUD")