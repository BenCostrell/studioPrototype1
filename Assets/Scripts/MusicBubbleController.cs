using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBubbleController : MonoBehaviour {

	public float stunDuration;
	public float lifeDuration;
	public float cooldown;
	public float castDuration;
	private float timeElapsed;
	public int parentPlayerNum;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeDuration);
		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		transform.localScale = Vector3.Lerp (0.5f * Vector3.one, Vector3.one, timeElapsed / lifeDuration);
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject collidedObject = collider.gameObject;
		if (collidedObject.tag == "Player") {
			PlayerController pc = collider.gameObject.GetComponent<PlayerController> ();
			if (pc.playerNum != parentPlayerNum) {
				Destroy (gameObject);
				collidedObject.GetComponent<PlayerController> ().Stun (stunDuration);
			}
		}
	}
}
