using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour 
{
	public GameObject player;
	public GunScript gunScript;
	public float smoothInput;
	public float sensitivity = 5;
	private float speed = 30;
	private Rigidbody2D playerRb;

	void Start ()
	{
		smoothInput = 0;
		playerRb = player.GetComponent<Rigidbody2D> ();
	}

	void Update () 
	{
		if (Input.touchCount == 0)
			smoothInput = Mathf.Lerp (smoothInput, 0, Time.deltaTime * sensitivity);
		else if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch (0);

			if (touch.position.x > Screen.width/2)
				smoothInput = Mathf.Lerp (smoothInput, 1, Time.deltaTime * sensitivity);
			else if (touch.position.x < Screen.width/2)
				smoothInput = Mathf.Lerp (smoothInput, -1, Time.deltaTime * sensitivity);
		}
		else if (Input.touchCount == 2)
		{
			gunScript.FireLaser();
			smoothInput = Mathf.Lerp (smoothInput, 0, Time.deltaTime * sensitivity);
		}
		playerRb.velocity = new Vector2 (speed * smoothInput, 0);
	}
}
