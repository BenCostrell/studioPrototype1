using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToyGeneratorScript: MonoBehaviour {

    public GameObject calculator;
    public GameObject cat;
    public GameObject kazoo;
    public GameObject dog;

    GameObject[] spawnpoint;
    List<int> usedIndices;
    

	// Use this for initialization
	void Start () {

        //find the spawnpoints
        //start a list to put the spawnpoints in when they are used
        //run the spawner
        spawnpoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        usedIndices = new List<int>();
        ToySpawner();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void ToySpawner()
    {
        //running one time for each item
        GameObject calculatorObj = GenerateToy(calculator);
        calculatorObj.GetComponent<ToyControllerScript>().toyName = "Calculator";

        GameObject catObj = GenerateToy(cat);
        catObj.GetComponent<ToyControllerScript>().toyName = "Cat";

        GameObject dogObj = GenerateToy(dog);
        dogObj.GetComponent<ToyControllerScript>().toyName = "Dog";

        GameObject kazooObj = GenerateToy(kazoo);
        kazooObj.GetComponent<ToyControllerScript>().toyName = "Kazoo";
    }

    //this is checking to see if the the spawnPoint has already been used
    //is passed the index against all the points we've used. If any of them match, then we can't use. If they don't, we can.
    bool CheckIndex(int indexToCheck)
    {
        foreach (int i in usedIndices)
        {
            if (i == indexToCheck)
            {
               return false;
            }
        }

        return true;
    }

    //returns a GameObject to be instantiated
    //the random range grabs a random index that could be used to place the object
    //if "isGood" has been returned true, then add that to the LIST of usedIndices and instantiate the object at the location
    //if "isGood" has been returned false, then run the randomRange again until it's returned true
    //isGood is set to check the index using the CheckIndex function
    GameObject GenerateToy(GameObject toyPrefab)
    {
        int spawnPointIndex = Random.Range(0, 6);
        bool isGood = CheckIndex(spawnPointIndex);
        while (!isGood)
        {
            spawnPointIndex = Random.Range(0, 6);
            isGood = CheckIndex(spawnPointIndex);
        }
        usedIndices.Add(spawnPointIndex);
        GameObject toy = Instantiate(toyPrefab, spawnpoint[spawnPointIndex].transform.position, Quaternion.identity) as GameObject;
        return toy;
    }
}
