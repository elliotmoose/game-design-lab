using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : Mushroom
{
    // Start is called before the first frame update
    public Texture t;
	public override void ConsumedBy(GameObject player){
		// give player jump boost
		// player.GetComponent<MarioPlayer>().maxSpeed *= 2;
		GameManager.Instance.audioMixer.FindSnapshot("Excited").TransitionTo(1f);
		StartCoroutine(RemoveEffect(player));
	}

	public override void OnCollected() {
		// PowerupManager.Instance.AddPowerup(1, this);
	}

	IEnumerator RemoveEffect(GameObject player){
		yield return new WaitForSeconds(5.0f);
		GameManager.Instance.audioMixer.FindSnapshot("Default").TransitionTo(1f);
		// player.GetComponent<MarioPlayer>().maxSpeed /= 2;
	}
}
