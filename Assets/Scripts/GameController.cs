using UnityEngine;

public class GameController : MonoBehaviour {

	public Spawner[] spawners;
	public bool gamePaused = false;
	public float gameSpeed = 1f;
	public float gameSpeedIncrease = 0.001f;
	public int currentCoalCount;
	public int totalCoalCount = 30;
	public int hitCount = 0;

	float endCount = 5f;

	[SerializeField]
	AudioClip siren;
	[SerializeField]
	private UnityEngine.UI.Image ww3Image;
	[SerializeField]
	private UnityEngine.UI.Image instructionImage;
	[SerializeField]
	private UnityEngine.UI.Image greensImage;
	private AudioSource aSource;
	private bool coalTriggered;
	private Player player;
	private SpillMotion spill;

	void Start() {
		currentCoalCount = totalCoalCount;
		spill = GetComponent<SpillMotion>();
		player = FindObjectOfType<Player>();
		aSource = FindObjectOfType<AudioSource>();
	}

	void OnEnable() {
		instructionImage.gameObject.SetActive(true);
		Time.timeScale = 0;
	}

	void Update() {
		if (player.popularity < 30 && !spill.enabled) {
			StartSpill();
		}
		gameSpeed += Time.deltaTime * gameSpeedIncrease;
		if (currentCoalCount <= 0) {
			WW3();
		}
		if (hitCount > 100 && player.popularity > 70) {
			Greens();
		}
	}

	public void StartGame() {
		instructionImage.gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	void WW3() {
		endCount -= 0.1f;
		Time.timeScale = 0.0f;
		if (!ww3Image.gameObject.activeSelf) {
			ww3Image.gameObject.SetActive(true);
			aSource.clip = siren;
			aSource.Play();
		}
	}

	void Greens() {
		endCount -= 0.1f;
		Time.timeScale = 0.0f;
		if (!greensImage.gameObject.activeSelf) {
			greensImage.gameObject.SetActive(true);
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
