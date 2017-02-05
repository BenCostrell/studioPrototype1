using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		DetectActionInput ();
	}

	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxisRaw ("Horizontal_P" + playerNum), Input.GetAxisRaw ("Vertical_P" + playerNum));

		direction = direction.normalized;

		rb.velocity = speed * direction;
	}

	void DetectActionInput(){
		if (Input.GetButtonDown ("BasicAttack_P" + playerNum)) {
			BasicAttack ();
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
	}
}
