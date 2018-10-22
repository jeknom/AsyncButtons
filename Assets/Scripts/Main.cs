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
			stopwatch.Start();
			var buttonsToggled = ParallelFunctions.IsToggled(Players, 3);
			var tapper = await ParallelFunctions.GetButtonTap(Players);
			
			if (buttonsToggled)
			Players[tapper].Score += 1;
		}	
	}

	
}