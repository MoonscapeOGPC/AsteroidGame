using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
	
	public int shake = 0;
	public float intensity = 0;
	public int smoothness = 0;
	Vector3 oldPosition;
	Vector3 nextPosition;
	bool hasBeenSet = false;
	int smoothCount = 0;

	void Update () {
		if (shake > 0)
		{
			if (!hasBeenSet)
			{
				oldPosition = transform.localPosition;
				hasBeenSet = true;
				smoothCount = 0;
			}
			if (smoothCount == 0)
			{

				nextPosition = (Random.insideUnitSphere * intensity)+ oldPosition-transform.localPosition;
			}
			transform.localPosition += nextPosition / (smoothness + 1);
			shake--;
			if (smoothCount == smoothness)
			{
				smoothCount = -1;
			}
			smoothCount++;

		}
		else
		{
			transform.localPosition = oldPosition;
			hasBeenSet = false;
			shake = 0;
		}
	}
}