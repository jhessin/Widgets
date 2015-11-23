using JetBrains.Annotations;
using UnityEngine;

//GUI_WaypointStore : handles the interface for the store transactions
//shows the available items for purchase, allows the player to sell his own inventory, and buy transforms

namespace GrillbrickStudios
{
	public class GUI_WaypointStore : MonoBehaviour
	{
		private GUI_StoreGrid _grid = new GUI_StoreGrid();
		private WeaponManager _weaponManager;
		public GUISkin customSkin;
		public Texture2D laserTile;
		public Texture2D RepairTile;
		public Texture2D EnergyTile;
		private bool openStore;

		private Widget_Inventory playerInventory;
		internal StoreFront store;
		public Texture2D storeBG;

		//------------------------------------------------------------
		public void Awake()
		{
			playerInventory = FindObjectOfType<Widget_Inventory>();
			store = FindObjectOfType<StoreFront>();
			_weaponManager = FindObjectOfType<WeaponManager>();

			if (!playerInventory)
				Debug.Log("No link to player's inventory.");
			if (!_weaponManager)
			{
				Debug.Log("No link to players weapons.");
			}
		}
		
		public void OnGUI()
		{
			if (customSkin)
				GUI.skin = customSkin;

			if (!openStore) return;

			_grid.SetGrid(new Rect(Screen.width/2 - storeBG.width/4, Screen.height/2 - storeBG.height/4, storeBG.width,
				storeBG.height));

			// clear the item grid
			_grid.clearGrid();

			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), " ");
			GUI.Label(
				new Rect(Screen.width/2 - storeBG.width/2, Screen.height/2 - storeBG.height/2, storeBG.width, storeBG.height),
				storeBG);

			if (store.CheckTransformationBuy(StoreFront.ItemLaser) && !_weaponManager.HasDoubleLaser)
			_grid.AddItem(new TileLaser(laserTile, this));

			if (store.CheckTransformationBuy(StoreFront.ItemRepair))
			_grid.AddItem(new TileRepair(RepairTile, this));

			if (store.CheckTransformationBuy(StoreFront.ItemEnergy))
			_grid.AddItem(new TileEnergy(EnergyTile, this));


			// show the item grid
			_grid.showGrid();

			if (GUI.Button(new Rect(Screen.width/2 - 126, Screen.height - 100, 252, 113), "Close Store"))
			{
				StoreFrontToggle();
			}
		}

		private void BuyLaser()
		{
			store.BuyTransformation(StoreFront.ItemLaser);
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

		private class TileLaser :
			GUI_StoreButton
		{
			private readonly GUI_WaypointStore parent;

			public TileLaser(Texture2D texture2D, GUI_WaypointStore guiWaypointStore) : base(texture2D)
			{
				parent = guiWaypointStore;
			}

			public override void onClick()
			{
				parent.BuyLaser();
				parent.StoreFrontToggle();
			}
		}
	}

	public class TileEnergy : GUI_StoreButton
	{
		private readonly GUI_WaypointStore parent;
		public TileEnergy(Texture2D energyTile, GUI_WaypointStore guiWaypointStore) : base(energyTile)
		{
			parent = guiWaypointStore;
		}

		public override void onClick()
		{
			parent.store.BuyTransformation(StoreFront.ItemEnergy);
		}
	}

	public class TileRepair : GUI_StoreButton
	{
		private GUI_WaypointStore parent;
		public TileRepair(Texture2D medpakTile, GUI_WaypointStore guiWaypointStore) : base(medpakTile)
		{
			parent = guiWaypointStore;
		}

		public override void onClick()
		{
			parent.store.BuyTransformation(StoreFront.ItemRepair);
		}
	}
}