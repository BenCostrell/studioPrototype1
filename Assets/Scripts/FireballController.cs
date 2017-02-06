using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

	public int damage;
	public float baseKnockback;
	public float knockbackGrowth;
	public float speed;
	public float cooldown;
	public float animationDuration;
	private int direction;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<Renderer> ().isVisible) {
			Destroy (gameObject);
		}
	}

	public void InitializeTrajectory(int dir){
		direction = dir;
		GetComponent<Rigidbody2D> ().velocity = direction * new Vector2 (speed, 0);
		transform.localScale = new Vector3 (-direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject collidedObject = collider.gameObject;
		if (collidedObject.tag == "Player") {
			Destroy (gameObject);
			collidedObject.GetComponent<PlayerController> ().TakeHit (damage, baseKnockback, knockbackGrowth, direction * Vector3.right);
		}
	}
}
