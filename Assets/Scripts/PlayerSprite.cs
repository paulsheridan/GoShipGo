using UnityEngine;
using System.Collections;

public class PlayerSprite : MonoBehaviour 
{

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;

	private SpriteRenderer spriteRenderer;
	
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite == null)
			spriteRenderer.sprite = sprite1;
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			ChangeTheSprite (); 
		}
	}
	
	void ChangeTheSprite ()
	{
		if (spriteRenderer.sprite == sprite1) 
		{
			spriteRenderer.sprite = sprite2;
		}
		else
		{
			spriteRenderer.sprite = sprite1; 
		}
	}
}