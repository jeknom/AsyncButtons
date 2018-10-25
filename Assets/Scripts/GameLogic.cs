using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour 
{
	public Button[] Buttons;
	public Text[] Texts;

	async void Start () 
	{
		foreach (var text in Texts)
			text.text = Players.Scores[Array.IndexOf(Texts, text)].ToString();

		var completedTask = await Task.WhenAny(AllowedTap(Buttons, TimeSpan.FromSeconds(2)), UnallowedTap(Buttons));
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	static async Task AllowedTap(IEnumerable<Button> buttons, TimeSpan delay)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		await Task.Delay(delay);
		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
		{
			pair.button.onClick.RemoveAllListeners();
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));
		}

		foreach (var button in buttons)
			button.GetComponent<Image>().color = new Color32(0, 255, 0, 255);

		var result = await taskCompletionSource.Task;
		Players.Scores[result] ++;
	}

	static async Task UnallowedTap(IEnumerable<Button> buttons)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));

		var result = await taskCompletionSource.Task;

		Players.Scores[result]--;
		for (var i = 0; i < Players.Scores.Length; i++)
		{
			if (Players.Scores[i] != result)
				Players.Scores[i]++;
		}
	}
}

public static class Players
{
	public static int[] Scores = new int[4];
}