using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	
	public Button[] Buttons;
	private Score _score = new Score();
	private Stopwatch _stopwatch = new Stopwatch();
	
	async void Start () 
	{
		_stopwatch.Start();
		var random = new System.Random();
		var randomizedTime = random.Next(1000, 10000);
		// var playerWhoTapped = GetButtonTap(Buttons);
	}

	static async Task<int> GetButtonTap(IEnumerable<Button> buttons)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() =>
				taskCompletionSource.SetResult(pair.index));
		
		return await taskCompletionSource.Task;
	}
}