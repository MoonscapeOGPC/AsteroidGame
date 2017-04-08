using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InstExit : MonoBehaviour {

	void OnMouseDown(){
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}
