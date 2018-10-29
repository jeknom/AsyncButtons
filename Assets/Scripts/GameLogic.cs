using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Players
{
	public static int[] Score = new int[4];
}

public class GameLogic : MonoBehaviour 
{
	public Button[] Buttons;
	public Text[] Texts;

	private System.Random random = new System.Random();

	async void Start () 
	{
		for (var i = 0; i < Texts.Length; i++)
			Texts[i].text = Players.Score[i].ToString();

		var prohibitedTap = GetTap(Buttons);
		var allowedTap = ToggleButtonsAfterDelay(Buttons, TimeSpan.FromSeconds(3));

		var completedTask = await Task.WhenAny(prohibitedTap, allowedTap);
		var playerIndex = completedTask.Result;

		if (completedTask == prohibitedTap)
		{
			Players.Score[playerIndex]--;
			for (var i = 0; i < Players.Score.Length; i++)
				if (Array.IndexOf(Players.Score, Players.Score[i]) != playerIndex)
					Players.Score[i]++;
		}
		else if (completedTask == allowedTap)
			Players.Score[playerIndex]++;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	static async Task<int> GetTap(IEnumerable<Button> buttons)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));

		return await taskCompletionSource.Task;
	}

	static async Task<int> ToggleButtonsAfterDelay(Button[] buttons, TimeSpan delay)
	{
		await Task.Delay(delay);
		
		foreach (var button in buttons)
		{
			button.onClick.RemoveAllListeners();
			button.GetComponent<Image>().color = new Color(0, 255, 0, 255);
		}
		
		return await GetTap(buttons);
	}
}