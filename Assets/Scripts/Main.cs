using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour 
{
	public Player[] Players;
	private System.Random random = new System.Random();
	
	async void Start () 
	{
		while(true)
		{
			StartCoroutine(ButtonFunctions.SetActiveAfterDelay(Players, TimeSpan.FromSeconds(random.Next(3, 6))));
			var tapper = await ButtonFunctions.GetTap(Players);

			if (Players[tapper].ButtonIsActive)
				Players[tapper].Score ++;
			else
			{
				Players[tapper].Score --;

				foreach(var player in Players)
					if (Array.IndexOf(Players, player) != tapper)
						player.Score++;
			}

			StopAllCoroutines();
			foreach (var player in Players)
				player.ButtonIsActive = false;
		}
	}
}