using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Snake
{
	public Transform Head;
	float moveSize;
	public List <Transform> Body;
	public LinkedList<Transform> Body1;
	Vector3 headingDirection;
	int LengthToIncrease=0;



	public void Init ()
	{
		headingDirection = new Vector3 (1, 0, 0);
		moveSize = Head.gameObject.GetComponent<MeshFilter> ().mesh.bounds.size.x;
		//Body = new List<Transform> ();
		Body1 = new LinkedList<Transform> ();
		for (int i=0; i<Body.Count; i++) {

			Transform t = Body [i];

			Body1.AddLast (t);



		}


	}

	public void Move (Vector3 NewHeadPosition)
	{
		headingDirection = NewHeadPosition;

		Vector3 prevHead = Head.transform.position;

		Head.transform.position += NewHeadPosition * moveSize;

		//check for growth
		while(LengthToIncrease>0)
		{
			Add(Body[Body.Count-1].transform);
			LengthToIncrease--;

		}


		if (Body.Count > 0) {

			if (Body.Count == 1) {
				Body [0].transform.position = prevHead;
			} else {
				Transform temp1, temp2;
		

				temp1 = Body1.First.Value;
				temp2 = Body1.Last.Value;



				temp2.position = prevHead;


				Body1.RemoveLast ();
				Body1.AddFirst (temp2);
			

				;

			}
		}
	}

	//increase leanght
	public void IncreaseSize()
	{
		LengthToIncrease++;
	}

	//roate head
	public void RotateHead(float angle)
	{
		Head.eulerAngles = new Vector3 (0,0, angle);
	}

	public void Add (Transform newBody)
	{

		newBody.gameObject.SetActive (true);
		newBody.position = Body [Body.Count - 1].position + headingDirection * moveSize;
		Body1.AddLast (newBody);
		//Body.Add (newBody);


	}

	
	
};

[System.Serializable]
public class SnakeMovement : MonoBehaviour
{

	public Snake _snake;
	public GameObject _snakeBodyType;
	Vector3 headingDirection,ResetPostion;
	Quaternion ResetRotation;
	//main class

	bool stopMovement=false;


	//event system
		SnakeEventManager eventSystem;
	// Use this for initialization
	void Start ()
	{
		eventSystem = SnakeEventManager.Instance;
		eventSystem.GameOver += F_Hit;

		headingDirection = new Vector3 (1, 0, 0);
		_snake.Init ();//update size
		StartCoroutine ("SnakeSwap");
		ResetPostion = _snake.Head.position;
		ResetRotation = _snake.Head.rotation;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!stopMovement)
		Movement ();
		if(Input.GetKeyDown(KeyCode.F1))
		{
			eventSystem.E_Restart();
			stopMovement=false;
			if(_snake.Body.Count>0)
			for(int i=0;i<_snake.Body.Count;i++)
			{
				Destroy(_snake.Body[i].transform.gameObject);
			}

			_snake.Body.Clear();
			_snake.Body1.Clear();
			 _snake.Head.position=ResetPostion;
			_snake.Head.rotation=ResetRotation;
			headingDirection=Vector3.right;
			_snake.Init();


		}
		if(Input.GetKeyDown(KeyCode.F2))
		{
			if(Time.timeScale>0)
			Time.timeScale=0;
			else
				Time.timeScale=1;

		}

	}

	void Movement ()
	{

		float val = Input.GetAxis ("Horizontal");
		Vector3 temp = headingDirection;

		//old method
		/*
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) { //right

			headingDirection.x = temp.y;
			headingDirection.y = temp.x * -1;
			_snake.RotateHead(-90);


		} 
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
			headingDirection.x = temp.y * -1;
			headingDirection.y = temp.x;
			_snake.RotateHead(90);

		}*/

		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {

			if(headingDirection!=Vector3.left &&  headingDirection!=Vector3.right)
			{headingDirection = Vector3.right;
			_snake.RotateHead (0);
		}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
			if(headingDirection!=Vector3.right &&  headingDirection!=Vector3.left)
			{
				headingDirection=Vector3.left;
				_snake.RotateHead(180);}
		
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {

			if (headingDirection != Vector3.down &&  headingDirection!=Vector3.up) {
				headingDirection = Vector3.up;
				_snake.RotateHead (90);

			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) {
			
			if (headingDirection != Vector3.up &&  headingDirection!=Vector3.down) {
				headingDirection = Vector3.down;
				_snake.RotateHead (-90);
				
			}
		}

	}
	void OnTriggerEnter(Collider other) {

		if (other.tag == "Tag_Food")
		{
			_snake.IncreaseSize();
			//Debug.Log (other.name);
			GameObject temp=(GameObject)(Instantiate(_snakeBodyType,other.transform.position,other.transform.rotation));
			Destroy(other.gameObject);
			temp.SetActive(false);
			_snake.Body.Add(temp.transform);



			eventSystem.E_GrowSnake();//fire event
		}
		else if(other.tag=="Tag_Obstacle")
		{
			eventSystem.E_GameOver();

		}
	}
	public void F_Hit()
	{

		stopMovement = true;
	}


	IEnumerator SnakeSwap ()
	{
		while (true) {
			if(!stopMovement)
			_snake.Move (headingDirection);


			yield return new WaitForSeconds (0.4f);
		}
	}


}
