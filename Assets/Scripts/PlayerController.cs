using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	private Animator anim;
	public List<string> playerToys; 

	public int damage;
	private float timeUntilActionable;
	public float hitstunFactor;
	public float knockbackDamageGrowthFactor;
	private bool inHitstun;
	private GameObject basicAttackObj;
	private BasicAttackController basicAttackCont;
	public float basicAttackDuration;
	private bool basicAttackActive;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		timeUntilActionable = 0;
		inHitstun = false;

		basicAttackObj = transform.GetChild (0).gameObject;
		basicAttackCont = GetComponentInChildren<BasicAttackController> ();
		basicAttackObj.SetActive (false);
		basicAttackActive = false;

		playerToys = new List<string>(); 

	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilActionable > 0) {
			timeUntilActionable -= Time.deltaTime;
		} else {
			if (inHitstun) {
				rb.velocity = Vector3.zero;
				inHitstun = false;
				GetComponent<SpriteRenderer> ().color = Color.white;
			}
			if (basicAttackActive) {
				basicAttackCont.SetAttackHitbox (false);
				basicAttackObj.SetActive (false);
				anim.SetTrigger ("ResetToNeutral");
				basicAttackActive = false;
			}
			Move ();
			DetectActionInput ();
		}
	}

	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxisRaw ("Horizontal_P" + playerNum), Input.GetAxisRaw ("Vertical_P" + playerNum));

		direction = direction.normalized;

		rb.velocity = speed * direction;

		if (direction.x * transform.localScale.x > 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

	void DetectActionInput(){
		if (Input.GetButtonDown ("BasicAttack_P" + playerNum)) {
			BasicAttack ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{ 
		if (other.gameObject.tag == "Toy" && playerToys.Count < 2) 
		{ 
			Debug.Log(gameObject.name + " grabbed the " + other.gameObject.name + "!!!"); 
			playerToys.Add(other.gameObject.GetComponent<ToyControllerScript>().toyName); 
			Destroy(other.gameObject); 
		} 

	} 
	void OnTriggerExit2D(Collider2D collider){
		if (collider.tag == "Arena"){
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}

	void BasicAttack(){
		anim.SetTrigger ("basicAttack");
		basicAttackObj.SetActive (true);
		basicAttackCont.SetAttackHitbox (true);
		basicAttackActive = true;
		timeUntilActionable = basicAttackDuration;
		rb.velocity = Vector3.zero;
	}

	public void TakeHit(int damageTaken, float baseKnockback, float knockbackGrowth, Vector3 knockbackVector){
		damage += damageTaken;
		float knockbackMagnitude = baseKnockback + (knockbackGrowth * damage * knockbackDamageGrowthFactor);
		timeUntilActionable = knockbackMagnitude * hitstunFactor;
		rb.velocity = knockbackMagnitude * knockbackVector;
		inHitstun = true;
		GetComponent<SpriteRenderer> ().color = Color.red;
	}
}
