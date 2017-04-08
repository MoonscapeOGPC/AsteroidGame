using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public float maxWidth;
	public float maxHeight;
	public float posX;
	public float posY;
	public Texture2D healthbarTexture;
	PlayerHealth playerHealth;
	FlightControl flightControl;

	void OnGUI(){
		GUI.DrawTexture (new Rect (posX, posY, maxWidth * playerHealth.health () / playerHealth.maxPlayerHealth, maxHeight), healthbarTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (posX, posY + maxHeight * 2, maxWidth * flightControl.fuel / flightControl.maxFuel, maxHeight), healthbarTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (posX, posY + maxHeight * 4, maxWidth * flightControl.resource / flightControl.maxResource, maxHeight), healthbarTexture, ScaleMode.StretchToFill);
	}

	void Start () {
		flightControl = GetComponent <FlightControl> ();
		playerHealth = GetComponent<PlayerHealth>();
	}

	void Update () {
	
	}
}
