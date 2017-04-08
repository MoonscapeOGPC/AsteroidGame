using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public FlightControl flightController;
	public PlayerHealth playerHealth;

	void Setup(){
		flightController = GetComponent<FlightControl> ();
		playerHealth = GetComponent<PlayerHealth> ();
	}
		
	void Update () {
		if(playerHealth.health() <= 0)  
			SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
	}
}
