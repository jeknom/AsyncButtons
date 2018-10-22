using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	
	public List<Button> Buttons;
	public Text[] Texts;
	private bool[] IsActive = new bool[4];
	private int[] Score = new int[4];

	private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
	
	async void Start () 
	{
		var random = new System.Random();
		foreach (var status in IsActive)
			status.Equals(true);

		while(true)
		{
			var timeUntilTapAllowed = random.Next(3, 6);
			foreach (var button in Buttons)
				button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

			_stopwatch.Start();
			StartCoroutine(ToggleButtonColorWithDelay(Buttons, timeUntilTapAllowed));
			
			var playerWhoTapped = await GetButtonTap(Buttons);
			_stopwatch.Stop();

			if (_stopwatch.ElapsedMilliseconds >= timeUntilTapAllowed * 1000)
			{
				Score[playerWhoTapped] += 1;
				for (var i = 0; i < 4; i++)
				{
					Texts[i].text = string.Format("Player {0}: {1}", i + 1, Score[i]); 
				}
			}
			else
			{
				Buttons[playerWhoTapped].GetComponent<Image>().color = new Color32(120, 120, 120, 255);
				IsActive[playerWhoTapped] = false;
			}

			_stopwatch.Reset();
		}
	}

	IEnumerator ToggleButtonColorWithDelay(List<Button> buttons, float delayInSeconds)
	{
		yield return new WaitForSeconds(delayInSeconds);
		foreach (var button in buttons)
		{
			button.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
		}
	}
	static async Task<int> GetButtonTap(IEnumerable<Button> buttons)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() =>
				taskCompletionSource.SetResult(pair.index));
		
		var result = await taskCompletionSource.Task;

		foreach (var button in buttons)
			button.onClick.RemoveAllListeners();

		return result;
	}
}