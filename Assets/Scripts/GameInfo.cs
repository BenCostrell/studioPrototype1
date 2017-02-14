using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	public List<Ability.Type> player1Abilities;
	public List<Ability.Type> player2Abilities;

	// Use this for initialization
	void Start () {
		player1Abilities = new List<Ability.Type> ();
		player2Abilities = new List<Ability.Type> ();
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("ToggleFullscreen")) {
			Screen.fullScreen = !Screen.fullScreen;
		}
	}
}
