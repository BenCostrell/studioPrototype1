using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackController : MonoBehaviour {

	public int damage;
	public float baseKnockback;
	public float knockbackGrowth;
	public bool hitboxActive;


	// Use this for initialization
	void Start () {
		hitboxActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetAttackHitbox(bool status){
		hitboxActive = status;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (hitboxActive) {
			if (collider.gameObject.tag == "Player") {
				PlayerController pc = collider.gameObject.GetComponent<PlayerController> ();
				if (pc.playerNum != transform.parent.GetComponent<PlayerController> ().playerNum) {
					Vector3 knockbackVector = (collider.transform.position - transform.parent.position).normalized;
					pc.TakeHit (damage, baseKnockback, knockbackGrowth, knockbackVector);
					hitboxActive = false;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if (!hitboxActive) {
			if (collider.gameObject.tag == "Player") {
				hitboxActive = true;
			}
		}
	}
}
