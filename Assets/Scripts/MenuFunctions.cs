using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
	public void LoadGame()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

}