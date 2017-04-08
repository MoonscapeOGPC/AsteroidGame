using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	public FlightControl flightController;

	void Update () {
		if (flightController.resource >= 500)
			SceneManager.LoadScene ("MainMenu");
	}
}
