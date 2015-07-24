using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIscript : MonoBehaviour {
	public Text text,GameOverText;
	//event system
	SnakeEventManager eventSystem;
	// Use this for initialization
	void Start () {
		eventSystem = SnakeEventManager.Instance;
		eventSystem.UpdateScore += UpdateText;
		eventSystem.GameOver+=GameOver;
		eventSystem.Restart += Restart;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateText()
	{

		if(text)
		text.text = (GameMain.Instance.score).ToString();

	}

	public void GameOver()
	{

		GameOverText.text="Score "+GameMain.Instance.score+"\nGame OVER-Press F1 to Restart";

	}
	public void Restart()
	{

		GameOverText.text = "";

	}
}
