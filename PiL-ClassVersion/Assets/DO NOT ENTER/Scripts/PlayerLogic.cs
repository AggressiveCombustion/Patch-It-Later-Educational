using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float jumpForce = 5.0f;
    public bool isGrounded = false;
    public float groundCheckDistance = 1.0f;

    Vector2 moveDirection;

    Animator anim;

    public Transform ashes;

    public GameObject[] pieces;

    bool dontFall = false;

    float elapsed = 0;

    public float airTime = 0.0f;
    public bool didJump = false;
    bool bounced = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        isGrounded = IsGrounded();

        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(h));
        anim.SetFloat("direction", Mathf.Sign(h));

        bool jump = Input.GetButtonDown("Jump");

        float tempY = moveDirection.y;

        if (!bounced)
            moveDirection = new Vector2(h, tempY);
        else
            isGrounded = false;
        
        moveDirection.y -= GameManager.instance.gravity * Time.deltaTime;

        if(!isGrounded)
        {
            airTime += Time.deltaTime;
        }

        if (dontFall)
            moveDirection.y = 0;

        if(isGrounded)
        {
            didJump = false;
            moveDirection.y = 0;
        }

        if(jump && (isGrounded || airTime < 0.2f) && !didJump)
        {
            didJump = true;
            anim.SetBool("jump", true);
            moveDirection.y = jumpForce;

            transform.parent = null;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (moveDirection.y < -5)
        {
            moveDirection.y = -5;
        }

        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        GetComponent<CharacterController>().Move(moveDirection * moveSpeed * Time.deltaTime);

        anim.SetBool("isGrounded", isGrounded);

        if (transform.position.y < -12)
            DieByFall();
    }

    bool IsGrounded()
    {
        CheckForBouncer();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.transform.tag == "Ground")
            {
                if(hit.transform.GetComponent<Dropper>())
                {
                    if (!hit.transform.GetComponent<Dropper>().drop)
                    {
                        hit.transform.GetComponent<Dropper>().anim.SetTrigger("drop");
                        GameManager.instance.AddTimer(0.5f, hit.transform.GetComponent<Dropper>().StartDrop);
                    }
                }

                transform.parent = hit.transform;
                anim.SetBool("jump", false);
                airTime = 0;
                return true;
            }
            else
            {
                transform.parent = null;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                return false;
            }
        }
        else
        {
            transform.parent = null;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            return false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal" && GameObject.FindObjectOfType<KeyManager>().GetComponent<KeyManager>().hasKey)
        {
            dontFall = true;
            // beat level
            //other.transform.GetComponent<Goal>().GoalReached();
            if (other.GetComponent<KeyManager>().hasKey)
            {
                GameManager.instance.CompleteLevel();
            }
        }

        if (other.tag == "Key")
        {
            Debug.Log("Touch Key");
            GameObject om = GameObject.Find("ObjectManager");
            if (om.GetComponent<ObjectManager>().totalPoints != 0)
                return;
            GameObject.FindObjectOfType<KeyManager>().GetComponent<KeyManager>().hasKey = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Fan")
        {
            // died by fan
            DieByFan();
            
        }

        if(other.tag == "TextTrigger")
        {
            other.GetComponent<TriggerText>().DoTheThing();
        }

        /*if(other.tag == "Bouncer")
        {
            Debug.Log("Touch Bouncer");
            moveDirection = moveDirection * -1 * 10;
        }*/
    }

    void CheckForBouncer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.transform.tag == "Bouncer")
            {
                didJump = true;
                anim.SetBool("jump", true);
                moveDirection.y = jumpForce * 1.25f;
                //hit.transform.GetComponent<Animator>().SetTrigger("bounce");
                hit.transform.GetComponent<Bouncer>().DoBounceAnimation();
                GameManager.instance.AddTimer(0.5f, hit.transform.GetComponent<Bouncer>().ResetBouncer);
                Debug.Log("BOUNCE");

                transform.parent = null;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.up, out hit, groundCheckDistance))
        {
            if (hit.transform.tag == "Bouncer")
            {
                didJump = true;
                anim.SetBool("jump", true);
                moveDirection.y = jumpForce * -1.25f;
                //hit.transform.GetComponent<Animator>().SetTrigger("bounce");
                hit.transform.GetComponent<Bouncer>().DoBounceAnimation();
                GameManager.instance.AddTimer(0.5f, hit.transform.GetComponent<Bouncer>().ResetBouncer);
                Debug.Log("BOUNCE");

                transform.parent = null;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, groundCheckDistance))
        {
            if (hit.transform.tag == "Bouncer")
            {
                didJump = true;
                bounced = true;
                anim.SetBool("jump", true);
                moveDirection.x = jumpForce * 1.25f;
                GameManager.instance.AddTimer(0.25f, StopBouncing);
                //hit.transform.GetComponent<Animator>().SetTrigger("bounce");
                hit.transform.GetComponent<Bouncer>().DoBounceAnimation();
                GameManager.instance.AddTimer(0.5f, hit.transform.GetComponent<Bouncer>().ResetBouncer);
                Debug.Log("BOUNCE");

                transform.parent = null;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, groundCheckDistance))
        {
            if (hit.transform.tag == "Bouncer")
            {
                didJump = true;
                bounced = true;
                anim.SetBool("jump", true);
                moveDirection.x = jumpForce * -1.25f;
                GameManager.instance.AddTimer(0.25f, StopBouncing);
                //hit.transform.GetComponent<Animator>().SetTrigger("bounce");
                hit.transform.GetComponent<Bouncer>().DoBounceAnimation();
                GameManager.instance.AddTimer(0.5f, hit.transform.GetComponent<Bouncer>().ResetBouncer);
                Debug.Log("BOUNCE");

                transform.parent = null;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void StopBouncing()
    {
        bounced = false;
        moveDirection.x = 0;
    }

    void DieByFan()
    {
        GameManager.instance.PlaySoundChop();
        SpawnPieces();
        GameManager.instance.ShakeCamera();
        GameObject.Find("PanelFade").GetComponent<PanelFade>().FadeOut();
        GameManager.instance.AddTimer(2.0f, GameManager.instance.RestartLevel);
        Destroy(gameObject);
    }

    public void DieByLaser()
    {
        GameManager.instance.PlaySoundBurn();
        Instantiate(ashes, transform.position, transform.rotation);
        GameManager.instance.ShakeCamera();
        GameObject.Find("PanelFade").GetComponent<PanelFade>().FadeOut();
        GameManager.instance.AddTimer(2.0f, GameManager.instance.RestartLevel);
        Destroy(gameObject);
    }

    public void DieByFall()
    {
        GameManager.instance.PlaySoundCrack();
        GameObject.Find("PanelFade").GetComponent<PanelFade>().FadeOut();
        GameManager.instance.ShakeCamera();
        GameManager.instance.AddTimer(2.0f, GameManager.instance.RestartLevel);
        Destroy(gameObject);
    }

    public void SpawnPieces()
    {
        foreach(GameObject g in pieces)
        {
            GameObject f = Instantiate(g, transform.position, transform.rotation);
            f.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            f.GetComponent<Rigidbody>().velocity = f.transform.up * moveSpeed * 3;
        }
    }
}
