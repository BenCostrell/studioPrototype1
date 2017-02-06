using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	private Animator anim;
	public List<string> playerToys; 
	public GameObject fireballPrefab;

	public int damage;
	private float timeUntilActionable;
	public float hitstunFactor;
	public float knockbackDamageGrowthFactor;
	private bool inHitstun;
	private GameObject basicAttackObj;
	private BasicAttackController basicAttackCont;
	public float basicAttackDuration;
	private bool basicAttackActive;
	private bool actionInProcess;
	private float ability1CooldownCounter;
	private float ability2CooldownCounter;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		timeUntilActionable = 0;
		ability1CooldownCounter = 0;
		ability2CooldownCounter = 0;
		inHitstun = false;
		actionInProcess = false;

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
				basicAttackActive = false;
			}
			if (actionInProcess) {
				actionInProcess = false;
				anim.SetTrigger ("ResetToNeutral");
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

		if (ability1CooldownCounter > 0) {
			ability1CooldownCounter -= Time.deltaTime;
		}
		else if (Input.GetButtonDown("Ability1_P" + playerNum)){
			ability1CooldownCounter = ThrowFireball ();
		}

		if (ability2CooldownCounter > 0) {
			ability2CooldownCounter -= Time.deltaTime;
		}
		else if (Input.GetButtonDown("Ability2_P" + playerNum)){
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

	float ThrowFireball(){
		int directionFacing = 1;
		if (transform.localScale.x > 0) {
			directionFacing *= -1;
		}
		Vector3 fireballOffset = directionFacing * 2 * Vector3.right;
		GameObject fireball = Instantiate (fireballPrefab, transform.position + fireballOffset, Quaternion.identity);
		FireballController fc = fireball.GetComponent<FireballController> ();
		fc.InitializeTrajectory (directionFacing);

		anim.SetTrigger ("ThrowFireball");
		InitiateAction (fireball.GetComponent<FireballController> ().animationDuration);
		return fc.cooldown;
	}

	void Die(){
		Destroy (gameObject);
	}

	void BasicAttack(){
		InitiateAction (basicAttackDuration);
		anim.SetTrigger ("basicAttack");
		basicAttackObj.SetActive (true);
		basicAttackCont.SetAttackHitbox (true);
		basicAttackActive = true;

	}

	public void TakeHit(int damageTaken, float baseKnockback, float knockbackGrowth, Vector3 knockbackVector){
		damage += damageTaken;
		float knockbackMagnitude = baseKnockback + (knockbackGrowth * damage * knockbackDamageGrowthFactor);
		timeUntilActionable = knockbackMagnitude * hitstunFactor;
		rb.velocity = knockbackMagnitude * knockbackVector;
		inHitstun = true;
		GetComponent<SpriteRenderer> ().color = Color.red;
	}

	void InitiateAction(float actionDuration){
		timeUntilActionable = actionDuration;
		rb.velocity = Vector3.zero;
		actionInProcess = true;
	}
}
