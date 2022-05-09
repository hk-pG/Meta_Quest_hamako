using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColisionAction : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
	}

	void OnColisionEnter(Collision collision) {
		if (collision.gameObject.name != "Plane") {
			transform.position = new Vector3(transform.position.x, transform.position.y + (collision.transform.localScale.y / 2) + transform.localScale.y / 2, transform.position.z);
		}
	}
}
