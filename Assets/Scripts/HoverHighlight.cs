using UnityEngine;
using System.Collections;

public class HoverHighlight : MonoBehaviour {

	public Color onEnter;
	public Color onExit;
	SpriteRenderer renderer;

	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		renderer.color = onExit;
	}

	void Update () {
		
	}

	void OnMouseEnter(){
		renderer.color = onEnter;
	}

	void OnMouseExit(){
		renderer.color = onExit;
	}
}
