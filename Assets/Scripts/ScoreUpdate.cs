using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUpdate : MonoBehaviour 
{
	public int score;
	Text text;
	
	public void GameScore () 
	{
		text = GetComponent <Text> ();
		text.text = "Your Score: " + score + "\r\nHigh Score: " + PlayerPrefs.GetInt ("highScore");
	}
}
