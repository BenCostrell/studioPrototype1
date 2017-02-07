using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToySceneManager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public GameObject playerPrefab;
    public Vector3 player1Spawn;
    public Vector3 player2Spawn;
	public RuntimeAnimatorController player1Anim;
	public RuntimeAnimatorController player2Anim;
    public Image transitionImage;

	public GameObject gameInfo;
	private Dictionary<string, Ability.Type> toyAbilityDict;

    AudioSource audioSource;
    AudioClip audioClip;

    public AudioClip nerdVO;
    public AudioClip rockstarVO;
    public AudioClip davidVO;
    public AudioClip jdSalingerVO;
    public AudioClip politicianVO;
    public AudioClip architectVO;

    bool fadeToBlack;
    float timeElapsed;

    // Use this for initialization
    void Start()
    {
		CreateToyAbilityDict ();
		InitializePlayers();
        audioSource = Camera.main.GetComponent<AudioSource>();
        audioClip = Camera.main.GetComponent<AudioClip>();

        fadeToBlack = false;
        transitionImage.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<PlayerController>().playerToys.Count == 2 && player2.GetComponent<PlayerController>().playerToys.Count == 2)
        {
            VoiceOverDebug();

            if (audioSource.isPlaying == false && fadeToBlack == false)
            {
                StartPlayerOneVO();
            }

        }

        if (fadeToBlack == true)
        {
            Debug.Log("FadeToBlack");
            timeElapsed += Time.deltaTime;
            transitionImage.canvasRenderer.SetAlpha(Mathf.Lerp(0, 1, timeElapsed / 2));
            StartCoroutine(ChangeScene(2));
        }
    }

    void InitializePlayers()
    {
        player1 = Instantiate(playerPrefab, player1Spawn, Quaternion.identity) as GameObject;
        player1.GetComponent<SpriteRenderer>().sprite = player1Sprite;
        player1.GetComponent<PlayerController>().playerNum = 1;
		player1.GetComponent<Animator> ().runtimeAnimatorController = player1Anim;

        player2 = Instantiate(playerPrefab, player2Spawn, Quaternion.identity) as GameObject;
        player2.GetComponent<SpriteRenderer>().sprite = player2Sprite;
        player2.GetComponent<PlayerController>().playerNum = 2;
		player2.GetComponent<Animator> ().runtimeAnimatorController = player2Anim;
    }

	void CreateToyAbilityDict(){
		toyAbilityDict = new Dictionary<string, Ability.Type> ();

		toyAbilityDict.Add ("Cat", Ability.Type.Shield);
		toyAbilityDict.Add ("Dog", Ability.Type.Lunge);
		toyAbilityDict.Add ("Kazoo", Ability.Type.Sing);
		toyAbilityDict.Add ("Calculator", Ability.Type.Fireball);
	}

    string GetArchetype(List<string> toyList)
    {
        if (toyList.Contains("Cat") && toyList.Contains("Calculator"))
        {
            return "Nerd";
        }

        if (toyList.Contains("Cat") && toyList.Contains("Kazoo"))
        {
            return "JD Salinger";
        }

        if (toyList.Contains("Cat") && toyList.Contains("Dog"))
        {
            return "David";
        }

        if (toyList.Contains("Dog") && toyList.Contains("Calculator"))
        {
            return "Politician";
        }

        if (toyList.Contains("Dog") && toyList.Contains("Kazoo"))
        {
            return "Rockstar";
        }

        if (toyList.Contains("Kazoo") && toyList.Contains("Calculator"))
        {
            return "Architect";
        }

        return "";
    }

    void StartPlayerOneVO()
    {
        string player1Archetype = GetArchetype(player1.GetComponent<PlayerController>().playerToys);
        if (player1Archetype == "Nerd")
        {
            Debug.Log("NERD VO IS PLAYING");
            audioSource.clip = nerdVO;
            audioSource.Play();
            StartCoroutine(waitToPlay(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player1Archetype == "Rockstar")
        {
            Debug.Log("ROCKSTAR VO IS PLAYING");
            audioSource.clip = rockstarVO;
            audioSource.Play();
            StartCoroutine(waitToPlay(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player1Archetype == "Architect")
        {
            Debug.Log("ARCHITECT VO IS PLAYING");
            audioSource.clip = architectVO;
            audioSource.Play();
            StartCoroutine(waitToPlay(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player1Archetype == "David")
        {
            Debug.Log("DAVID VO IS PLAYING");
            audioSource.clip = davidVO;
            audioSource.Play();
            StartCoroutine(waitToPlay(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player1Archetype == "Politician")
        {
            Debug.Log("POLITICIAN VO IS PLAYING");
            audioSource.clip = politicianVO;
            audioSource.Play();
            StartCoroutine(waitToPlay(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player1Archetype == "JD Salinger")
        {
            Debug.Log("JD SALINGER VO IS PLAYING");
            audioSource.clip = jdSalingerVO;
            audioSource.Play();
            StartCoroutine(waitToPlay(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }
    }

    void StartPlayerTwoVO()
    {
        Debug.Log("StartPlayerTwoVO is working");
        string player2Archetype = GetArchetype(player2.GetComponent<PlayerController>().playerToys);
        if (player2Archetype == "Rockstar")
        {
            Debug.Log("ROCKSTAR VO IS PLAYING");
            audioSource.clip = rockstarVO;
            audioSource.Play();
            StartCoroutine(waitToTransition(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player2Archetype == "Architect")
        {
            Debug.Log("ARCHITECT VO IS PLAYING");
            audioSource.clip = architectVO;
            audioSource.Play();
            StartCoroutine(waitToTransition(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player2Archetype == "David")
        {
            Debug.Log("DAVID VO IS PLAYING");
            audioSource.clip = davidVO;
            audioSource.Play();
            StartCoroutine(waitToTransition(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player2Archetype == "Politician")
        {
            Debug.Log("POLITICIAN VO IS PLAYING");
            audioSource.clip = politicianVO;
            audioSource.Play();
            StartCoroutine(waitToTransition(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }

        if (player2Archetype == "JD Salinger")
        {
            Debug.Log("ROCKSTAR VO IS PLAYING");
            audioSource.clip = jdSalingerVO;
            audioSource.Play();
            StartCoroutine(waitToTransition(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }
        if (player2Archetype == "Nerd")
        {
            Debug.Log("NERD VO IS PLAYING");
            audioSource.clip = nerdVO;
            audioSource.Play();
            StartCoroutine(waitToTransition(audioSource.clip.length));
            Debug.Log("audio is playing = " + audioSource.isPlaying);
        }
    }

    void StartTransition()
    {
        fadeToBlack = true;
    }

    IEnumerator waitToPlay(float time)
    {
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        StartPlayerTwoVO();
    }

    IEnumerator waitToTransition(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.Stop();
        StartTransition();
    }

    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
		SetGameInfo ();
        SceneManager.LoadScene("fightRoom");
    }

	void SetGameInfo(){
		GameInfo gi = gameInfo.GetComponent<GameInfo> ();
		gi.player1Abilities = GetAbilityListFromToyList(player1.GetComponent<PlayerController>().playerToys);
		gi.player2Abilities = GetAbilityListFromToyList(player2.GetComponent<PlayerController>().playerToys);
	}

	List<Ability.Type> GetAbilityListFromToyList(List<string> toylist){
		List<Ability.Type> abilityList = new List<Ability.Type> ();
		foreach (string toy in toylist) {
			Ability.Type ability; 
			toyAbilityDict.TryGetValue (toy, out ability);
			abilityList.Add (ability);
		}
		return abilityList;
	}

    void VoiceOverDebug()
    {
        Debug.Log("Player1 has the " + player1.GetComponent<PlayerController>().playerToys[0] + " and the " + player1.GetComponent<PlayerController>().playerToys[1]);

        Debug.Log("Player2 has the " + player2.GetComponent<PlayerController>().playerToys[0] + " and the " + player2.GetComponent<PlayerController>().playerToys[1]);

        //player1 archetypes
        FindToyCombos(player1, "Cat", "Calculator", "NERD");
        FindToyCombos(player1, "Cat", "Kazoo", "JD SALINGER");
        FindToyCombos(player1, "Dog", "Cat", "DAVID");
        FindToyCombos(player1, "Dog", "Kazoo", "ROCKSTAR");
        FindToyCombos(player1, "Dog", "Calculator", "POLITICIAN");
        FindToyCombos(player1, "Calculator", "Kazoo", "ARCHITECT");

        //player2 archetypes
        FindToyCombos(player2, "Cat", "Calculator", "NERD");
        FindToyCombos(player2, "Cat", "Kazoo", "JD SALINGER");
        FindToyCombos(player2, "Dog", "Cat", "DAVID");
        FindToyCombos(player2, "Dog", "Kazoo", "ROCKSTAR");
        FindToyCombos(player2, "Dog", "Calculator", "POLITICIAN");
        FindToyCombos(player2, "Calculator", "Kazoo", "ARCHITECT");


    }

    void FindToyCombos(GameObject player, string toy1, string toy2, string archetpye)
    {
        if ((player.GetComponent<PlayerController>().playerToys.Contains(toy1) && (player.GetComponent<PlayerController>().playerToys.Contains(toy2))))
        {
            Debug.Log(player.name + " is a " + archetpye + "!!!");
        }
    }
}
