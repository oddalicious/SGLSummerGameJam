using UnityEngine;
using System.Collections;

public class Coal : Resource {

	private bool coalTriggered = false;
	public Color c;
	protected override void Update() {
		base.Update();
		ColorParticleSystem();
	}

	protected override void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D(other);
		if (other.tag.Equals("Player") && !coalTriggered) {
			coalTriggered = true;
			game.currentCoalCount--;
		}
	}

	void ColorParticleSystem() { // NOT FUCKING WORKING
								 //float tempPercent = (float)game.currentCoalCount / (float)game.totalCoalCount;
								 //c = new Color((1f - tempPercent) * 255f, tempPercent * 255f, 0f);
								 //Debug.Log(c);
								 //pSystem.startColor = c;
	}
}
