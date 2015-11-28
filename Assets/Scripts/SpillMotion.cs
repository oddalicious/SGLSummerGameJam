using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SpillMotion : MonoBehaviour {

	[SerializeField]
	Spawner[] spillSpawners;
	GameController game;

	[SerializeField]
	private SpriteRenderer turnbull;
	private Player player;
	public List<Resource> activeResources;
	bool entered = false;
	bool exited = false;
	float opacity = 1f;
	Color color;

	// Use this for initialization
	void Start() {
		game = GetComponent<GameController>();
		player = FindObjectOfType<Player>();
	}

	bool Initialize() {
		if (!entered) {
			FadeTurnbull(true);
			ModifyOpacity();
			if (opacity == 0.0f) {
				entered = true;
				ClearResources();
				opacity = 1;
				game.gameSpeed = 1f;
			}
			return false;
		}
		player.SetState(Player.State.Spill, 0, 0, true);
		ToggleSpawners(true);
		game.gamePaused = false;
		return true;
	}

	void FadeTurnbull(bool input) {
		color = turnbull.color;
		color.a = (input) ? Mathf.Clamp01(color.a + Time.deltaTime) : Mathf.Clamp01(color.a - Time.deltaTime);
		turnbull.color = color;
	}

	bool ReturnToNormal() {
		if (!exited) {
			FadeTurnbull(false);
			game.gamePaused = true;
			ModifyOpacity();
			if (opacity == 0.0f) {
				exited = true;
				ClearResources();
				opacity = 1;
				game.gameSpeed = 1f;
			}
			return false;
		}
		player.SetState(Player.State.Default, 0, 0, false);
		ToggleSpawners(false);
		GetActiveResources(true);
		game.gamePaused = false;
		return true;
	}

	void ModifyOpacity() {
		opacity = Mathf.Clamp01(opacity - Time.deltaTime);
		foreach (Resource r in activeResources) {
			color = r.GetComponent<SpriteRenderer>().color;
			color.a = opacity;
			r.GetComponent<SpriteRenderer>().color = color;
		}
	}

	public void GetActiveResources(bool input) {
		activeResources = FindObjectsOfType<Resource>().Where(n => n.spill == input).ToList();
	}

	public void ClearResources() {
		if (activeResources.Count > 0) {
			foreach (Resource r in activeResources) {
				Destroy(r.gameObject);
			}
			activeResources.Clear();
		}
	}

	void OnEnable() {
		entered = false;
		exited = false;
		if (!player)
			player = FindObjectOfType<Player>();
		if (!game)
			game = FindObjectOfType<GameController>();
		GetActiveResources(false);
		opacity = 1f;
	}

	// Update is called once per frame
	void Update() {
		if (Initialize()) {

			if (player.popularity == 0.0f) {
				Application.LoadLevel("Loss");
			}
			else if (player.popularity > 50) {
				if (activeResources.Count == 0)
					GetActiveResources(true);
				if (ReturnToNormal()) {
					game.Continue();
					enabled = false;
				}
			}
			player.popularity += Time.deltaTime;
		}
	}

	private void ToggleSpawners(bool input) {
		foreach (Spawner spawner in spillSpawners) {
			spawner.gameObject.SetActive(input);
		}
	}

}
