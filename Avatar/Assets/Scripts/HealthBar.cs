using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon;

public class HealthBar : Photon.MonoBehaviour {

	//public GameObject player;
	//public Vector3 greenHealthBar;

	// [SerializeField]  <- needed for private fields
	//public float playerHP;

	void Start() {
		// Get the green portion of the health bar to change it's width:
		//greenHealthBar = 
			//GameObject.Find ("Healthy").GetComponent<RectTransform> ().localScale;
	}

	// TODO: Assign new bars to respawned players

	void Update () {
		//playerHP = player.GetComponent<PlayerBehavior>().health;
		// Calculate the width of the bar based on the current health:
//		greenHealthBar.x = playerHP / 100;
//
//		if (greenHealthBar.x <= 0f) {
//			greenHealthBar.x = 0;
//		}
//
//		// Apply the width adjustment based on the current player health:
//		gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale.x = (playerHP / 100);
	}

	[PunRPC]
	public void UpdateHealthBar(float newHealth) {
		Debug.Log("Updating health bar via RPC for PhotonView " + gameObject.GetPhotonView().viewID);
		// Keep the bar from "displaying" a less-than-zero amount:
		if (newHealth / 100	<= 0f) {
			newHealth = 0;
		}
		Vector3 updateSize = gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale;
		updateSize.x = newHealth / 100;
		gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3 (updateSize.x, updateSize.y, updateSize.z);
	}
//	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
//		if (stream.isWriting) {
//			stream.SendNext (greenHealthBar.x);
//		} else {
//			greenHealthBar.x = (float)stream.ReceiveNext ();
//		}
//	}
	
}
