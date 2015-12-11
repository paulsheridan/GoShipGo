using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour 
{

	public static int score;

	Text text;
	
	void Awake () 
	{
		text = GetComponent <Text> ();
		text.text = "High Score: " + PlayerPrefs.GetInt ("highScore");
	}
}
