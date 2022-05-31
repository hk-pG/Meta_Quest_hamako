using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Controller : MonoBehaviour {
	public float jumpPower = 10.0f;
	public float movePower = 1000.0f;
	public float rotateDeg = 0.5f;

	// オブジェクトを受け取るメンバ達
	public Rigidbody rb;
	public GameObject vrCamera;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		Jump();
		KeyController();
		VRCController();
	}

	void KeyController() {
		CursorKeyMove();
		FixDirectionToForwardByKey();
	}

	void CursorKeyMove() {
		// 入力から移動方向を取得、3次元ベクトルに変換
		Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		// 方向ベクトルにスピードを掛けて、実際に移動するベクトルを作成
		Vector3 movement = transform.TransformDirection(moveDir) * movePower;

		rb.AddForce(movement);
	}

	void Jump() {

		if (Input.GetKeyDown(KeyCode.Space) ||
				OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) {
			rb.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
			Debug.Log("\nジャンプしたお\n");
		}
	}

	void VRCController() {
		VRStickMove();
		FixDirectionToForwardByTrigger();
	}

	void VRStickMove() {
		bool isInput = false;
		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.LTouch)) {
			isInput = true;
			if (rb.velocity.magnitude < 10) {
				//指定したスピードから現在の速度を引いて加速力を求める
				float currentSpeed = movePower - rb.velocity.magnitude;
				//調整された加速力で力を加える
				rb.AddForce(transform.forward * currentSpeed);
			}
		}

		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch)) {
			isInput = true;
			if (rb.velocity.magnitude < 10) {
				//指定したスピードから現在の速度を引いて加速力を求める
				float currentMovePower = movePower - rb.velocity.magnitude;
				//調整された加速力で力を加える
				rb.AddForce(transform.forward * -currentMovePower);
			}
		}

		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch)) {
			isInput = true;
			if (rb.velocity.magnitude < 10) {
				//指定したスピードから現在の速度を引いて加速力を求める
				float currentMovePower = movePower - rb.velocity.magnitude;
				//調整された加速力で力を加える
				rb.AddForce(transform.right * currentMovePower);
			}
		}
		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch)) {
			isInput = true;
			if (rb.velocity.magnitude < 10) {
				//指定したスピードから現在の速度を引いて加速力を求める
				float currentMovePower = movePower - rb.velocity.magnitude;
				//調整された加速力で力を加える
				rb.AddForce(transform.right * -currentMovePower);
			}
		}

		if (!isInput) {
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
		}
	}

	void FixDirectionToForwardByTrigger() {
		Vector3 movement = vrCamera.transform.forward;
		movement = new Vector3(0, movement.y, 0);
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
			rb.transform.forward = movement;
		}
	}

	void RotatePlayerByCamera() {
	}

	void FixDirectionToForwardByKey() {
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			transform.Rotate(new Vector3(0, -90, 0));
		}


		if (Input.GetKeyDown(KeyCode.RightShift)) {
			transform.Rotate(new Vector3(0, 90, 0));
		}
	}
}
