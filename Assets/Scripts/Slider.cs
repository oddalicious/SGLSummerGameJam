using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slider : MonoBehaviour {

	[SerializeField]
	private Image panel;
	private Player player;
	Vector2 panelPosition;
	float panelWidth, popularity, end, start, distance;

	void Start() {
		panelPosition = panel.transform.position;
		panelWidth = panel.transform.localScale.x;
		player = FindObjectOfType<Player>();
		start = (panelPosition.x - panelWidth) + (panelWidth * 0.1f);
		end = (panelPosition.x + panelWidth) - (panelWidth * 0.1f);
		distance = end - start;
	}

	void Update() {
		popularity = player.popularity;
		float xPos = Mathf.Clamp(start + (distance * (popularity / 100)), start, end);
		transform.position = Vector2.Lerp(transform.position, new Vector2(xPos, panelPosition.y), Time.deltaTime * 10);
	}

}
