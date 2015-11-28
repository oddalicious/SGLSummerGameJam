using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class SpillMotion : MonoBehaviour {

	public List<Resource> activeResources;

	[SerializeField]
	private Spawner[] spillSpawners;
	[SerializeField]
	private SpriteRenderer turnbull;

	private GameController game;
	private Player player;
	private bool entered = false;
	private bool exited = false;
	private float opacity = 1f;
	private Color color;

	// Use this for initialization
	void Start() {
		game = GetComponent<GameController>();
		player = FindObjectOfType<Player>();
	}

	void OnEnable() {
		if (!player)
			player = FindObjectOfType<Player>();
		if (!game)
			game = FindObjectOfType<GameController>();
		GetActiveResources(false);

	}

	void OnDisable() {
		entered = false;
		exited = false;
		opacity = 1f;
		game.gameSpeed = 1f;
	}

	void Update() {
		if (StartSpill()) {

			if (player.popularity == 0.0f) {
				Application.LoadLevel("Loss");
			}
			else if (player.popularity > 50) {
				if (activeResources.Count == 0)
					GetActiveResources(true);
				if (EndSpill()) {
					game.Continue();
					enabled = false;
				}
			}
			player.popularity += Time.deltaTime;
		}
	}

	bool StartSpill() {
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

	bool EndSpill() {
		if (!exited) {
			FadeTurnbull(false);
			game.gamePaused = true;
			ModifyOpacity();
			if (opacity == 0.0f) {
				exited = true;
				ClearResources();
				opacity = 1;
			}
			return false;
		}
		player.SetState(Player.State.Default, 0, 0, false);
		ToggleSpawners(false);
		GetActiveResources(true);
		game.gamePaused = false;
		return true;
	}

	void FadeTurnbull(bool input) {
		color = turnbull.color;
		color.a = (input) ? Mathf.Clamp01(color.a + Time.deltaTime) : Mathf.Clamp01(color.a - Time.deltaTime);
		turnbull.color = color;
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

	private void ToggleSpawners(bool input) {
		foreach (Spawner spawner in spillSpawners) {
			spawner.gameObject.SetActive(input);
		}
	}

	private void ModifyOpacity() {
		opacity = Mathf.Clamp01(opacity - Time.deltaTime);
		foreach (Resource r in activeResources) {
			color = r.GetComponent<SpriteRenderer>().color;
			color.a = opacity;
			r.GetComponent<SpriteRenderer>().color = color;
		}
	}
}