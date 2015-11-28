using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

	public bool spill = false;
	public float spawnRate = 1f;
	public int speed = 3;
	public Player.State stateEffect;
	public Spawner spawner;
	public bool triggered = false;

	[SerializeField]
	protected float scoreGainAmount = 1.0f;
	[SerializeField]
	protected float popularityGainAmount = 1.0f;

	protected GameController game;
	protected Player player;

	protected virtual void Start() {
		player = FindObjectOfType<Player>();
		triggered = false;
		game = FindObjectOfType<GameController>();
	}

	protected virtual void Update() {
		if (!game.gamePaused) {
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(-20, transform.position.y), Time.deltaTime * speed * game.gameSpeed);
			if (transform.position.x <= -20)
				Destroy(this.gameObject);
		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals("Player")) {
			player.SetState(stateEffect, scoreGainAmount, popularityGainAmount, triggered);
			player.collisionCount++;
			triggered = true;
		}

	}

	protected virtual void OnTriggerExit2D(Collider2D other) {
		if (other.tag.Equals("Player")) {
			player.collisionCount--;
		}
		if (other.tag.Equals("Spawner")) {
			spawner.SpawnObject();
		}
	}
}