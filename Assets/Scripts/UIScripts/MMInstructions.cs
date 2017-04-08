using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MMInstructions : MonoBehaviour {


	void OnMouseDown(){
		SceneManager.LoadScene ("InstructionsScene", LoadSceneMode.Single);
	}
}
