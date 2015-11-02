using UnityEngine;
using System.Collections;

//StoreFront: handles purchasing of transformations at waypoints
namespace GrillbrickStudios
{
	public class StoreFront : MonoBehaviour {
		//This will be called from the store front if CheckTransformationBuy returns true on selecttion
		public void BuyTransformation()
		{
			
		}

		/* Ingrediants required for Widget's Transforms----------------------------------------------------------------------------------------------------------------------------------------
Hook Transform: boss_plowblade, 2 screws, 2 nuts
Shield Transform: boss_tray, 1 screw, 1 nut
Flight Transform:  boss_windblade, 4 screws, 4 nuts
*/

		//called from the Store Purchase Button.  Player must select which upgrade they want, which is passed to this 
		public bool CheckTransformationBuy(int selection)
		{
			Widget_Inventory inventory;
			GameObject widgetPlayer = GameObject.FindWithTag("Player");

			inventory = widgetPlayer.GetComponent<Widget_Inventory>();

			// number of items required to buy for Transformations
			int bossitem = 0;
			int screws = 0;
			int nuts = 0;

			switch (selection)
			{
				case 0:	//shield
					bossitem = 1;
					screws = 1;
					nuts = 1;
					break;
				case 1: //hook
					bossitem = 1;
					screws = 2;
					nuts = 2;
					break;
				case 2: //flight
					bossitem = 1;
					screws = 4;
					nuts = 4;
					break;
				default:
					return false;
			}

			if (inventory.CompareItemCount(InventoryItem.BOSS_TRAY, bossitem) &&
			    inventory.CompareItemCount(InventoryItem.SCREW, screws) &&
			    inventory.CompareItemCount(InventoryItem.NUT, nuts))
				return true;
			return false;
		}
	}
}