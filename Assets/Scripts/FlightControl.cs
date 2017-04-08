using UnityEngine;
using System.Collections;

public class FlightControl : MonoBehaviour {

	/**
	 * Cached RigidBody. We COULD request
	 * it every step with getcomponent, but
	 * this doesn't change, so why not just get it
	 * once?
	 */
	Rigidbody rigidBody;
	/**
	 * Cahced health component.
	 * See above; we don't want to call getcomponent
	 * every time.
	 */
	PlayerHealth health;
	/**
	 * Serial communicator used to trigger
	 * mini games.
	 */
	SerialCommunicator serial;
	/**
	 * Component used to shake the camera screen.
	 */
	ScreenShake screenShake;
	/**
	 * The multiplier by which the input should be
	 * ... multiplied ... before a force is applied
	 * to the player.
	 */
	const float ACCL_GENERAL_MULT = 5.0f;
	/**
	 * The resulting multiplier to apply
	 * during forward acceleration.
	 */
	const float ACCL_FORWARD_MULT = 1.0f * ACCL_GENERAL_MULT;
	/**
	 * The resulting multiplier to apply
	 * during backward acceleration.
	 */
	const float ACCL_BACKWARD_MULT = .75f * ACCL_GENERAL_MULT;
	/**
	 * The drag to apply when stabilizing the ship.
	 */
	const float STABILIZE_MULT = 2f;
	const float FUEL_CONSUMPTION = 10f;
	/**
	 * Used by external components to modify the speed and stuff of the ship.
	 */
	public float externalMult = 1;
	public float maxFuel = 500;
	public float maxResource = 500;
	public float fuel;
	public float resource;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		health = GetComponent<PlayerHealth> ();
		serial = GetComponent<SerialCommunicator> ();
		screenShake = GetComponent<Transform>().Find ("PlayerCamera").GetComponent<ScreenShake> ();
		fuel = maxFuel / 2;
		resource = 0;
	}

	void Update () {
		/**
		 * TODO: Figure out if these can be better configured.
		 * Right now, "right" or "forward" is determine experimentally.
		 */
		/* Accelerates the ship in the direction it's facing. */
		if (fuel > 0) {
			int consuming = 0;
			if (Input.GetButton ("Accelerate")) {
				float inputAxis = Input.GetAxis ("Accelerate");
				inputAxis = (inputAxis > 0) ? (inputAxis * ACCL_FORWARD_MULT) : (inputAxis * ACCL_BACKWARD_MULT);
				rigidBody.AddForce (transform.up * inputAxis * externalMult);
				consuming++;
			}
			/* Rotates the ship on its axis (it still faces the same way) */
			if (Input.GetButton ("Rotate")) {
				rigidBody.AddTorque (transform.up * Input.GetAxis ("Rotate") * -1 * externalMult);
				consuming++;
			} 
			/* Rotates the ship horizontally, turning it to the left or to the right. */
			if (Input.GetButton ("Horizontal")) {
				rigidBody.AddTorque (transform.forward * Input.GetAxis ("Horizontal") * -1 * externalMult);
				consuming++;
			}
			/* Rotates the ship vertically, turning it down or up relative to where it's looking. */
			if (Input.GetButton ("Vertical")) {
				rigidBody.AddTorque (transform.right * Input.GetAxis ("Vertical") * externalMult);
				consuming++;
			}
			/* Kills the angular rotation of the ship. Prevents over-spinny situations. */
			if (Input.GetButton ("Stabilize")) {
				rigidBody.angularDrag = STABILIZE_MULT;
				consuming++;
			} else {
				rigidBody.angularDrag = 0;
			}
			/* Different kind of movement; Moves the ship up, down, left or right relative to its current
		 * bearing. */
			if (Input.GetButton ("TranslateHorizontal" )) {
				rigidBody.AddForce (transform.right * ACCL_GENERAL_MULT * Input.GetAxis("TranslateHorizontal") * externalMult);
				consuming++;
			}
			if (Input.GetButton ("TranslateVertical")) {
				rigidBody.AddForce (transform.forward * -1 * ACCL_GENERAL_MULT * Input.GetAxis("TranslateVertical") * externalMult);
				consuming++;
			}
			if (Input.GetButton ("KillVelocity")) {
				rigidBody.drag = STABILIZE_MULT;
				consuming++;
			} else {
				rigidBody.drag = 0;
			}

			fuel -= consuming * Time.deltaTime * FUEL_CONSUMPTION;
		}

		if (Input.GetMouseButton (0)) {
			RaycastHit hitObject;
			if(Physics.Raycast(transform.position, transform.up, out hitObject, 5)){
				Asteroid asteroidComponent;

				screenShake.intensity = .1f;
				screenShake.shake = 1;
				screenShake.smoothness = 1;

				if ((asteroidComponent = hitObject.collider.GetComponent<Asteroid> ()) != null) {
					if (asteroidComponent.isExplosive) {
						health.damage (Random.Range (50, 100));
						Object.Destroy (asteroidComponent.gameObject);
					} else {
						resource += asteroidComponent.resourceAmount / Asteroid.MINE_STEPS;
						fuel += asteroidComponent.fuelAmount / Asteroid.MINE_STEPS;
						print(asteroidComponent.fuelAmount / Asteroid.MINE_STEPS);
						asteroidComponent.mineAmount -= 1f / Asteroid.MINE_STEPS;
					}
				}
			}
		}

		fuel = Mathf.Max (0, Mathf.Min (fuel, maxFuel));

		/**
		 * TODO: Convert these to InputManager inputs.
		 */

	}

	void OnCollisionEnter(Collision collisionInfo){
		serial.triggerGame ();
		health.damage (collisionInfo.impulse.magnitude * 50);
	}
}
