using UnityEngine;

public class SpillGaffe : Resource {

	public float popularityEffect;

	protected override void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals("Player") && !triggered) {
			player.popularity = Mathf.Clamp(player.popularity + popularityEffect, 0, 100);
			triggered = true;
			player.collisionCount++;
		}
	}


}
