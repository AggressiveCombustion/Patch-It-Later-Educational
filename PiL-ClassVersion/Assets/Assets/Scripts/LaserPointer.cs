using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    public Transform startPoint;
    public Transform reflectLaser;
    LineRenderer lr;
    public float maxDistance = 100.0f;
    public Vector3 endPoint;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();

        if(transform.name.Contains("Reflect"))
        {
            GameManager.instance.AddTimer(0.5f, KillReflectLaser);
        }
        
	}

    void KillReflectLaser()
    {
        if(gameObject != null)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(startPoint.position, -transform.up, out hit, maxDistance))
        {
            endPoint = hit.point;
            if(hit.transform.tag == "Player")
            {
                GameObject player = GameObject.Find("Player");
                if(player != null)
                {
                    player.GetComponent<PlayerLogic>().DieByLaser();
                }
            }
            if (hit.transform.tag == "Fan" && !name.Contains("Reflect"))
            {
                Instantiate(reflectLaser, hit.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
        }
        else
        {
            endPoint = startPoint.position - transform.up * maxDistance;
        }

        lr.SetPosition(0, startPoint.position);
        lr.SetPosition(1, endPoint);
	}
}
