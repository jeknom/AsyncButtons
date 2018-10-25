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

		// Delay to prevent round from starting immediately after the last round was completed.
		await Task.Delay(TimeSpan.FromSeconds(0.1));

		var roundTime = TimeSpan.FromSeconds(random.Next(2, 5));
		await Task.WhenAny(TapAfterDelay(Buttons, roundTime), TapTooEarly(Buttons));
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	static async Task TapAfterDelay(IEnumerable<Button> buttons, TimeSpan delay)
	{
		await Task.Delay(delay);

		var taskCompletionSource = new TaskCompletionSource<int>();
		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
		{
			pair.button.onClick.RemoveAllListeners();
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));
		}

		foreach (var button in buttons)
			button.GetComponent<Image>().color = new Color32(0, 255, 0, 255);

		var result = await taskCompletionSource.Task;
		Players.Score[result]++;
	}

	static async Task TapTooEarly(IEnumerable<Button> buttons)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));

		var result = await taskCompletionSource.Task;

		Players.Score[result] --;
		for (var i = 0; i < Players.Score.Length; i++)
			if (Array.IndexOf(Players.Score, Players.Score[i]) != result)
				Players.Score[i] ++;
	}
}