using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public Text ScoreText;
	private int _score = 0;
	private bool _buttonIsActive = false;

	public Button PlayerButton { get; set; }
	public bool ButtonIsActive 
	{ 
		get
		{
			return _buttonIsActive;
		} 
		set
		{
			_buttonIsActive = value;

			if (value)
			{
				PlayerButton.GetComponent<Image>().color = new Color32(0,255,0,255);
			}
			else
			{
				PlayerButton.GetComponent<Image>().color = new Color32(255,255,255,255);
			}
		}
	}

	public int Score 
	{ 
		get
		{
			return _score;
		}
		set
		{
			_score = value;
			ScoreText.text = _score.ToString();
		}
	}

	private void Start ()
	{
		this.PlayerButton = GetComponent<Button>();
	}
}