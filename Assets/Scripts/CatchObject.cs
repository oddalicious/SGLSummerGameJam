using UnityEngine;
using System.Collections;

public class CatchObject : MonoBehaviour {

	Player p;

	public Player.State stateEffect;
	public Spawner spawner;
	bool active = false;

	// Use this for initialization
	void Start() {
		p = FindObjectOfType<Player>();
	}

	// Update is called once per frame
	void Update() {
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(-20, transform.position.y), Time.deltaTime * 3);
		if (transform.position.x <= -20)
			Destroy(this.gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Triggering");
		if (other.tag.Equals("Player")) {
			Debug.Log("Entering Collider");
			p.SwapState(stateEffect);
			p.collisionCount++;
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("Exiting");
		if (other.tag.Equals("Player")) {
			Debug.Log("Exiting Collider");
			p.collisionCount--;
		}
		if (other.tag.Equals("Spawner")) {
			spawner.SpawnObject();
		}
	}
}