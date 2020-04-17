using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int coinValue;
	public GameObject coinValTxt;

	void Awake()
	{
		coinValue = coinValue;
		coinValTxt.GetComponent<TextMesh> ().text = coinValue.ToString ();
	}

}
