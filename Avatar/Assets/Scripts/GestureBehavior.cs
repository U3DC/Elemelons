using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class GestureBehavior : Photon.MonoBehaviour {

	GameObject player;
	Rigidbody rb;
    public int playerID;
	AudioSource flameWoosh;
    //public Color attackerColor;
	float startTime;
	float currentTime;

	public bool attack = false;
	public bool defense = false;
	public bool isLive = false;

	void Start() {
		//GetComponent<Renderer>().material.color = attackerColor;
		rb = gameObject.GetComponent<Rigidbody>();
		startTime = Time.time;
		isLive = true;

	    // Old code for colorizing, kept here for RPC example:
//        Vector3 serializedColor;
//        serializedColor.x = attackerColor.r;
//        serializedColor.y = attackerColor.g;
//        serializedColor.z = attackerColor.b;
//        // Run a Remote Procedure Call for only currently connected users, sending the values to SetColor:
//        PhotonView.Get(this).RPC("SetColor", PhotonTargets.All, serializedColor);
    }

//    [PunRPC] // As a photon serialized view, only send floats/ints/vectors/quaternions.
//    public void SetColor(Vector3 color)
//    {
//        // Sets this Player to the color sent with the call:
//        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);
//        attackerColor = GetComponent<Renderer>().material.color;
//        if (PhotonView.Get(this).isMine)
//        { // If this user called the function, send this call to the other users, including those who join:
//            photonView.RPC("SetColor", PhotonTargets.OthersBuffered, color);
//        }
//    }

    void Update() {
		currentTime = Time.time;

		if (attack) {
			if (currentTime - startTime > 3f) {
				isLive = false;
				Destroy (gameObject);
			}
		}

		if (defense) {
			if (currentTime - startTime > 2f) {
				isLive = false;
				Destroy (gameObject);
			}
		}
	}

	// For attack:
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Fireball destroyed itself on " + collision.gameObject.tag);
		isLive = false;
		Destroy(gameObject); // Destroy the attack on any collision.
	}

	// For defense:
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("attack")) // Check for attacking objects
		{
			// Destroy any attacks that are not this player's:
			if (collision.gameObject.GetComponent<GestureBehavior> ().playerID != playerID) {
				Destroy (collision.gameObject);
				flameWoosh.Play ();
			}
		}
	}

	public void DoAfterStart(Vector3 direction) {
		// Orient the new object:
		Quaternion rotationForTrails = Quaternion.FromToRotation (Vector3.back, direction);
		transform.rotation = rotationForTrails;

		// Send attacks flying!
		if (attack) {
			rb = gameObject.GetComponent<Rigidbody> ();
			rb.AddForce (direction * 1250f);
		}
	}

	public void OnPhotonSerializedView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (isLive);
		} else {
			this.isLive = (bool)stream.ReceiveNext ();
		}
	}

}
