using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public enum State { Default, Evil, Happy, Sad, Celebrate, Spill }

	public State state;
	public Sprite defaultFace;
	public Sprite evilFace;
	public Sprite happyFace;
	public Sprite sadFace;
	public Sprite celebrateFace;
	public Sprite spillFace;
	public int collisionCount = 0;
	public float score;
	public float popularity;
	public float modifier = 1f;

	private int location;
	private GameController game;
	private float inputcooldown = 0.25f;
	private float maxInputCooldown = 0.25f;
	[SerializeField]
	Slider slider;
	[SerializeField]
	private Text scoreText;
	private float popularityGain = 0.0f;
	private float scoreGain = 0.0f;
	private SpriteRenderer myRender;

	// Use this for initialization
	void Start() {
		myRender = GetComponent<SpriteRenderer>();
		game = FindObjectOfType<GameController>();
		popularity = 100;
		score = 0;
	}

	// Update is called once per frame
	void Update() {
		if (!game.gamePaused) {
			HandleInput();
			UpdateValues();
			UpdateTexts();
			if (collisionCount == 0)
				SetState(State.Default, 0.0f, 0.0f, false);
		}
	}

	void UpdateValues() {
		score = Mathf.Max(score + (scoreGain * modifier), 0);
		popularity = Mathf.Clamp(popularity + popularityGain, 0, 100);
	}

	void UpdateTexts() {
		scoreText.text = Mathf.RoundToInt(score).ToString();
		slider.value = popularity;
	}

	public void SetState(State otherState, float _scoreGain, float _popularityGain, bool triggered) {

		scoreGain = _scoreGain;
		popularityGain = _popularityGain;

		switch (otherState) {
			case State.Default:
				myRender.sprite = defaultFace;
				break;
			case State.Evil:
				if (state == otherState && !triggered) {
					modifier += 0.5f;
				}
				myRender.sprite = evilFace;
				modifier += 1f;
				break;
			case State.Happy:
				if (state == otherState && !triggered) {
					modifier += 0.5f;
				}
				myRender.sprite = happyFace;
				modifier += 0.5f;
				break;
			case State.Sad:
				myRender.sprite = sadFace;
				modifier = 1f;
				break;
			case State.Celebrate:
				if (state == otherState && !triggered) {
					modifier += 0.5f;
				}
				myRender.sprite = celebrateFace;
				if (!triggered) {
					score += 150;
					popularity += 5;
				}
				modifier = 1;
				scoreGain = 0;
				popularityGain = 0;
				break;
			case State.Spill:
				myRender.sprite = spillFace;
				modifier = 0;
				popularityGain = 0;
				scoreGain = 0;
				break;
		}
	}

	void HandleInput() {
		inputcooldown -= Time.deltaTime;
		if (Input.GetAxis("Cancel") > 0 || Input.GetAxis("Cancel") < 0)
			Application.LoadLevel("Menu");
		//Handle Touch
		if (Input.touchCount > 0) {
			Touch();
		}
		//Handle Up button
		if (Input.GetAxis("Vertical") < 0) {
			Up();
		}
		//Handle Down Button
		if (Input.GetAxis("Vertical") > 0) {
			Down();
		}
		//Move to new position if there is one
		if (Vector2.Distance(transform.position, new Vector2(transform.position.x, (location * 2.5f) + 1)) > 0.01f)
			transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (location * 2.5f) + 1), Time.deltaTime * 7);
	}

	void Touch() {
		Touch t = Input.GetTouch(0);
		if (t.position.y > Screen.height / 2) {
			Down();
		}
		else if (t.position.y <= Screen.height / 2) {
			Up();
		}
	}

	void Up() {
		if (inputcooldown < 0.0f) {
			switch (location) {
				case 1:
					location = 0;
					break;
				case 0:
					location = -1;
					break;
				default:
					break;
			}
			inputcooldown = maxInputCooldown;
		}
	}
	void Down() {
		if (inputcooldown < 0.0f) {
			switch (location) {

				case 0:
					location = 1;
					break;
				case -1:
					location = 0;
					break;
				default:
					break;
			}
			inputcooldown = maxInputCooldown;
		}
	}
}