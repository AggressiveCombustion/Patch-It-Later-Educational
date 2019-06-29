using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashes : MonoBehaviour {

    Vector2 moveDirection;
    bool isGrounded = false;
    float moveSpeed = 5;
    float groundCheckDistance = .5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = IsGrounded();

        float tempY = moveDirection.y;
        moveDirection = new Vector2(0, tempY);

        moveDirection.y -= GameManager.instance.gravity * Time.deltaTime;

        if (isGrounded)
        {
            moveDirection.y = 0;
        }
        

        if (moveDirection.y < -20)
        {
            moveDirection.y = -20;
        }

        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        GetComponent<CharacterController>().Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.transform.tag == "Ground")
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
