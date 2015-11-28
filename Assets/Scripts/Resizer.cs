using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Resizer : MonoBehaviour {

	[SerializeField]
	Image popularityField;

	float screenWidth, screenHeight;
	Vector2 screenPos;

	// Use this for initialization
	void Start() {
		screenPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0.0f));
		Debug.Log(screenPos);
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		popularityField.transform.localScale = new Vector2(screenWidth / 8, screenHeight / 10);
		popularityField.transform.position = new Vector2(screenPos.x - (popularityField.transform.localScale.x) / 2, popularityField.transform.localScale.y - screenHeight);
	}

	// Update is called once per frame
	void Update() {

	}
}
