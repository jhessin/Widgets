//GUI_MainMenu:  adds the backdrop and main navigation buttons to the scene.
//This scene will be the first thing the app loads, and will allow the player to pick a level to load or quit.

var customSkin: GUISkin;
var mainMenuBG : Texture2D;
var mainTitle : Texture2D;

private var isLoading : boolean = false;

//Main Menu-------------------------------------------------------
function OnGUI()
{
	if(customSkin)
		GUI.skin = customSkin;
	
	//BG Images
	GUI.Box(Rect( 0, 0, Screen.width, Screen.height), " ");
	GUI.Label(Rect( 0, 30, Screen.width, Screen.height), mainMenuBG);
	
	//Title and Buttons
	GUI.Label(Rect(Screen.width - 500, 50, mainTitle.width, mainTitle.height), mainTitle);
	
	
	//If game is currently loading, display a notification to the user
	if (isLoading)
	{
		GUI.Label( Rect(Screen.width/2 - 50, Screen.height - 40, 100, 50), "Now Loading");
	}else 
	{
		if(GUI.Button( Rect(Screen.width - 380, Screen.height - 280, 320, 80), "Start Game", "Long Button") )
		{
			isLoading = true;
			Application.LoadLevel(1);  
		}
	
		if(GUI.Button( Rect(Screen.width - 380, Screen.height - 180, 320, 80), "Quit Game", "Long Button") )
		{
			Application.Quit();
		}
	}
	
}

@script ExecuteInEditMode()
@script AddComponentMenu("GUI/MainMenu")