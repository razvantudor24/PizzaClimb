using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndConditions : MonoBehaviour {

	GameManager gameManager;
	public int itemCount;
	public GameObject brokenCoin;
	//Item itemManager;

	void Start()
	{
		//itemManager = FindObjectOfType<Item> ();
		gameManager = FindObjectOfType<GameManager> ();
		itemCount = 0;
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		//game over fail
		if (col.CompareTag ("GameOver")) {
			gameManager.GameOverDie ();
			this.GetComponent<AudioSource> ().Stop ();
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		//game over success
		else if(col.CompareTag("End"))
			{
			Debug.Log ("Game Won!!!");
			gameManager.GameOverWin ();
			this.GetComponent<AudioSource> ().Stop ();
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}

		if (col.CompareTag ("Coin")) {
			PlayerPrefs.SetInt ("Coins", col.GetComponent<Coin>().coinValue + PlayerPrefs.GetInt ("Coins"));
			gameManager.PickUpCoin ();
			Debug.Log (col.GetComponent<Coin> ().coinValue);
			col.GetComponent<AudioSource> ().Play ();
			col.GetComponent<SpriteRenderer> ().enabled = false;
			col.GetComponent<CircleCollider2D> ().enabled = false;
			Destroy (col.gameObject, 0.5f);
			GameObject broken = Instantiate (brokenCoin, col.gameObject.transform.position, col.gameObject.transform.rotation) as GameObject;
			Destroy (broken, 0.5f);
		}

		if (col.CompareTag ("Item")) {

			col.GetComponent<AudioSource> ().Play ();
			
			if (col.GetComponent<Item> ().itemIndex == 1) {
				gameManager.item1Image.SetActive (true);
				itemCount++;
			}
			else if (col.GetComponent<Item> ().itemIndex == 2) {
				gameManager.item2Image.SetActive (true);
				itemCount++;
			}
			else if (col.GetComponent<Item> ().itemIndex == 3) {
				gameManager.item3Image.SetActive (true);
				itemCount++;
			}

			if (itemCount >= 3)
				PlayerPrefs.SetInt ("Coins", 150 + PlayerPrefs.GetInt ("Coins"));
			
			col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			col.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (col.gameObject, 1);
		}
			
	}

	public IEnumerator DestroyItem(GameObject item)
	{
		yield return new WaitForSeconds (0.3f);
		Destroy (item);
	}
}
