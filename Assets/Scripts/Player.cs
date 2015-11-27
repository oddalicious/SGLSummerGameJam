using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public enum State { Default, Evil, Happy, Sad, Celebrate }
	public State state;
	int location;

	private float inputcooldown = 0.25f;
	private float maxInputCooldown = 0.25f;
	public int collisionCount = 0;

	[SerializeField]
	Text scoreText;
	[SerializeField]
	Text popularityText;

	public float score;
	public float popularity;
	public float modifier = 1f;

	private float popularityGain = 0.0f;
	private float scoreGain = 0.0f;

	private SpriteRenderer myRender;

	public Sprite defaultFace;
	public Sprite evilFace;
	public Sprite happyFace;
	public Sprite sadFace;
	public Sprite celebrateFace;

	// Use this for initialization
	void Start() {
		myRender = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update() {
		HandleInput();
		UpdateValues();
		CheckLoss();
		UpdateTexts();
		if (collisionCount == 0)
			SwapState(State.Default, 0.0f, 0.5f, false);
	}

	void UpdateValues() {
		score += scoreGain * modifier;
		popularity += popularityGain * modifier;
	}

	void UpdateTexts() {
		scoreText.text = score.ToString();
		popularityText.text = popularity.ToString();
	}

	void CheckLoss() {
		if (Time.time > 10) {
			if (popularity < 0.0f) {
				Debug.Log("You Lose!");
			}
		}
	}

	public void SwapState(State otherState, float _scoreGain, float _popularityGain, bool triggered) {
		if (state == otherState) {
			modifier += 0.5f;
		}
		scoreGain = _scoreGain;
		popularityGain = _popularityGain;

		switch (otherState) {
			case State.Default:
				myRender.sprite = defaultFace;
				break;
			case State.Evil:
				myRender.sprite = evilFace;
				modifier += 1f;
				break;
			case State.Happy:
				myRender.sprite = happyFace;
				modifier += 0.5f;
				break;
			case State.Sad:
				myRender.sprite = sadFace;
				modifier = 1f;
				break;
			case State.Celebrate:
				myRender.sprite = celebrateFace;
				if (!triggered) {
					score += 150;
					popularity += 20;
				}
				modifier = (modifier < 2) ? 2 : modifier + 2;
				break;
		}
	}

	void HandleInput() {
		inputcooldown -= Time.deltaTime;

		if (Input.GetAxis("Vertical") < 0) {
			if (inputcooldown < 0.0f) {
				if (inputcooldown > 0.0f) {
					inputcooldown -= Time.deltaTime;
					return;
				}
				switch (location) {
					case 1:
						location = 0;
						break;
					case 0:
						location = -1;
						break;
					default:
						Debug.Log("Can't Move Down");
						break;
				}
				inputcooldown = maxInputCooldown;
			}
		}

		if (Input.GetAxis("Vertical") > 0) {
			if (inputcooldown < 0.0f) {

				switch (location) {

					case 0:
						location = 1;
						break;
					case -1:
						location = 0;
						break;
					default:
						Debug.Log("Can't Move Up");
						break;
				}
				inputcooldown = maxInputCooldown;
			}
		}
		if (Vector2.Distance(transform.position, new Vector2(transform.position.x, location * 2.5f)) > 0.01f)
			transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, location * 2.5f), Time.deltaTime * 7);
	}
}
