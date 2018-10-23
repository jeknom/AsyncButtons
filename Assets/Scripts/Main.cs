using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour 
{
	public Player[] Players;
	
	async void Start () 
	{
		var stopwatch = new System.Diagnostics.Stopwatch();
		var random = new System.Random();

		while (true)
		{
			var roundTime = TimeSpan.FromSeconds(random.Next(2, 3));
			StartCoroutine(ButtonFunctions.SetGreenButtonsWithDelay(Players, roundTime));
			stopwatch.Start();
			
			var tapper = await ButtonFunctions.GetButtonTap(Players);
			stopwatch.Stop();

			if (stopwatch.Elapsed.TotalSeconds >= roundTime.TotalSeconds)
			{
				Players[tapper].Score ++;
				Debug.Log(Players[tapper].Score);
			}
			else
			{
				Players[tapper].IsActive = false;
				Debug.Log("Tap too soon!");
			}

			stopwatch.Reset();
			foreach (var player in Players)
				if (player.IsActive)
					player.PlayerButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

			Debug.Log(stopwatch.Elapsed.TotalSeconds);
		}	
	}
}