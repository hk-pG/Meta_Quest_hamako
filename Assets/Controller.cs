using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public float jumpPower = 10.0f;
	public float movePower = 30.0f;
	public float rotateDeg = 0.5f;
	public Rigidbody rb;
	public GameObject vrCamera;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		Jump();
		CursorKeyMove();
		fixDirectionToForward();
		StickMovement();
		if (Input.GetKeyDown(KeyCode.A)) {
			transform.Rotate(new Vector3(0, 90.0f, 0));
		}
	}


	void CursorKeyMove() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * movePower);
	}

	void Jump() {

		if (Input.GetKeyDown(KeyCode.Space) ||
				OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) {
			rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
		}
	}

	void StickMovement() {
		bool isInput = false;
		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.LTouch)) {
			isInput = true;
			if (rb.velocity.magnitude < 10) {
				//指定したスピードから現在の速度を引いて加速力を求める
				float currentSpeed = movePower - rb.velocity.magnitude;
				//調整された加速力で力を加える
				rb.AddForce(rb.transform.right * currentSpeed);
			}
		}

		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch)) {
			isInput = true;
			if (rb.velocity.magnitude < 10) {
				//指定したスピードから現在の速度を引いて加速力を求める
				float currentMovePower = movePower - rb.velocity.magnitude;
				//調整された加速力で力を加える
				rb.AddForce(rb.transform.right * -currentMovePower);
			}
		}

		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch)) {
			isInput = true;
			transform.Rotate(new Vector3(0, rotateDeg, 0));
		}
		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch)) {
			isInput = true;
			transform.Rotate(new Vector3(0, -rotateDeg, 0));
		}

		if (!isInput) {
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
		}
	}

	void fixDirectionToForward() {
		Vector3 movement = vrCamera.transform.forward;
		movement = new Vector3(0, movement.y, 0);
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
			rb.transform.forward = movement;
		}
	}
}
