using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
// Use this for initialization
	public GameObject[] groups;
	void Start(){
	spawnNext();
}
public void spawnNext() {
	// Random Index
	int i = Random.Range(0, groups.Length);

	// Spawn Group at current Position
	Instantiate(groups[i],
		transform.position,
		Quaternion.identity);
}
}
