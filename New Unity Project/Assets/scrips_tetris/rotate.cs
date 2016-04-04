using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public string cname;
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
			Group group = FindObjectOfType<Group>();
			group.gameObject.SendMessage("Rotate");
			//Debug.Log(cname + " is Clicked");
		}
	}
	void OnTriggerExit(Collider _other){
		if (other == _other) {
			other = null;
		}
	}
}
