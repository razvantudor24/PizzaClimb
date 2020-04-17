using UnityEngine;
using System.Collections;

public class CharacterLock : MonoBehaviour {

	public string characterName;
	public string carNameDisplay;
	public string carDescrDisplay;
	public bool isLocked;
	public int unlockCost;

	void Awake()
	{
		if(PlayerPrefs.HasKey("CharacterUnlocked" + characterName))
			isLocked = false;
	}


}
