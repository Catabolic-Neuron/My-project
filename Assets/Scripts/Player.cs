using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrustSpeed = 0.5f;
    public float turnSpeed = 0.5f;
    private Rigidbody2D _rigidbody;
    public AudioSource shootingsound;

    void Awake()
    {
        shootingsound = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle thrusting forward
        if (Input.GetKey(KeyCode.W) || (Gamepad.current != null && Gamepad.current.rightTrigger.isPressed))
        {
            _rigidbody.AddForce(transform.up * thrustSpeed);
        }

        // Handle rotating clockwise
        if (Input.GetKey(KeyCode.D) || (Gamepad.current != null && Gamepad.current.leftStick.ReadValue().x > 0))
        {
            _rigidbody.AddTorque(-turnSpeed);
        }

        // Handle rotating counterclockwise
        if (Input.GetKey(KeyCode.A) || (Gamepad.current != null && Gamepad.current.leftStick.ReadValue().x < 0))
        {
            _rigidbody.AddTorque(turnSpeed);
        }

        // Handle shooting with keyboard or controller
        if (Input.GetKeyDown(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.rightShoulder.wasPressedThisFrame))
        {
            shootingsound.Play();
            Shoot();
        }
    }

    private void Shoot()
    {
        // Instantiate bullet and shoot in the direction the ship is facing
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Shoot(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            // Reset ship's velocity and angular velocity
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            // Deactivate the ship
            gameObject.SetActive(false);

            // Notify GameManager of player death
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
