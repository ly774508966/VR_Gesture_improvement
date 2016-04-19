using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class restartm : MonoBehaviour {

	private Collider other;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider _other){
		if (other == null) {
			other = _other;
			//TODO:
			SceneManager.LoadScene("Tetris_Mounted");
			//Debug.Log(cname + " is Clicked");*/

		}
	}
	void OnTriggerExit(Collider _other){
		if (other == _other) {
			other = _other;
			other = null;
		}
	}
}
