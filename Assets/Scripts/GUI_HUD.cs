using UnityEngine;
using System.Collections;
//GUI_HUD: displays the pertinant information for Widget, his items, and any current enemy


namespace GrillbrickStudios
{
	[ExecuteInEditMode]
	[AddComponentMenu("GUI/HUD")]
	public class GUI_HUD : MonoBehaviour
	{
		//Set up Textures-------------------------------------------------------------------
		//For larger games, this should be done programmatically
		public GUISkin customSkin;

		public Texture2D screwImage;
		public Texture2D gearImage;
		public Texture2D repairkitImage;
		public Texture2D energykitImage;

		// Left Vital Tex
		public Texture2D lbarImage;
		public Texture2D lhbar;
		public Texture2D lebar;
		public Texture2D widgetImage;

		// Right Vital Tex
		public Texture2D rbarImage;
		public Texture2D rhbar;
		public Texture2D rebar;
		public Texture2D enemyImage;
		public Texture2D circBackImage;

		//-----------------------------------------

		GUI_CustomControls customControls;
		Widget_Status playerInfo;
		Widget_Inventory playerInvo;
		Widget_AttackController playerAttack;
		EBunny_Status closestEnemyStatus;
		GameObject player;

		GameObject closestEnemy;
		float enemyDistance;

		// Initialize Player Info------------------
		public void Start()
		{
			playerInfo = FindObjectOfType<Widget_Status>();
			customControls = FindObjectOfType<GUI_CustomControls>();
			playerInvo = FindObjectOfType<Widget_Inventory>();
			playerAttack = FindObjectOfType<Widget_AttackController>();
			player = GameObject.FindWithTag("Player");
		}

		// Display----------------------------------
		public void OnGUI()
		{
			if (customSkin)
				GUI.skin = customSkin;

			// Widget's Vitals
			customControls.LeftStatusMeter(widgetImage, playerInfo.health, playerInfo.energy, lbarImage, lhbar, lebar);

			// Inventory Buttons-------------------
			if (customControls.InvoHudButton(new Rect(10, Screen.height - 100, 93, 95), playerInvo.GetItemCount(InventoryItem.ENERGYPACK), energykitImage, "Click to use an Energy Pack."))
			{
				playerInvo.UseItem(InventoryItem.ENERGYPACK, 1);
			}
			if (customControls.InvoHudButton(new Rect(110,Screen.height - 100,93,95),
				playerInvo.GetItemCount(InventoryItem.REPAIRKIT), repairkitImage, "Click to use a Repair Kit."))
			{
				playerInvo.UseItem(InventoryItem.REPAIRKIT, 1);
			}
			// Non-Usable Inventory Buttons
			customControls.InvoHudButton(new Rect(Screen.width - 210, Screen.height - 100, 93, 95),
				playerInvo.GetItemCount(InventoryItem.SCREW), screwImage, "Number of screws you've collected");
			customControls.InvoHudButton(new Rect(Screen.width - 110, Screen.height - 100, 93, 95),
				playerInvo.GetItemCount(InventoryItem.NUT), gearImage, "Number of gears you've collected.");

			// Enemy Vitals
			closestEnemy = playerAttack.GetClosestEnemy();
			if (closestEnemy != null)
			{
				enemyDistance = Vector3.Distance(closestEnemy.transform.position, player.transform.position);
				if (enemyDistance < 20.0)
				{
					closestEnemyStatus = closestEnemy.GetComponent<EBunny_Status>();
					enemyImage = closestEnemyStatus.GetCharImage();
					customControls.RightStatusMeter(enemyImage, closestEnemyStatus.health, closestEnemyStatus.energy, rbarImage, rhbar, rebar, circBackImage);
				}
			}
		}


	}
}