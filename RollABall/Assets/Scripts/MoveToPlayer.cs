using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveToPlayer : MonoBehaviour
{
    public Transform player;
    public float speed;
    private Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RB.MovePosition(Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime));
    }
}
