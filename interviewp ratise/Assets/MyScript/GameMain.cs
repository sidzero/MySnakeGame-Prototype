using UnityEngine;
using System.Collections;
public enum GameState
{
	playing,won,lost,pasued,menu


};
public class GameMain : MonoBehaviour {

	public static GameMain instance;
	//instnace
	public static GameMain Instance
	{
		get{
			if(instance==null)
			{
				instance=GameObject.FindObjectOfType(typeof(GameMain)) as GameMain;
			}
			return instance;
		}
	}

	public GameState _Currentstate;
	public int level=1,score=0;
	//event system
	SnakeEventManager eventSystem;

	// Use this for initialization
	void Start () {

		_Currentstate = GameState.menu;
		eventSystem = SnakeEventManager.Instance;
		eventSystem.GrowSnake +=IncreaseScore;//bfr gameover was here dnt kknw y
		eventSystem.Restart += Restart ;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameOver()
	{

		_Currentstate = GameState.lost;


	}
	public void IncreaseScore()
	{
		score = score+10;
		eventSystem.E_UpdateScore ();
	}

	public void Restart()
	{
		score = 0;
		eventSystem.E_UpdateScore ();
	}
}
