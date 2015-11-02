using UnityEngine;
using System.Collections;
using System;
//Widget_Inventory:  All of Widget's collected items are updated here
//Also handles functions for item use and inventory managment

namespace GrillbrickStudios
{
	//All the available items in the game available for the character to find-------------------------------------
	public enum InventoryItem
	{
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

	[AddComponentMenu("Inventory/Widget's Inventory")]
	public class Widget_Inventory : MonoBehaviour
	{
		//We'll use a statically typed BuiltIn array rather than a Javascript array here.  
		public int[] widgetInventory;

		public Widget_Status playerStatus;

		// Item Properties--------------------------
		float repairKitHealAmt = 5.0f;
		float energyPackHealAmt = 5.0f;

		// Initialize Widget's starting Inventory--------------------------------
		public void Awake()
		{
			playerStatus = GetComponent<Widget_Status>();
			widgetInventory = new int[(int) InventoryItem.COUNT_NUM_ITEMS];

			foreach (InventoryItem item in widgetInventory)
			{
				widgetInventory[(int)item] = 0;
			}

			// Give Widget some starting items
			widgetInventory[(int)InventoryItem.ENERGYPACK] = 1;
			widgetInventory[(int)InventoryItem.REPAIRKIT] = 2;
		}

		public void GetItem(InventoryItem item, int amount)
		{
			widgetInventory[(int)item] += amount;
		}

		public void UseItem(InventoryItem item, int amount)
		{
			if (widgetInventory[(int)item] <= 0) return;

			widgetInventory[(int)item] -= amount;

			switch (item)
			{
				case InventoryItem.ENERGYPACK:
					playerStatus.AddEnergy(energyPackHealAmt);
					break;
				case InventoryItem.REPAIRKIT:
					playerStatus.AddHealth(repairKitHealAmt);
					break;
			}
		}

		public bool CompareItemCount(InventoryItem compItem, int compNumber)
		{
			if ((int) compItem > widgetInventory.Length) return false;
			return widgetInventory[(int)compItem] >= compNumber;
		}

		public int GetItemCount(InventoryItem compItem)
		{
			if ((int) compItem > widgetInventory.Length) return -1;
			return widgetInventory[(int)compItem];
		}

	}
}