using UnityEngine;
using System.Collections;

public class OverlayScript : MonoBehaviour 
{

	public void MainMenu () 
	{
		Application.LoadLevel ("Menu");
	}
	public void PlayAgain () 
	{
		//Application.LoadLevel ("NovaRun");
		Application.LoadLevel(Application.loadedLevel);
	}
}
