using UnityEngine;
using System.Collections;
//GUI_MainMenu:  adds the backdrop and main navigation buttons to the scene.
//This scene will be the first thing the app loads, and will allow the player to pick a level to load or quit.

namespace GrillbrickStudios
{
	[ExecuteInEditMode]
	[AddComponentMenu("GUI/MainMenu")]
	public class GUI_MainMenu : MonoBehaviour
	{
		public GUISkin customSkin;
		public Texture2D mainMenuBG;
		public Texture2D mainTitle;

		private bool isLoading;

		public void OnGUI()
		{
			if (customSkin)
				GUI.skin = customSkin;

			//BG Images
			GUI.Box(new Rect(0,0,Screen.width, Screen.height), " ");
			GUI.Label(new Rect(0,30,Screen.width, Screen.height), mainMenuBG);

			//Title and Buttons
			GUI.Label(new Rect(Screen.width - 500, 50, mainTitle.width, mainTitle.height), mainTitle);

			if (GUI.Button(new Rect(Screen.width - 380, Screen.height - 280, 320, 80), "Start Game", "Long Button"))
			{
				isLoading = true;
				Application.LoadLevel(1);
			}

			if (GUI.Button(new Rect(Screen.width - 380, Screen.height - 180, 320, 80), "Quit Game", "Long Button"))
			{
				Application.Quit();
			}

			// If game is currently loading, display a notification to the user
			if (isLoading)
			{
				GUI.Label(new Rect(Screen.width/2 - 50, Screen.height - 40, 100, 50), "Now Loading");
			}
		}
	}
}