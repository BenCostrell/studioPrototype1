using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	public int playerNum;
	private Animator anim;
    public List<string> playerToys;
    

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

        playerToys = new List<string>();
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

		if (direction.x * transform.localScale.x > 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

	void DetectActionInput(){
		if (Input.GetButtonDown ("BasicAttack_P" + playerNum)) {
			BasicAttack ();
			Debug.Log ("player " + playerNum + " attacking");
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
		anim.SetTrigger ("basicAttack_P" + playerNum);
	}
}
