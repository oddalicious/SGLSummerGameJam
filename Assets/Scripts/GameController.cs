using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private Player player;
	private SpillMotion spill;
	public Spawner[] spawners;
	public bool gamePaused = false;
	public float gameSpeed = 1f;

	void Start() {
		spill = GetComponent<SpillMotion>();
		player = FindObjectOfType<Player>();
	}

	// Update is called once per frame
	void Update() {
		if (player.popularity < 30 && !spill.enabled) {
			//Initiate Spill Motion
			Disable();
		}
		gameSpeed += Time.deltaTime * 0.1f;
	}

	public void Continue() {
		ToggleSpawners(true);
	}

	public void Disable() {
		ToggleSpawners(false);
		spill.enabled = true;
		gamePaused = true;
	}

	public void ToggleSpawners(bool input) {
		foreach (Spawner s in spawners) {
			s.gameObject.SetActive(input);
		}
	}
}
