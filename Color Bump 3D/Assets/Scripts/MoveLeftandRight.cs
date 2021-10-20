using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftandRight : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private bool right;
    [SerializeField] private bool dontMove;

    private bool stop;
    private float minX;
    private float maxX;

    void Start()
    {
        maxX = transform.position.x + distance;
        minX = transform.position.x - distance;
    }

    void Update()
    {
        if(!stop && !dontMove)
        {
            if (right)
            {
                transform.position += Vector3.right * speed ;
                if(transform.position.x >= maxX)
                {
                    right = false;
                }
            }
            else
            {
                transform.position += Vector3.left * speed ;
                if (transform.position.x <= minX)
                {
                    right = true;
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "White" && target.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            stop = true;
            GetComponent<Rigidbody>().freezeRotation = false;
        }
        
    }


}
