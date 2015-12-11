using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour 
{
	public float playerScore = 0;
	public GUIText scoreText;

	private bool alive;


	void Start ()
	{
		alive = true;
	}

	void Update () 
	{
		playerScore += Time.deltaTime / 200;
		if (alive == true)
			UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Distance: " + (int) (playerScore) + "Km";
	}

	public void Dead ()
	{
		alive = false;
	}
}
