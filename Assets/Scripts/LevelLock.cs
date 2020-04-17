using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour {

	public string levelNr;
	public string levelName;
	public bool isLocked;
	public int unlockCost;
	public GameObject unlockBtn;
	public string levelDescr;

	void Awake()
	{
		if(PlayerPrefs.HasKey("LevelUnlocked" + levelNr))
			isLocked = false;
	}

}
