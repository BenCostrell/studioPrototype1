using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	public Animator anim;
	public List<string> playerToys; 
	public GameObject basicAttackPrefab;
	public GameObject lungePrefab;
	public GameObject fireballPrefab;
	public GameObject musicBubblePrefab;

	private enum Ability {BasicAttack, Fireball, Lunge, Sing, Shield};

	private Ability ability1;
	private Ability ability2;

	public int damage;
	private float timeUntilActionable;
	public float hitstunFactor;
	public float knockbackDamageGrowthFactor;
	private bool inHitstun;
	private bool actionInProcess;
	private float basicAttackCooldownCounter;
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

		playerToys = new List<string>(); 

		SetAbilities (Ability.Fireball, Ability.Lunge);

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
			if (actionInProcess) {
				actionInProcess = false;
				anim.SetTrigger ("ResetToNeutral");
			}
			Move ();
			DetectActionInput ();
		}
	}

	void SetAbilities(Ability ab1, Ability ab2){
		ability1 = ab1;
		ability2 = ab2;
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
		if (basicAttackCooldownCounter > 0) {
			basicAttackCooldownCounter -= Time.deltaTime;
		}
		else if (Input.GetButtonDown("BasicAttack_P" + playerNum)){
			basicAttackCooldownCounter = DoAbility (Ability.BasicAttack);
		}

		if (ability1CooldownCounter > 0) {
			ability1CooldownCounter -= Time.deltaTime;
		}
		else if (Input.GetButtonDown("Ability1_P" + playerNum)){
			ability1CooldownCounter = DoAbility (ability1);
		}

		if (ability2CooldownCounter > 0) {
			ability2CooldownCounter -= Time.deltaTime;
		}
		else if (Input.GetButtonDown("Ability2_P" + playerNum)){
			ability2CooldownCounter = DoAbility (ability2);
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

	float DoAbility(Ability ab){
		if (ab == Ability.BasicAttack) {
			return BasicAttack ();
		} else if (ab == Ability.Fireball) {
			return ThrowFireball ();
		} else if (ab == Ability.Lunge) {
			return Lunge ();
		} else if (ab == Ability.Sing) {
			return Sing ();
		} else if (ab == Ability.Shield) {
			return Shield ();
		}
		return 0f;
	}



	float BasicAttack(){
		GameObject basicAttack = Instantiate (basicAttackPrefab, transform, false);
		BasicAttack ba = basicAttack.GetComponent<BasicAttack> ();
		ba.Init (gameObject);
		return ba.cooldown;
	}

	float ThrowFireball(){
		int directionFacing = 1;
		if (transform.localScale.x > 0) {
			directionFacing *= -1;
		}
		Vector3 fireballOffset = directionFacing * 2 * Vector3.right;
		GameObject fireball = Instantiate (fireballPrefab, transform.position + fireballOffset, Quaternion.identity);
		Fireball fc = fireball.GetComponent<Fireball> ();
		fc.Init (gameObject);

		return fc.cooldown;
	}

	float Lunge(){
		GameObject lunge = Instantiate (lungePrefab, transform, false);
		Lunge lun = lunge.GetComponent<Lunge> ();
		lun.Init (gameObject);
		return lun.cooldown;
	}

	float Sing(){
		GameObject musicBubble = Instantiate (musicBubblePrefab, transform.position, Quaternion.identity);
		MusicBubble mb = musicBubble.GetComponent<MusicBubble> ();
		mb.Init (gameObject);
		return mb.cooldown;
	}

	float Shield(){
		return 0f;
	}

	void Die(){
		Destroy (gameObject);
	}


		

	public void TakeHit(int damageTaken, float baseKnockback, float knockbackGrowth, Vector3 knockbackVector){
		damage += damageTaken;
		float knockbackMagnitude = baseKnockback + (knockbackGrowth * damage * knockbackDamageGrowthFactor);
		Stun(knockbackMagnitude * hitstunFactor);
		rb.velocity = knockbackMagnitude * knockbackVector;
	}

	public void Stun(float hitstun){
		timeUntilActionable = hitstun;
		inHitstun = true;
		rb.velocity = Vector3.zero;
		GetComponent<SpriteRenderer> ().color = Color.red;
	}

	public void InitiateAction(float actionDuration){
		timeUntilActionable = actionDuration;
		rb.velocity = Vector3.zero;
		actionInProcess = true;
	}
}
