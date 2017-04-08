using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour {

	/**
	 * The maximum amount of health the player can have.
	 */
	public float maxPlayerHealth = 500;
	/**
	 * The health the player currently has.
	 */
	float playerHealth;
	/**
	 * Cached FlightControl component.
	 * Required to decrease the maneuverability.
	 */
	FlightControl flightControl;
	/**
	 * Cached SerialCommunicator component.
	 * Used to trigger minigames.
	 */
	SerialCommunicator serial;

	/**
	 * Deals damage to the player.
	 * Wrapper to prevent unsanctioned access.
	 */
	public void damage(float health) {
		playerHealth -= health;
		print (playerHealth);
		ScreenShake screenShake = GetComponent<Transform>().Find ("PlayerCamera")
			.GetComponent<ScreenShake> ();
		screenShake.shake = 30;
		screenShake.smoothness = 1;
		screenShake.intensity = .10f;
	}

	/**
	 * Heals the player. Wrapper to prevent unsanctioned access.
	 */
	public void heal(float health){
		playerHealth += health;
		print (playerHealth);
	}

	public float health(){
		return playerHealth;
	}
		
	void Start () {
		playerHealth = maxPlayerHealth;
		flightControl = GetComponent<FlightControl> ();
		serial = GetComponent<SerialCommunicator> ();
	}

	void Update () {
		playerHealth = Mathf.Max(0, Mathf.Min (playerHealth, maxPlayerHealth));
		flightControl.externalMult = Mathf.Max(0, Mathf.Log10 (playerHealth / (maxPlayerHealth / 10)));

		/* Toggles minigames if health is below max; This allows the player to heal. */
		if (playerHealth < maxPlayerHealth && serial.gameOneDone && serial.gameTwoDone) {
			serial.triggerGame ();
		}
	}
}
