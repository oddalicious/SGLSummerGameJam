using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : MonoBehaviour {

	[SerializeField]
	List<GameObject> spawnableObjects;

	// Use this for initialization
	void Start() {
		SpawnObject();
	}

	void OnTriggerExit2D(Collider2D other) {
		SpawnObject();
	}

	public void SpawnObject() {
		int colID = Random.Range(0, spawnableObjects.Count);
		GameObject spawned = Instantiate(spawnableObjects[colID], transform.position, transform.rotation) as GameObject;
		spawned.GetComponent<CatchObject>().spawner = this;
	}
}
