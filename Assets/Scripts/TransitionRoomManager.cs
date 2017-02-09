using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionRoomManager : MonoBehaviour {

    public AudioClip transitionVO;

    AudioSource audioSource;
    AudioClip audioClip;

    // Use this for initialization
    void Start () {

        audioSource = Camera.main.GetComponent<AudioSource>();
        audioClip = Camera.main.GetComponent<AudioClip>();

        audioSource.clip = transitionVO;
        audioSource.Play();

    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(ChangeScene(audioSource.clip.length));


		
	}

    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("fightRoom");
    }
}
