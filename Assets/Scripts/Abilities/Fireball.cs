﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Attack {

	private float speed;
	private float direction;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (!GetComponent<Renderer> ().isVisible) {
			Destroy (gameObject);
		}
	}

	public override void Init(GameObject player){
		animTrigger = "ThrowFireball";
		cooldown = 1;
		castDuration = 0.2f;
		baseKnockback = 6;
		knockbackGrowth = 0.5f;
		damage = 1;
		speed = 10;

		base.Init (player);

		direction = -1 * Mathf.Sign (player.transform.localScale.x);
		GetComponent<Rigidbody2D> ().velocity = direction * new Vector2 (speed, 0);
		transform.localScale = new Vector3 (-direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	protected override Vector3 GetDirectionHit(GameObject playerHit){
		return direction * Vector3.right;
	}

	protected override void HitPlayer(GameObject player){
		base.HitPlayer (player);
		Destroy (gameObject);
	}
}
