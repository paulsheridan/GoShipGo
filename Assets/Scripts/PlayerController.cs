using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private float maxSpeed = 40f;
	public static float moveX;

	void FixedUpdate() 
	{
		moveX = Input.GetAxis ("Horizontal");
		//moveX = Input.acceleration.x;

		//GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * maxSpeed, 0);
	}
}
