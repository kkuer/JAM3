using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public Transform cam;

    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] float rotationSpeed;

    private bool isJumping = false;

    public ParticleSystem walkingParticles;
    public ParticleSystem jumpingParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        float verticalInput = Input.GetAxisRaw("Vertical") * speed;
        float horizontalInput = Input.GetAxisRaw("Horizontal") * speed;

        //cam direction
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        //setting relative cam direction
        Vector3 forwardRelative = verticalInput * camForward.normalized;
        Vector3 rightRelative = horizontalInput * camRight.normalized;

        Vector3 moveDir = forwardRelative + rightRelative;

        //set velocity
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        //move
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            //jump
            isJumping = true;
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);

            //play jumping particles
        }

        if (moveDir!= Vector3.zero)
        {
            //rotate
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            //play walking particles
            if (!walkingParticles.isPlaying)
            {
                walkingParticles.Play();
            }
        }
        else
        {
            walkingParticles.Stop();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isJumping = false;
    }
}
