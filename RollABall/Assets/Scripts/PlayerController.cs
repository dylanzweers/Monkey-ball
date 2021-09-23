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
    public int itemsToWin = 10;
    public Camera camera;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float pointerX;
    private float pointerY;
    private float movementZ = 0.0f;
    private bool HasKey = false;
    private int score = 0;
    private bool hasJumped = false;
    Vector3 newPosition;
    Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        newPosition = transform.position;
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLook(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(movementVector);
        if (Physics.Raycast(ray, out hit))
        {
            newPosition = hit.point;
        }
    }

    void OnClick(InputValue movementValue)
    {
        dest = newPosition;
    }

    void OnJump()
    {
        if (hasJumped == false)
        {
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
        rb.MovePosition(Vector3.Lerp(transform.position, dest, speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        hasJumped = false;
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            addOneToScore();
        }
        if (other.gameObject.CompareTag("Destructable"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Door"))
        {
            if (HasKey)
            {
                other.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Key"))
        {
            HasKey = true;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasJumped = true;
    }



    void addOneToScore()
    {
        ++score;
        setScoreText();

        if (score == itemsToWin)
        {
            winTextObject.SetActive(true);
        }
    }


}
