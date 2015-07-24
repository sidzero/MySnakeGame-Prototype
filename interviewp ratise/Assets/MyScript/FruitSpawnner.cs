using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitSpawnner : MonoBehaviour {
	public GameObject Fruit;
	List <Transform> fruitList;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnFruit()
	{


		GameObject temp = Instantiate (Fruit);
		fruitList.Add (temp.transform);
	}
}
