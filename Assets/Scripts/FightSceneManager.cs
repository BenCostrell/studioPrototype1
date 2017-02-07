using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	private GameObject gameInfo;


	// Use this for initialization
	void Awake () {
		gameInfo = GameObject.FindGameObjectWithTag ("GameInfo");
		InitializePlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Reset")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void InitializePlayers(){
		player1 = Instantiate (playerPrefab, player1Spawn, Quaternion.identity) as GameObject;
		PlayerController pc1 = player1.GetComponent<PlayerController> ();
		player1.GetComponent<SpriteRenderer> ().sprite = player1Sprite;
		pc1.playerNum = 1;
		player1.GetComponent<Animator> ().runtimeAnimatorController = player1Anim;
		pc1.abilityList = gameInfo.GetComponent<GameInfo> ().player1Abilities;

		player2 = Instantiate (playerPrefab, player2Spawn, Quaternion.identity) as GameObject;
		PlayerController pc2 = player2.GetComponent<PlayerController> ();
		player2.GetComponent<SpriteRenderer> ().sprite = player2Sprite;
		pc2.playerNum = 2;
		player2.GetComponent<Animator> ().runtimeAnimatorController = player2Anim;
		pc2.abilityList = gameInfo.GetComponent<GameInfo> ().player2Abilities;


	}
}
