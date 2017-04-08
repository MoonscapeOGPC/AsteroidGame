using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialLevelEndMarker : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		SceneManager.LoadScene ("IntroTwo");
	}
}
