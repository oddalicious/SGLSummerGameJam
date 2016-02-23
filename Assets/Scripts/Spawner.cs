using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Spawner : MonoBehaviour {

    public int upScale = 6;
    public float spawnCD = 3;

    [SerializeField]
    private List<GameObject> spawnableObjects;
    [SerializeField]
    private GameController game;
    private GameObject previous;

    void Start() {
        spawnableObjects = spawnableObjects.OrderBy(n => n.GetComponent<Resource>().spawnRate).ToList();
    }

    IEnumerator SpawnTimer(float _seconds) {
        yield return new WaitForSeconds(_seconds);
        SpawnObject();
    }

    void OnEnable() {
        SpawnObject();
    }

    //void OnTriggerExit2D(Collider2D other) {
    //    if (!game.gamePaused)
    //        SpawnObject();
    //}

    public void SpawnObject() {
        GameObject spawned;
        while (true) {
            foreach (GameObject resource in spawnableObjects) {
                if (Random.Range(0f, 1f) <= resource.GetComponent<Resource>().spawnRate) {
                    spawned = Instantiate(resource, transform.position, transform.rotation) as GameObject;
                    if (previous) {
                        spawned.transform.position = previous.transform.position;
                        spawned.transform.Translate(previous.GetComponent<Renderer>().bounds.size.x, 0, 0);
                    }
                    spawned.gameObject.transform.localScale *= upScale;
                    spawned.GetComponent<Resource>().spawner = this;
                    spawned.GetComponent<Resource>().speed *= 3;
                    previous = spawned;
                    StartCoroutine(SpawnTimer(spawnCD / game.gameSpeed));
                    return;
                }
            }

        }
    }
}
