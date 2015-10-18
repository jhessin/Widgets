//PickupItems: handles any items lying around the world that Widget can pick up

var itemType : InventoryItem;
var itemAmount = 1;
var sparkleEmitter : ParticleEmitter;

private var pickedUp = false;


//When Widget finds an item on the field------------------------------------------------------------------------------
function OnTriggerEnter(collider: Collider){

	//make sure that this is a player hitting the item and not an enemy
	var playerStatus : Widget_Status = collider.GetComponent(Widget_Status);
	if(playerStatus == null) return;
	
	//stop it from being picked up twice by accident
	if(pickedUp) return;
	
	//If everything's good, put it in Widget's Inventory
	var widgetInventory  = collider.GetComponent(Widget_Inventory);
	widgetInventory.GetItem(itemType, itemAmount);
	pickedUp = true;
	
		
	//play any sound for picking it up
	
	//stop any FX when picking it up
	sparkleEmitter.emit = false;
	
	//Get rid of it now that it's in the inventory
	Destroy(gameObject);
	
}


// Make sure the pickup is setup properly with a collider-----------------------------------------------------------------
function Reset ()
{
	if (GetComponent.<Collider>() == null)	
	{
		gameObject.AddComponent(SphereCollider);
	}
	GetComponent.<Collider>().isTrigger = true;
	
	sparkleEmitter.emit = true;
}

@script AddComponentMenu("Inventory/PickupItems")