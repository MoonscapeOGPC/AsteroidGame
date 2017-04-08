using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float fuelAmount = 0;
	public float resourceAmount = 0;
	public bool isExplosive = false;
	public float mineAmount = 1;
	public Rigidbody smallAsteroid = null;

	Color color;
	Renderer renderer;

	public const int MINE_STEPS = 60;

	void Start () {
		renderer = GetComponent<Renderer> ();

		color = new Color ();
		color.r = Mathf.Min (1, renderer.material.color.r + (isExplosive ? .1f : 0));
		color.g = Mathf.Min (1, renderer.material.color.g + (resourceAmount > 0 ? .1f : 0));
		color.b = Mathf.Min (1, renderer.material.color.b + (fuelAmount > 0 ? .1f : 0));

		renderer.material.color = color;
	}

	void Update () {
		if (mineAmount <= 0) {
			if (smallAsteroid != null) {
				for(int i = 0; i < 5; i++){
					Vector3 currentPos = new Vector3 (transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2), transform.position.z + Random.Range(-2, 2));
					Vector3 directionTo = currentPos - transform.position;
					directionTo.Normalize ();
					directionTo *= .5f;
					Rigidbody newObjct = Instantiate(smallAsteroid, currentPos,  Quaternion.identity) as Rigidbody;
					newObjct.velocity = directionTo;
				}
			}
			Object.Destroy (gameObject);
		}
	}
}
