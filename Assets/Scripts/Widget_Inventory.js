//Widget_Inventory:  All of Widget's collected items are updated here
//Also handles functions for item use and inventory managment


//All the available items in the game available for the character to find-------------------------------------
enum InventoryItem{
	DEBUG_ITEM,
	SCREW,
	NUT, 
	BOSS_TRAY,
	BOSS_PLOWBLADE,
	BOSS_WINDBLADE,
	ENERGYPACK,
	REPAIRKIT, 
	COUNT_NUM_ITEMS
}

//We'll use a statically typed BuiltIn array rather than a Javascript array here.  
var widgetInventory: int[] ;

var playerStatus: Widget_Status; 
playerStatus = GetComponent(Widget_Status) ;

//Item Properties------------------------------------------------------------------------------------------------
private var repairKitHealAmt = 5.0;
private var energyPackHealAmt = 5.0;


//Initilize Widget's starting Inventory-------------------------------------------------------------------------
function Awake(){
	
	//widgetInventory = new int[InventoryItem.COUNT_NUM_ITEMS];
	widgetInventory = new int[8];
	
	for (var item in widgetInventory){
		widgetInventory[item] = 0;
	}

	//Give Widget some starting items
	widgetInventory[InventoryItem.ENERGYPACK] = 1;
	widgetInventory[InventoryItem.REPAIRKIT] = 2;
		
}

//Inventory Management Functions---------------------------------------------------------------------------------
function GetItem(item: InventoryItem, amount: int){
	widgetInventory[item] += amount;
}


function UseItem(item: InventoryItem, amount: int){
	
	if(widgetInventory[item] <= 0) return;
	
	widgetInventory[item] -= amount;
	
	switch(item){
		case InventoryItem.ENERGYPACK:
			playerStatus.AddEnergy(energyPackHealAmt);
			break;
		case InventoryItem.REPAIRKIT:
			playerStatus.AddHealth(repairKitHealAmt);
			break;
	}
	
}

function CompareItemCount(compItem: InventoryItem, compNumber: int){

	return widgetInventory[compItem] >= compNumber;
}

function GetItemCount(compItem: InventoryItem){
	return widgetInventory[compItem];
}

@script AddComponentMenu("Inventory/Widget's Inventory")