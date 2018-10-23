using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour {

	public static async Task<int> GetButtonTap(IEnumerable<Player> players)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(players, (player, index) => new {player, index}))
			if (pair.player.IsActive)
				pair.player.PlayerButton.onClick.AddListener(() =>
					taskCompletionSource.SetResult(pair.index));
		
		var result = await taskCompletionSource.Task;

		// Removes all button listeners so that the tasks won't be bound to the buttons
		foreach (var player in players)
			player.PlayerButton.onClick.RemoveAllListeners();

		return result;
	}

	public static IEnumerator SetGreenButtonsWithDelay(Player[] players, TimeSpan delay)
	{
		yield return new WaitForSeconds((float)delay.TotalSeconds);

		foreach (var player in players)
			if (player.IsActive)
				player.PlayerButton.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
	}
}