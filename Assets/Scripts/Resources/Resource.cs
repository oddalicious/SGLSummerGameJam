using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

	[SerializeField]
	protected Player p;

	public float spawnRate = 1f;

	public Player.State stateEffect;
	public Spawner spawner;
	public bool triggered = false;
	[SerializeField]
	protected int scoreGainAmount = 1;
	[SerializeField]
	protected int popularityGainAmount = 1;

	// Use this for initialization
	protected virtual void Start() {
		p = FindObjectOfType<Player>();
		triggered = false;
	}

	// Update is called once per frame
	protected void Update() {
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(-20, transform.position.y), Time.deltaTime * 3);
		if (transform.position.x <= -20)
			Destroy(this.gameObject);

	}

	protected void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals("Player")) {
			p.SwapState(stateEffect, scoreGainAmount, popularityGainAmount, triggered);
			p.collisionCount++;
			triggered = true;
		}

	}

	protected void OnTriggerExit2D(Collider2D other) {
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