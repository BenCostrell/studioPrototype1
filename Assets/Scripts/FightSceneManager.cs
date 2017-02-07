using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public GameObject cooldownUIP1A1;
	public GameObject cooldownUIP1A2;
	public GameObject cooldownUIP2A1;
	public GameObject cooldownUIP2A2;
	public Sprite fireballUI;
	public Sprite lungeUI;
	public Sprite shieldUI;
	public Sprite singUI;
	private Dictionary<Ability.Type, Sprite> spriteDict;

	// Use this for initialization
	void Awake () {
		gameInfo = GameObject.FindGameObjectWithTag ("GameInfo");
		InitializeSpriteDict ();
		InitializePlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Reset")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}


	void InitializeSpriteDict(){
		spriteDict = new Dictionary<Ability.Type, Sprite> ();
		spriteDict.Add (Ability.Type.Fireball, fireballUI);
		spriteDict.Add (Ability.Type.Lunge, lungeUI);
		spriteDict.Add (Ability.Type.Shield, shieldUI);
		spriteDict.Add (Ability.Type.Sing, singUI);
	}

	void InitializePlayers(){
		List<Ability.Type> p1TestList = new List<Ability.Type>(){Ability.Type.Fireball, Ability.Type.Lunge};
		List<Ability.Type> p2TestList = new List<Ability.Type>(){Ability.Type.Sing, Ability.Type.Shield};
		bool testMode = true;

		player1 = Instantiate (playerPrefab, player1Spawn, Quaternion.identity) as GameObject;
		PlayerController pc1 = player1.GetComponent<PlayerController> ();
		pc1.inFightScene = true;
		player1.GetComponent<SpriteRenderer> ().sprite = player1Sprite;
		pc1.playerNum = 1;
		player1.GetComponent<Animator> ().runtimeAnimatorController = player1Anim;
		if (testMode) {
			pc1.abilityList = p1TestList;
		} else {
			pc1.abilityList = gameInfo.GetComponent<GameInfo> ().player1Abilities;
		}
		Sprite spriteP1A1;
		spriteDict.TryGetValue (pc1.abilityList [0], out spriteP1A1);
		cooldownUIP1A1.GetComponent<SpriteRenderer> ().sprite = spriteP1A1;
		Sprite spriteP1A2;
		spriteDict.TryGetValue (pc1.abilityList [1], out spriteP1A2);
		cooldownUIP1A2.GetComponent<SpriteRenderer> ().sprite = spriteP1A2;


		player2 = Instantiate (playerPrefab, player2Spawn, Quaternion.identity) as GameObject;
		PlayerController pc2 = player2.GetComponent<PlayerController> ();
		pc2.inFightScene = true;
		player2.GetComponent<SpriteRenderer> ().sprite = player2Sprite;
		pc2.playerNum = 2;
		player2.GetComponent<Animator> ().runtimeAnimatorController = player2Anim;
		if (testMode) {
			pc2.abilityList = p2TestList;
		} else {
			pc2.abilityList = gameInfo.GetComponent<GameInfo> ().player2Abilities;
		}
		Sprite spriteP2A1;
		spriteDict.TryGetValue (pc2.abilityList [0], out spriteP2A1);
		cooldownUIP2A1.GetComponent<SpriteRenderer> ().sprite = spriteP2A1;
		Sprite spriteP2A2;
		spriteDict.TryGetValue (pc2.abilityList [1], out spriteP2A2);
		cooldownUIP2A2.GetComponent<SpriteRenderer> ().sprite = spriteP2A2;

	}

	public void UpdateCooldownBar(int playerNum, int abilityNum, float fractionOfCooldownRemaining){
		Transform bar = null;
		if (playerNum == 1) {
			if (abilityNum == 1) {
				bar = cooldownUIP1A1.transform.GetChild (0);
			} else if (abilityNum == 2) {
				bar = cooldownUIP1A2.transform.GetChild (0);
			}
		}
		else if (playerNum == 2) {
			if (abilityNum == 1) {
				bar = cooldownUIP2A1.transform.GetChild (0);
			} else if (abilityNum == 2) {
				bar = cooldownUIP2A2.transform.GetChild (0);
			}
		}
		bar.localScale = new Vector3 (0.4f * (1 - fractionOfCooldownRemaining), bar.localScale.y, bar.localScale.z);

		if (fractionOfCooldownRemaining > 0) {
			bar.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			bar.gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		}

	}

}
