using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour 
{
	private static int[] Score = new int[4];
	public Button[] Buttons;
	public Text[] Texts;

	async void Start () 
	{
		for (var i = 0; i < Texts.Length; i++)
			Texts[i].text = Score[i].ToString();

		var randomDelay = Task.Delay(Random.Range(2000, 3000));
		var getTap = GetTap(Buttons);

		var completedTask = await Task.WhenAny(randomDelay, getTap);

		if (completedTask == getTap)
			for (var i = 0; i < Score.Length; i++)
				Score[i] = (i != getTap.Result) ? Score[i] + 1 : Score[i] - 1;
		
		else if (completedTask == randomDelay)
		{
			foreach (var button in Buttons)
				button.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
			Score[await GetTap(Buttons)]++;
		}
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	static async Task<int> GetTap(IEnumerable<Button> buttons)
	{
		var taskCompletionSource = new TaskCompletionSource<int>();

		foreach (var pair in Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));

		return await taskCompletionSource.Task;
	}
}