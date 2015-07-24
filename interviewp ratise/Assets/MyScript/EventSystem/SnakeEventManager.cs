using UnityEngine;
using System.Collections;

public class SnakeEventManager : MonoBehaviour {

	private static SnakeEventManager instance;

	private SnakeEventManager(){}
	//instnace
	public static SnakeEventManager Instance
	{
		get{
			if(instance==null)
			{
				instance=GameObject.FindObjectOfType(typeof(SnakeEventManager)) as SnakeEventManager;
			}
			return instance;
		}
	}

	//events
	public delegate void GrowSnakeEvent();
	public event GrowSnakeEvent GrowSnake;

	public delegate void GameOverEvent();
	public event GameOverEvent GameOver;
	//restart
	public delegate void RestartEvent();
	public event RestartEvent Restart;


	//update Score
	public delegate void UpdateScoreEvent();
	public event UpdateScoreEvent UpdateScore;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	public void E_GrowSnake()//fire event
	{

		GrowSnake ();

	}
	public void E_Restart()
	{
		//Debug.Log ("restarting");
		Restart ();
	}


	public void E_GameOver()
	{
		//Debug.Log ("Game OvEr");
		//if(GameOver!=null)
		GameOver ();
		//Debug.Log ("Game OvEr2");

	}

	public void E_UpdateScore()
	{
		//Debug.Log ("UpdateScore");
		UpdateScore ();

	}
}
