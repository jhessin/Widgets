using UnityEngine;
using System.Collections;
using System;

//GUI_WaypointStore : handles the interface for the store transactions
//shows the available items for purchase, allows the player to sell his own inventory, and buy transforms
namespace GrillbrickStudios
{
	public class GUI_WaypointStore : MonoBehaviour
	{
		public Texture2D storeBG;
		public GUISkin customSkin;

		private Widget_Inventory playerInventory;
		private bool openStore = false;

		//------------------------------------------------------------
		public void Awake()
		{
			playerInventory = FindObjectOfType<Widget_Inventory>();
			if (!playerInventory)
				Debug.Log("No link to player's inventory.");
		}

		public void OnGUI()
		{
			if (customSkin)
				GUI.skin = customSkin;

			if (!openStore) return;

			GUI.Box(new Rect(0,0,Screen.width, Screen.height), " ");
			GUI.Label(new Rect(Screen.width/2 - storeBG.width/2, Screen.height/2 - storeBG.height/2, storeBG.width, storeBG.height), storeBG);

			if (GUI.Button(new Rect(Screen.width/2 - 126, Screen.height - 100, 252, 113), "Close Store"))
			{
				StoreFrontToggle();
			}
		}

		public void StoreFrontToggle()
		{
			if (openStore)
				openStore = false;
			else
				openStore = true;
		}

		public bool GetStoreStatus()
		{
			return openStore;
		}
	}
}