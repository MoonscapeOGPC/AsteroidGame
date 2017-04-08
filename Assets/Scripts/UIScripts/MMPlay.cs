using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MMPlay : MonoBehaviour {
	void OnMouseDown(){
		SceneManager.LoadScene ("IntroOne", LoadSceneMode.Single);
	}
}
