using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Spawner : MonoBehaviour {

	[SerializeField]
	List<GameObject> spawnableObjects;

	// Use this for initialization
	void Start() {
		SpawnObject();
		spawnableObjects = spawnableObjects.OrderBy(n => n.GetComponent<Resource>().spawnRate).ToList();
	}

	void OnTriggerExit2D(Collider2D other) {
		SpawnObject();
	}

	public void SpawnObject() {
		GameObject spawned;
		while (true) {
			foreach (GameObject resource in spawnableObjects) {
				if (Random.Range(0f, 1f) <= resource.GetComponent<Resource>().spawnRate) {
					spawned = Instantiate(resource, transform.position, transform.rotation) as GameObject;
					spawned.GetComponent<Resource>().spawner = this;
					return;
				}
			}
		}
	}
}
