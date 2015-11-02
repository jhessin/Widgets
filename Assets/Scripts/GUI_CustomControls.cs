using UnityEngine;
using System.Collections;
//GUI_CustomControls:  Contains the custom compound control classes for use elsewhere in the GUI_CustomControls

//Item HUD Button--------------------------------------------------------------------------------

//Displays the button, correct overlay item picture, and the number of the item currently in Widget's Invo.

namespace GrillbrickStudios
{
	[AddComponentMenu("GUI/CustomControls")]
	public class GUI_CustomControls : MonoBehaviour {
		public bool InvoHudButton(Rect screenPos, int numAvailable, Texture itemImage, string itemtooltip)
		{
			if (GUI.Button(screenPos, new GUIContent(itemImage, itemtooltip), "HUD Button"))
				return true;
			GUI.Label(new Rect(screenPos.xMax - 20, screenPos.yMax - 25, 20, 20), numAvailable.ToString());

			// display area for tooltips
			GUI.Label(new Rect(20, Screen.height - 130, 500, 100), GUI.tooltip);

			return false;
		}

		// Left Hand Health------------------------
		public void LeftStatusMeter(Texture charImage, float health, float energy, Texture bBarImage, Texture hBarImage, Texture eBarImage)
		{
			GUI.BeginGroup(new Rect(0, 0, 330, 125));

			// Place Back Bars
			GUI.Label(new Rect(40, 10, 272, 90), bBarImage);

			// Place Front Bars
			GUI.BeginGroup(new Rect(40, 10, 218 * (health / 10.0f) + 35, 90));
			GUI.Label(new Rect(0, 0, 272, 90), hBarImage);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(40, 10, 218 * (energy / 10.0f) + 10, 90));
			GUI.Label(new Rect(0, 0, 272, 90), eBarImage);
			GUI.EndGroup();

			// Place Head Circle
			GUI.Label(new Rect(0, 0, 330, 125), charImage);

			GUI.EndGroup();
		}

		// Right Hand Health------------------------
		public void RightStatusMeter(Texture charImage, float health, float energy, Texture bBarImage, Texture hBarImage, Texture eBarImage, Texture bCircleImage)
		{
			GUI.BeginGroup(new Rect(Screen.width - 330, 0, 330, 125));

			// Place Back Bars
			GUI.Label(new Rect(40, 10, 272, 90), bBarImage);

			// Place Front Bars
			GUI.BeginGroup(new Rect(40 + (218 - 218 * (health / 10.0f)), 10, 218 * (health / 10.0f), 90));
			GUI.Label(new Rect(0, 0, 272, 90), hBarImage);
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(40 + (218 - 218 * (energy / 10.0f)), 10, 218 * (energy / 10.0f), 90));
			GUI.Label(new Rect(0, 0, 272, 90), eBarImage);
			GUI.EndGroup();

			// Place Back Circle
			GUI.Label(new Rect(208, 0, 330, 125), bCircleImage);

			// Place Head Circle
			GUI.Label(new Rect(208, 0, 330, 125), charImage);

			GUI.EndGroup();
		}
    }
}