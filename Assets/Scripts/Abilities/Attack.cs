using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability {

	protected float baseKnockback;
	protected float knockbackGrowth;
	protected int damage;
	protected bool hitPlayer;

	// Use this for initialization
	void Start () {
		hitPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void HitPlayer(GameObject player){
		player.GetComponent<PlayerController> ().TakeHit (damage, baseKnockback, knockbackGrowth, GetDirectionHit(player));
		hitPlayer = true;
	}

	protected virtual Vector3 GetDirectionHit (GameObject playerHit){
		return Vector3.zero;
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject collidedObject = collider.gameObject;
		if (collidedObject.tag == "Player") {
			if ((collidedObject.GetComponent<PlayerController> ().playerNum != parentPlayer.GetComponent<PlayerController>().playerNum) && !hitPlayer){
				HitPlayer (collidedObject);
			}
		}
	}
}
