using UnityEngine;

//StoreFront: handles purchasing of transformations at waypoints

namespace GrillbrickStudios
{
	public class StoreFront : MonoBehaviour
	{
		//This will be called from the store front if CheckTransformationBuy returns true on selecttion

		public const int ItemShield = 0;
		public const int ItemHook = 1;
		public const int ItemFlight = 2;
		public const int ItemLaser = 3;
		private ItemCost _costFlight = new ItemCost(1, 4, 4);
		private ItemCost _costHook = new ItemCost(1, 2, 2);
		private ItemCost _costLaser = new ItemCost(0, 0, 0);

		private ItemCost _costShield = new ItemCost(1, 1, 1);
		private GameObject _laser;
		private GameObject _taser;
		private Widget_Inventory _inventory;

		public void Start()
		{
			GameObject widgetPlayer = GameObject.FindWithTag("Player");
			_inventory = widgetPlayer.GetComponent<Widget_Inventory>();
			_laser = GameObject.FindGameObjectWithTag("Laser");
			_taser = GameObject.FindGameObjectWithTag("Taser");

			DisableAll();
			EnableTaser();
		}

		private void EnableTaser()
		{
			foreach (EllipsoidParticleEmitter emitter in _taser.GetComponentsInChildren<EllipsoidParticleEmitter>())
			{
				emitter.enabled = true;
			}
		}

		public void BuyTransformation(int selection)
		{
			if (CheckTransformationBuy(selection))
			{
				switch (selection)
				{
					case ItemShield: //shield
						_costShield.BuyFrom(_inventory);
						// TODO: activate shield
						break;
					case ItemHook: //hook
						_costHook.BuyFrom(_inventory);
						// TODO: activate hook
						break;
					case ItemFlight: //flight
						_costFlight.BuyFrom(_inventory);
						// TODO: activate flight
						break;
					case ItemLaser:
						_costLaser.BuyFrom(_inventory);
						DisableAll();
						EnableLaser();
						break;
				}
			}
		}

		private void EnableLaser()
		{
			foreach (var mesh in _laser.GetComponentsInChildren<MeshRenderer>())
			{
				mesh.enabled = true;
			}
			foreach (var laserScript in _laser.GetComponentsInChildren<LaserScript>())
			{
				laserScript.enabled = true;
			}
		}

		private void DisableAll()
		{
			foreach (EllipsoidParticleEmitter emitter in _taser.GetComponentsInChildren<EllipsoidParticleEmitter>())
			{
				emitter.enabled = false;
			}
			foreach (var mesh in _laser.GetComponentsInChildren<MeshRenderer>())
			{
				mesh.enabled = false;
			}
			foreach (var laserScript in _laser.GetComponentsInChildren<LaserScript>())
			{
				laserScript.enabled = false;
			}
		}

		/* Ingrediants required for Widget's Transforms----------------------------------------------------------------------------------------------------------------------------------------
Hook Transform: boss_plowblade, 2 screws, 2 nuts
Shield Transform: boss_tray, 1 screw, 1 nut
Flight Transform:  boss_windblade, 4 screws, 4 nuts
*/

		//called from the Store Purchase Button.  Player must select which upgrade they want, which is passed to this 
		public bool CheckTransformationBuy(int selection)
		{


			// number of items required to buy for Transformations
			var bossitem = 0;
			var screws = 0;
			var nuts = 0;

			switch (selection)
			{
				case ItemShield: //shield
					_costShield.getCost(out bossitem, out screws, out nuts);
					break;
				case ItemHook: //hook
					_costHook.getCost(out bossitem, out screws, out nuts);
					break;
				case ItemFlight: //flight
					_costFlight.getCost(out bossitem, out screws, out nuts);
					break;
				case ItemLaser:
					_costLaser.getCost(out bossitem, out screws, out nuts);
					break;
				default:
					return false;
			}

			if (_inventory.CompareItemCount(InventoryItem.BOSS_TRAY, bossitem) &&
			    _inventory.CompareItemCount(InventoryItem.SCREW, screws) &&
			    _inventory.CompareItemCount(InventoryItem.NUT, nuts))
				return true;
			return false;
		}

		private struct ItemCost
		{
			public readonly int bossitem;
			public readonly int screws;
			public readonly int nuts;

			public ItemCost(int bi, int s, int n)
			{
				bossitem = bi;
				screws = s;
				nuts = n;
			}

			public void getCost(out int bi, out int s, out int n)
			{
				bi = bossitem;
				s = screws;
				n = nuts;
			}

			public void BuyFrom(Widget_Inventory inventory)
			{
				inventory.UseItem(InventoryItem.BOSS_TRAY, bossitem);
				inventory.UseItem(InventoryItem.SCREW, screws);
				inventory.UseItem(InventoryItem.NUT, nuts);
			}
		}
	}
}