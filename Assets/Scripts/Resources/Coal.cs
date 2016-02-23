using UnityEngine;
using System.Collections;

public class Coal : Resource {

    private bool coalTriggered = false;
    public Color c;
    protected override void Update() {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (other.tag.Equals("Player") && !coalTriggered) {
            coalTriggered = true;
            game.currentCoalCount--;
        }
    }
}
