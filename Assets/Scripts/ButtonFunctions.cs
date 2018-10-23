using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour {
	public static async Task<int> GetTap(IEnumerable<Player> players)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(players, (player, index) => new {player, index}))
				pair.player.PlayerButton.onClick.AddListener(() =>
					taskCompletionSource.SetResult(pair.index));
		
		var result = await taskCompletionSource.Task;

		// Removes all button listeners so that the tasks won't be bound to the buttons
		foreach (var player in players)
			player.PlayerButton.onClick.RemoveAllListeners();

		return result;
	}

	public static IEnumerator SetActiveAfterDelay(Player[] players, TimeSpan delay)
	{
		yield return new WaitForSeconds((int)delay.TotalSeconds);

		foreach (var player in players)
			player.ButtonIsActive = true;
	}
}