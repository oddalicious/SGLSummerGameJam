using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGame() {
        SceneManager.LoadScene("Main");
    }

    public void LoadWW3End() {
        Time.timeScale = 1;
        SceneManager.LoadScene("BoatPeople");
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits");
    }
}
