using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //start a list to put the spawnpoint
        spawnpoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        usedIndices = new List<int>();
        ToySpawner();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToySpawner()
    {
        GenerateToy(calculator);
        GenerateToy(cat);
        GenerateToy(dog);
        GenerateToy(kazoo);
    }

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
        Instantiate(toyPrefab, spawnpoint[spawnPointIndex].transform.position, Quaternion.identity);
        GameObject toy = Instantiate(toyPrefab, spawnpoint[spawnPointIndex].transform.position, Quaternion.identity) as GameObject;
        return toy;
    }
}
