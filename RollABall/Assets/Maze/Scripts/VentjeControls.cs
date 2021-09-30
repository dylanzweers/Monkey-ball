using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VentjeControls : MonoBehaviour
{
    public float moveSpeed = 1000f;
    public float rotateSpeed = 20f;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float movementZ = 0f;
    private int rotate;
    private Vector3 rotateDing;
    private Vector3 checkpoint;
    public float jumpSpeed;
    public bool opvloer;

    public BoxCollider boxcollider;


    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkpoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -50)
        {

            toCheckPoint();
        }


        //transform.Rotate(Vector3.up * rotate * rotateSpeed);

        Quaternion deltaRotation = Quaternion.Euler((rotateDing * rotateSpeed) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movePlayer = movementValue.Get<Vector2>();

        movementX = movePlayer.x;
        movementY = movePlayer.y;


    }


    private void toCheckPoint()
    {
        transform.position = checkpoint;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }


    void FixedUpdate()
    {


        Vector3 movement = new Vector3(movementX, rb.velocity.y, movementY);
        rb.AddRelativeForce(movement * moveSpeed);



    }

    void OnRotate(InputValue rotateValue)
    {
        rotate = (int)rotateValue.Get<float>();
        rotateDing[1] = rotate;

        //rb.MoveRotation(rb.MoveRotation * rotate);
    }

    void OnJump(InputValue jumpValue)
    {



        //if (opvloer == true){
        //    rb.AddForce(Vector2.up * jumpSpeed);
        //    opvloer = false;
        //}
        RaycastHit hit;
        if (Physics.Raycast(boxcollider.bounds.center, -transform.up, out hit, 1f))
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.VelocityChange);
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            opvloer = true;
        }
        //if (collision.gameObject.tag == "platform")
        //{
        //    collision.collider.transform.SetParent(transform);
        //}

        if (collision.gameObject.tag == "Enemy")
        {
            toCheckPoint();
        }
        if (collision.gameObject.tag == "Door")
        {
            toCheckPoint();
        }



    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "ground")
        {
            opvloer = false;
        }
        //if (collision.gameObject.tag == "platform")
        //{
        //    collision.collider.transform.SetParent(null);
        //}
    }



}
