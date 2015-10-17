//GUI_CustomControls:  Contains the custom compound control classes for use elsewhere in the GUI_CustomControls

//Item HUD Button--------------------------------------------------------------------------------

//Displays the button, correct overlay item picture, and the number of the item currently in Widget's Invo.
function InvoHudButton(screenPos: Rect, numAvailable : int, itemImage: Texture, itemtooltip: String ) : boolean
{	
		if( GUI.Button(screenPos, GUIContent(itemImage, itemtooltip), "HUD Button") ) 
			return true;
		GUI.Label( Rect(screenPos.xMax - 20, screenPos.yMax - 25, 20, 20 ), numAvailable.ToString() );
		
		//display area for tooltips
		GUI.Label( Rect( 20, Screen.height - 130, 500, 100), GUI.tooltip);
}

//Left Hand Health-----------------------------------------------------------------------------------------
function LeftStatusMeter(charImage : Texture, health : float, energy : float, bBarImage : Texture, hBarImage : Texture, eBarImage : Texture) 
{
	GUI.BeginGroup( Rect(0,0, 330, 125) );
	
	//Place Back Bars
	GUI.Label( Rect(40, 10, 272, 90), bBarImage );
	
	//Place Front Bars
	GUI.BeginGroup( Rect(40, 10, 218 * (health/10.0) +35 , 90) );
	GUI.Label( Rect(0, 0, 272, 90), hBarImage );
	GUI.EndGroup();
	
	GUI.BeginGroup( Rect( 40, 10, 218 * (energy/10.0) +10, 90) );
	GUI.Label( Rect(0, 0, 272, 90), eBarImage );
	GUI.EndGroup();

	//Place Head Circle
	GUI.Label( Rect(0, 0, 330, 125), charImage );
	
	GUI.EndGroup();

}

//Right Hand Health-----------------------------------------------------------------------------------------
function RightStatusMeter(charImage : Texture, health : float, energy : float, bBarImage : Texture, hBarImage : Texture, eBarImage : Texture, bCircleImage :Texture) 
{
	GUI.BeginGroup( Rect(Screen.width - 330,0, 330, 125) );
	
	//Place Back Bars
	GUI.Label( Rect(40, 10, 272, 90), bBarImage );
	
	//Place Front Bars
	GUI.BeginGroup( Rect(40 + (218-218*(health/10.0)), 10, 218*(health/10.0), 90) );
	GUI.Label( Rect(0, 0, 272, 90), hBarImage );
	GUI.EndGroup();
	
	GUI.BeginGroup( Rect( 40 + (218-218*(energy/10.0)), 10, 218*(energy/10.0), 90) );
	GUI.Label( Rect(0, 0, 272, 90), eBarImage );
	GUI.EndGroup();
	
	//Place Back Circle
	GUI.Label( Rect(208, 0, 330, 125), bCircleImage );
	
	//Place Head Circle
	GUI.Label( Rect(208, 0, 330, 125), charImage );
	
	GUI.EndGroup();

}

@script AddComponentMenu("GUI/CustomControls")