using UnityEngine;
using System.Collections;

public class CollisionTextDisplay : MonoBehaviour {

	public string displayText;
	bool isColliding;

	void Start () {
		isColliding = false;
		displayText = displayText.Replace ("\\n", "\n");
	}

	void OnGUI(){
		if(isColliding) GUI.Label(new Rect(20, 100, Screen.width, Screen.height) , displayText); 
	}
	
	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.GetComponent<FlightControl>() != null)
			isColliding = true;
	}

	void OnTriggerExit(Collider collision){
		if(collision.gameObject.GetComponent<FlightControl>() != null)
			isColliding = false;
	}

}
