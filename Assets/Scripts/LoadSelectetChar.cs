using UnityEngine;
using System.Collections;

public class LoadSelectetChar : MonoBehaviour {

	private GameObject[] characterList;
	private int index = 0;
	// Use this for initialization
	private void Start () {

		index = PlayerPrefs.GetInt ("Character");
		//Debug.Log ("index saved = " + index);

		characterList = new GameObject[transform.childCount];

		//fill the array
		for (int i = 0; i < transform.childCount; i++) 
		{
			characterList [i] = transform.GetChild (i).gameObject;
		}
		//disable the renderer
		foreach (GameObject go in characterList) 
			go.SetActive (false);

		//enale index 0
		if (characterList [index])
			characterList [index].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
