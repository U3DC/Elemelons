using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon;

public class OverheadHealth : Photon.MonoBehaviour {

	public GameObject targetPlayer;
	public Vector3 greenHealthBar;
	public float greenBarWidth;
	public float playerHealth;
	public float headOffset;

	void Start() {
		greenHealthBar = GameObject.Find ("Healthy").GetComponent<RectTransform> ().localScale;
	}

	void Update () {
		if (targetPlayer == null) {
			Destroy (this.gameObject);
			return;
		}
		playerHealth = targetPlayer.GetComponent<PlayerBehavior>().health;
		greenHealthBar.x = playerHealth / 100;

		if (greenHealthBar.x <= 0f) {
			greenHealthBar.x = 0;
		}

//		if (photonView.isMine) {
			GameObject.Find ("Healthy").GetComponent<RectTransform> ().localScale = new Vector3 (greenHealthBar.x, greenHealthBar.y, greenHealthBar.z);
//		}

	}
	
}
