using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetButton : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
