using UnityEngine;
using System.Collections;
using PathologicalGames;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
	public AudioClip crashSound;
	public AudioClip laserHitSound;
	public AudioClip shieldRecharge;
	public float playerScore = 0;
	public int score;
	public int finalScore;
	public Text scoreText;
	public GameObject overlay;
	public GameObject scoreSheet;
	public GameObject gun;

	private float health = 100f;
	private float healthChunk = 12.5f;
	private float repeatDamagePeriod = 0.4f;
	private float lastHitTime;
	private Animator anim;
	private MainCamera shakeCam;
	private SpriteRenderer shieldBar;
	private Vector3 healthScale;
	private bool alive;


	void Awake ()
	{
		shakeCam = Camera.main.GetComponent<MainCamera>();
		shieldBar = GameObject.Find("ShieldBar").GetComponent<SpriteRenderer>();
		healthScale = shieldBar.transform.localScale;
		alive = true;
	}

	void Update ()
	{
		playerScore += Time.deltaTime / 200;
		score = (int)(playerScore * 100);
		if (alive == true)
		{
			scoreText.text = "Score: " + (int) (score);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Asteroid")
		{
			DamageShip (healthChunk);
		}

		if (col.gameObject.tag == "HealthPickup")
		{
			HealShip (healthChunk);
		}
	}
	public void PlusTen ()
	{
		score += 10;
	}

	void DamageObject()
	{
		DamageShip (healthChunk);
	}

	void DamageShip (float damageAmount)
	{
		if (Time.time > lastHitTime + repeatDamagePeriod) 
		{
			AudioSource.PlayClipAtPoint(crashSound,transform.position);
			health -= damageAmount; 
			lastHitTime = Time.time; 
			shakeCam.Shake();
			Debug.Log ("Health = " + health);
			UpdateHealthBar ();
			HealthCheck ();
		}
	}

	void HealShip (float healAmount)
	{
		if (health <= 100) 
		{
			health += healAmount; 
			//Debug.Log ("Health = " + health);
			UpdateHealthBar ();
			HealthCheck ();
		}
	}

	void HealthCheck ()
	{
		if (health < 1)
		{
			StartCoroutine("ReloadGame");
			shakeCam.Shake();
			GetComponent<Renderer>().enabled = false;
			Collider2D[] cols = GetComponents<Collider2D>();
			foreach(Collider2D bc in cols)
			{
				bc.enabled = false;
			}
			alive = false;
			foreach(Transform child in transform)
			{
				child.gameObject.SetActive(false);
			}
		}
	}
	
	public void UpdateHealthBar ()
	{
		shieldBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1f, 1f);
	}
	
	IEnumerator ReloadGame ()
	{
		finalScore = score;
		if (finalScore > PlayerPrefs.GetInt ("highScore"))
		{
			PlayerPrefs.SetInt ("highScore", finalScore);
		}
		yield return new WaitForSeconds(1); 
		ScoreUpdate scoreUpdate = scoreSheet.GetComponent<ScoreUpdate>();
		scoreUpdate.score = finalScore;
		scoreUpdate.GameScore();
		overlay.SetActive(true);
	}
}
