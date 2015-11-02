using UnityEngine;

//PickupItems: handles any items lying around the world that Widget can pick up
namespace GrillbrickStudios
{
	[AddComponentMenu("Inventory/PickupItems")]
	public class PickupItems : MonoBehaviour
	{
		public InventoryItem itemType;
		public int itemAmount = 1;
		public ParticleEmitter sparkleEmitter;

		private bool pickedUp = false;

		// When Widget finds an item on the field---------------------------
		public void OnTriggerEnter(Collider other)
		{
			// make sure that this is a player hitting the item and not an enemy
			Widget_Status playerStatus = other.GetComponent<Widget_Status>();
			if (playerStatus == null) return;

			// stop it from being picked up twice by accident
			if (pickedUp) return;

			// if everything's good, put it in Widget's Inventory
			Widget_Inventory inventory = other.GetComponent<Widget_Inventory>();
			inventory.GetItem(itemType, itemAmount);
			pickedUp = true;

			// TODO: play any sound for picking it up

			// stop any FX when picking it up
			sparkleEmitter.emit = false;

			// Get rid of it now that it's in the inventory
			Destroy(gameObject);
		}

		public void Reset()
		{
			Collider col = GetComponent<Collider>() ?? gameObject.AddComponent<SphereCollider>();
			col.isTrigger = true;
			sparkleEmitter.emit = true;
		}
	}
}