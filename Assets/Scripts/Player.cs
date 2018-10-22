using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public Text ScoreText;
	private int _score = 0;
	private bool _isActive = true;

	public Button PlayerButton { get; set; }
	public int Score 
	{ 
		get
		{
			return _score;
		}
		set
		{
			ScoreText.text = _score.ToString();
			_score = value;
		}
	}
	public bool IsActive 
	{
		get
		{
			return this._isActive;
		}
		set
		{
			// Changes the button colour depending on whether it's active or not.
			if (value)
			{
				PlayerButton.GetComponent<Image>().color = new Color32(0, 255, 0 , 255);
			}
			else
			{
				PlayerButton.GetComponent<Image>().color = new Color32(120, 120, 120 , 255);
			}

			this._isActive = value;
		}
	}

	private void Start ()
	{
		this.PlayerButton = GetComponent<Button>();
	}
}
