using UnityEngine;
using System.Collections;

public class FightSceneManager : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public Sprite player1Sprite;
	public Sprite player2Sprite;
	public GameObject playerPrefab;
	public Vector3 player1Spawn;
	public Vector3 player2Spawn;
	public RuntimeAnimatorController player1Anim;
	public RuntimeAnimatorController player2Anim;

	// Use this for initialization
	void Start () {
		InitializePlayers ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializePlayers(){
		player1 = Instantiate (playerPrefab, player1Spawn, Quaternion.identity) as GameObject;
		player1.GetComponent<SpriteRenderer> ().sprite = player1Sprite;
		player1.GetComponent<PlayerController> ().playerNum = 1;
		player1.GetComponent<Animator> ().runtimeAnimatorController = player1Anim;

		player2 = Instantiate (playerPrefab, player2Spawn, Quaternion.identity) as GameObject;
		player2.GetComponent<SpriteRenderer> ().sprite = player2Sprite;
		player2.GetComponent<PlayerController> ().playerNum = 2;
		player2.GetComponent<Animator> ().runtimeAnimatorController = player2Anim;

	}
}
