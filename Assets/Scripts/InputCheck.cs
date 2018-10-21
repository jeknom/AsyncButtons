using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static async System.Threading.Tasks.Task<int> MysteryMethod(System.Collections.Generic.IEnumerable<UnityEngine.UI.Button> buttons)
	{
		var taskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<int>();

		foreach (var pair in System.Linq.Enumerable.Select(buttons, (button, index) => new {button, index}))
			pair.button.onClick.AddListener(() => taskCompletionSource.SetResult(pair.index));

		return await taskCompletionSource.Task;
	}
}
