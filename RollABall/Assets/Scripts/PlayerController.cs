using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public int jumpheight = 10;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float movementZ = 0.0f;
    private int score = 0;
    private bool hasJumped = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if(hasJumped == false){
        movementZ = jumpheight;
        hasJumped = true;
        }
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,movementZ,movementY);
         movementZ = 0;
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        hasJumped = false;
        if(other.gameObject.CompareTag("PickUp"))
        {
        other.gameObject.SetActive(false);
        addOneToScore();
        }
    }

    void addOneToScore()
    {
        ++score;
        setScoreText();

        if(score == 10){
            winTextObject.SetActive(true);
        }
    }


}
