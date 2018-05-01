﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class FlamethrowerBehavior : Photon.MonoBehaviour {

	public AudioSource flamethrowerWhoosh;
	public ParticleSystem throwerParticles;
	public int playerID; // Must be public for Photon, no need to provide a value.
	public float lowPitch = 0.45f;
	public float highPitch = 0.85f;
	float startTime;
	bool isActive = false;
	bool isActiveOnNetwork = false;

	void Start() {
		//throwerParticles = transform.gameObject.Find("Flamethrower").gameObject.GetComponent<ParticleSystem> (); // Find thrower particles.
	}

	// Called by DeveloperDefined gesture triggers and networked prefab instantiation:
	public void DoAfterStart() {
		startTime = Time.time; // Keep track of how long this move has been alive.
		isActive = true; // Boolean flag for network effects
	}
		
	void Update() {
		// Check for an active flamethrower:
		if (throwerParticles.isPlaying) {
			// Stop playing after 4 seconds of infinite POWAAAA
			if ((Time.time - startTime > 4)) {
				throwerParticles.Stop ();
				flamethrowerWhoosh.Stop ();
				isActive = false;
			}
		}

		// If this flamethrower should be active, activate it's effects:
		if (isActive && !throwerParticles.isPlaying) {
			throwerParticles.Play(); // Start the particle effects
			flamethrowerWhoosh.Play (); // Sound like a flamethrower
		}
			
		if (!photonView.isMine) {
			isActive = isActiveOnNetwork;
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (isActive);
		} 
		else {
			isActiveOnNetwork = (bool)stream.ReceiveNext ();
		}
	}

	// TODO: Figure out how to damage objects here.

}
