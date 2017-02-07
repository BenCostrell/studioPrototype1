using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

	public float cooldown;
	public GameObject parentPlayer;
	public float castDuration;
	public string animTrigger;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Init(GameObject player){
		parentPlayer = player;
		player.GetComponent<PlayerController> ().InitiateAction (castDuration);
		player.GetComponent<PlayerController> ().anim.SetTrigger (animTrigger);
	}
}
