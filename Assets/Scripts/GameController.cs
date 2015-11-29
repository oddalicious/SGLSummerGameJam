using UnityEngine;

public class GameController : MonoBehaviour {

	public Spawner[] spawners;
	public bool gamePaused = false;
	public float gameSpeed = 1f;
	public float gameSpeedIncrease = 0.001f;
	public int totalCoalCount = 30;

	[SerializeField]
	private UnityEngine.UI.Image ww3Image;

	private bool coalTriggered;
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
		if (totalCoalCount <= 0) {
			WW3();
		}
	}

	void WW3() {
		Time.timeScale = 0.0f;
		if (!ww3Image.gameObject.activeSelf) {
			ww3Image.gameObject.SetActive(true);
		}
		else {
			if (Input.anyKeyDown || Input.touchCount > 0) {
				GetComponent<LevelController>().LoadWW3End();
			}
		}
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
