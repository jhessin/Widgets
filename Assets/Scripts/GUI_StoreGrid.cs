using UnityEngine;
using System.Collections;

namespace GrillbrickStudios
{

	public class GUI_StoreGrid : MonoBehaviour
	{
		private const int GridSize = 75;
		private ArrayList _items = new ArrayList();
		private Rect _location;

		// Use this for initialization
		public void Awake()
		{
			if (_location == null)
			{
				_location = new Rect(50, 50, 100, 100);
			}
		}

		public void SetGrid(Rect grid)
		{
			_location = grid;
		}

		public void AddItem(GUI_StoreButton item)
		{
			_items.Add(item);
		}

		public void clearGrid()
		{
			_items.Clear();
		}

		public void showGrid()
		{
			GUI.BeginGroup(_location);
			int x = 0;
			int y = 0;
			foreach (GUI_StoreButton button in _items)
			{
				if (GUI.Button(new Rect(x, 0, GridSize, GridSize), button.icon))
				{
					button.onClick();
				}
				if (x < _location.width - GridSize)
				{
					x += GridSize;
				}
				else
				{
					x = 0;
					y += GridSize;
				}
			}
			GUI.EndGroup();
		}
	}
}