using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Button PlayerButton;
	private bool _isActive;
	public bool IsActive 
	{
		get
		{
			return this._isActive;
		}
		set
		{
			// Changes the button colour depending whether it's active.
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
	public int PlayerId { get; set; }
}
