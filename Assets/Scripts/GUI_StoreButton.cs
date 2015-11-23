using JetBrains.Annotations;
using UnityEngine;

namespace GrillbrickStudios
{
	public abstract class GUI_StoreButton
	{
		public Texture2D icon;

		public GUI_StoreButton(Texture2D icon)
		{
			this.icon = icon;
		}

		public abstract void onClick();
	}
}