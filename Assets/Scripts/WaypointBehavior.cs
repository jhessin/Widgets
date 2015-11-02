using UnityEngine;
using System.Collections;

//WaypointBehavior:  Handles the scripts on all the waypoints
// Calls the respawn update, and give the player the option of opening the store or not. 

namespace GrillbrickStudios
{
	[AddComponentMenu("GUI/WaypointGUI")]
	public class WaypointBehavior : MonoBehaviour
	{
		public GUISkin customSkin;
		private bool isTriggered;

		public void OnTriggerEnter(Collider other)
		{
			// make sure that this is a player hitting the platform and not an enemy
			Widget_Status playerStatus = other.GetComponent<Widget_Status>();
			if (playerStatus == null) return;

			isTriggered = true;

			playerStatus.energy = playerStatus.maxEnergy;
			playerStatus.health = playerStatus.maxHealth;
		}

		public void OnTriggerExit(Collider other)
		{
			// make sure that this is a player leaving the platform and not an enemy
			Widget_Status playerStatus = other.GetComponent<Widget_Status>();
			if (playerStatus == null) return;

			isTriggered = false;
		}

		public void OnGUI()
		{
			if (customSkin)
				GUI.skin = customSkin;

			if (isTriggered)
			{
				GUI_WaypointStore store = this.GetComponent<GUI_WaypointStore>();

				//Only display open button if store is currently closed
				if (!store.GetStoreStatus())
				{
					if (GUI.Button(new Rect(Screen.width/2-126, Screen.height-100,252,113), "Open Store"))
					{
						store.StoreFrontToggle();
					}
				}
			}
		}
	}
}