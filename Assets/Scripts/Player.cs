using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Spawner highSpawner;
    public Spawner midSpawner;
    public Spawner lowSpawner;
    public Vector2 moveLocation;
    public Resource activeResource;

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
        moveLocation = transform.position;
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

    public void SetState(State _otherState, float _scoreGain, float _popularityGain, bool _triggered) {

        scoreGain = _scoreGain;
        popularityGain = _popularityGain;

        switch (_otherState) {
            case State.Default:
                myRender.sprite = defaultFace;
                break;
            case State.Evil:
                if (state == _otherState && !_triggered) {
                    modifier += 0.5f;
                }
                myRender.sprite = evilFace;
                modifier += 1f;
                break;
            case State.Happy:
                if (state == _otherState && !_triggered) {
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
                if (state == _otherState && !_triggered) {
                    modifier += 0.5f;
                }
                myRender.sprite = celebrateFace;
                if (!_triggered) {
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
        /*
         *   Platform Specific Code
         */
#if UNITY_IOS || UNITY_ANDROID
        //Handle Touch
        if (Input.touchCount > 0) {
            Touch();
        }
#elif UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        //Handle Axis
        //Cancel
        if (Input.GetAxis("Cancel") > 0 || Input.GetAxis("Cancel") < 0)
            SceneManager.LoadScene("Menu");
        //Handle Up button
        if (Input.GetAxis("Vertical") < 0) {
            Up();
        }
        //Handle Down Button
        if (Input.GetAxis("Vertical") > 0) {
            Down();
        }
#endif
        //Move to new position if away from
        if (Vector2.Distance(transform.position, moveLocation) > 0.01f)
            transform.position = Vector2.Lerp(transform.position, moveLocation, Time.deltaTime * 7);
    }

    void Touch() {
        Touch t = Input.GetTouch(0);
        if (t.position.y > Screen.height / 2) {
            Down();
        } else
            Up();
    }

    void Up() {
        if (inputcooldown < 0.0f) {
            switch (location) {
                case 1:
                    location = 0;
                    moveLocation.y = midSpawner.transform.position.y;
                    break;
                case 0:
                    location = -1;
                    moveLocation.y = lowSpawner.transform.position.y;
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
                    moveLocation.y = highSpawner.transform.position.y;
                    break;
                case -1:
                    location = 0;
                    moveLocation.y = midSpawner.transform.position.y;
                    break;
                default:
                    break;
            }
            inputcooldown = maxInputCooldown;
        }
    }
}