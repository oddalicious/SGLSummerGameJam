using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public enum State { Default, Evil, Happy, Sad }
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

	public float gainRateEvil = 3f;
	public float gainRateHappy = 1f;
	public float gainRateSad = -1f;
	public float popularityRateEvil = -2f;
	public float popularityRateHappy = -1f;
	public float popularityRateSad = 1f;

	private SpriteRenderer myRender;

	public Sprite defaultFace;
	public Sprite evilFace;
	public Sprite happyFace;
	public Sprite sadFace;

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
			SwapState(State.Default);
	}

	void UpdateValues() {
		switch (state) {
			case State.Default:
				break;
			case State.Evil:
				score += Time.deltaTime * gainRateEvil * modifier;
				popularity += Time.deltaTime * popularityRateEvil;
				break;
			case State.Happy:
				score += Time.deltaTime * gainRateHappy * modifier;
				popularity += Time.deltaTime * popularityRateHappy;
				break;
			case State.Sad:
				score += Time.deltaTime * gainRateSad;
				popularity += Time.deltaTime * popularityRateSad;
				break;
		}
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

	public void SwapState(State otherState) {
		if (state == otherState) {
			modifier += 0.5f;
		}
		state = otherState;

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
		}
	}

	void HandleInput() {
		if (inputcooldown < 0.0f) {

			if (Input.GetAxis("Vertical") < 0) {
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
			}

			if (Input.GetAxis("Vertical") > 0) {
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
			}
			transform.position = new Vector2(transform.position.x, location * 2.5f);
			inputcooldown = maxInputCooldown;
		}
		else {
			inputcooldown -= Time.deltaTime;
		}
	}
}
