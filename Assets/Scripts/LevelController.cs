using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public void LoadMenu() {
		Application.LoadLevel("Menu");
	}

	public void LoadGame() {
		Application.LoadLevel("Main");
	}
}
