using UnityEngine;

public class GameController : MonoBehaviour {

	public Spawner[] spawners;
	public bool gamePaused = false;
	public float gameSpeed = 1f;
	public float gameSpeedIncrease = 0.001f;

	private Player player;
	private SpillMotion spill;

	void Start() {
		spill = GetComponent<SpillMotion>();
		player = FindObjectOfType<Player>();
	}

	void Update() {
		if (player.popularity < 30 && !spill.enabled) {
			StartSpill();
		}
		gameSpeed += Time.deltaTime * gameSpeedIncrease;
	}

	public void Continue() {
		ToggleSpawners(true);
	}

	public void StartSpill() {
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
