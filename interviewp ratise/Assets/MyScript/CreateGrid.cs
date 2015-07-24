using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateGrid : MonoBehaviour
{
	public GameObject cube,_Fruit;
	List<Vector3> gridPosition;
	//event system
	SnakeEventManager eventSystem;
	// Use this for initialization
	void Start ()
	{
		eventSystem = SnakeEventManager.Instance;
		eventSystem.GrowSnake += SpawnFruit;
		createGrid ();

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void createGrid ()
	{
		Vector3 viewSTart, start, end;



		float xSize = 1, ySize = 1;

		viewSTart = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, 0f));
		viewSTart.z = 0f;

		start = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0f, 0f));
		start.z = 0;


		float XmaxSize = (start - viewSTart).magnitude;
		start = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 1.0f, 0f));
		start.z = 0;
		float YmaxSize = (start - viewSTart).magnitude;


		gridPosition = new List<Vector3> ();
		for (int i=0; i<=Mathf.CeilToInt( XmaxSize); i++) 
			for (int j=0; j<=Mathf.CeilToInt(YmaxSize); j++) {
				if (i == 0 || j == 0 || (j == Mathf.CeilToInt(YmaxSize)) || (i == Mathf.CeilToInt (XmaxSize))) { //building walls only for walls

					Vector3 offset = viewSTart + new Vector3 (xSize * i, ySize * j, 1);
				GameObject temp=(GameObject)	Instantiate (cube, offset, cube.transform.rotation);
				temp.transform.SetParent(transform);	
				} 
			else if((i>2 && j<YmaxSize-2) &&(j>2 && i<XmaxSize-2)  )
			{

				Vector3 offset = viewSTart + new Vector3 (xSize * i, ySize * j, 0);
					gridPosition.Add (offset);
				}
		
		}
	}


	//spawn fruit
	void SpawnFruit()
	{

		int index = Random.Range (0,gridPosition.Count);
		 Instantiate (_Fruit,gridPosition[index],_Fruit.transform.rotation);



	}

}

