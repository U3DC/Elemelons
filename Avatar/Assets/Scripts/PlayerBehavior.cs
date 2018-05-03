using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayerBehavior : Photon.MonoBehaviour {

	public string flameType;
	public float health;
	public int cameraID;
	public PhotonPlayer thisPlayer;

	// To maintain player status between scenes. May belong higher up, on the Teleporting Rig instead..
	void Awake() {
		//DontDestroyOnLoad (this.gameObject);
	}

    void Start () {
		// Initialize health and healthbar:
		health = 100f;
		//PhotonView.Get (this).RPC ("", PhotonTargets.AllBufferedViaServer, health);
		gameObject.GetComponentInChildren<HealthBar>().player = gameObject;
		//Debug.Log ("FlameChoice is" + flameType);
		cameraID = GameObject.Find("Camera (eye)").GetInstanceID();
		thisPlayer = gameObject.GetComponent<PhotonView> ().owner;
    }

	void Update() {
		// Broken respawn code:
		if (health <= 0f) {
			//Destroy (gameObject.GetComponentInParent<Camera>());
			//GameObject.Find ("NetworkManager").GetComponent<Network> ().OnJoinedRoom ();
			// Delete all the old player stuff
			health = 100f;
		}
	}

	[PunRPC]
	public void TakeDamage(float damage) {
		health -= damage;
		Debug.Log ("TakeDamage RPC recieved for " + damage + " damage.");
		// Needs second RPC to send health bar updates to other players
	}
		
}