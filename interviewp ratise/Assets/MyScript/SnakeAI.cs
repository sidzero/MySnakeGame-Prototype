using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Body
{
	public Transform transform;

};
[System.Serializable]
public class SnakeAI : MonoBehaviour {
	
	public Body []snakeBody;
	float size;
	public Vector3 headingDirection;
	// Use this for initialization
	void Start () {
		size = snakeBody [0].transform.gameObject.GetComponent<MeshFilter> ().mesh.bounds.size.x;
		headingDirection = Vector3.right;

		StartCoroutine ("swap");
	
	}
	
	// Update is called once per frame
	void Update () {

		//swapMove ();

		if (Input.GetKey(KeyCode.RightArrow))
			headingDirection = Vector3.right;
		else if (Input.GetKey(KeyCode.DownArrow))
			headingDirection = -Vector3.up;    // '-up' means 'down'
		else if (Input.GetKey(KeyCode.LeftArrow))
			headingDirection = -Vector3.right; // '-right' means 'left'
		else if (Input.GetKey(KeyCode.UpArrow))
			headingDirection = Vector3.up;

	}

	void swapMove()
	{





	}
	IEnumerator swap()
	{

		while (true) {
			Vector3 v = snakeBody [0].transform.position;//safe old position
			snakeBody [0].transform.position += headingDirection * size;//move it by sme size
			snakeBody [2].transform.position = v;//take last part and put it in this place


			Body temp = snakeBody [1];
			snakeBody [1] = snakeBody [2];
			snakeBody [2] = temp;
			yield return new WaitForSeconds (0.2f);
		}

	}





}
