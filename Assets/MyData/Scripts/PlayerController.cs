using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3.5f;
    public int distanceOfRaycast;

    private float gravity = 10f;
    private RaycastHit _hit;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, distanceOfRaycast)) {
            if (Input.GetButtonDown("Fire1") && _hit.transform.CompareTag("Cube")) {
                _hit.transform.gameObject.GetComponent<Rotate>().changeSpin();
            }
        }
        playerMovement();
    }

    void playerMovement() {
        // [Joystick] / [A] [W] [D] [S]
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction *= speed;
        direction = Camera.main.transform.TransformDirection(direction);
        direction = Vector3.ProjectOnPlane(direction, Vector3.up);

        // [Up] [Down] / [Space] [Ctrl]
        if (Input.GetButton("Up")) direction.y += speed;
        if (Input.GetButton("Down")) direction.y -= speed;

        controller.Move(direction * Time.deltaTime);
    }
}
