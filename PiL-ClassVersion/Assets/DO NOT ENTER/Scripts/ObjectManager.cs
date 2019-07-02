using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    [HideInInspector]
    public GameObject[] objects;
    public int objectIndex = 0;

    [HideInInspector]
    public int[] points;

    SpriteRenderer sr;
    [HideInInspector]
    public Sprite[] sprites;

    public int totalPoints = 0;

    public GameObject[] allObjects;
    public Sprite[] allSprites;

    public HUD hud;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        if(allObjects.Length > 0)
            Init();

	}

    public void Init()
    {
        // put all of our objects into arrays, and stack multiples
        Dictionary<GameObject, int> itemRegistry = new Dictionary<GameObject, int>();

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i] != null)
            {
                if (itemRegistry.ContainsKey(allObjects[i]))
                {
                    itemRegistry[allObjects[i]] = (itemRegistry[allObjects[i]]) + 1;
                }

                else
                {
                    itemRegistry.Add(allObjects[i], 1);
                }
            }
        }

        // reinitialize arrays based on how many different items we have
        objects = new GameObject[itemRegistry.Count];
        points = new int[itemRegistry.Count];
        sprites = new Sprite[itemRegistry.Count];

        // go through directory and add each object to our array of objects, by count, in descending order

        int index = 0;
        while(itemRegistry.Count > 0)
        {
            int highest = 0;
            GameObject next = null;

            foreach(KeyValuePair<GameObject, int> kvp in itemRegistry)
            {
                if(kvp.Value > highest)
                {
                    highest = kvp.Value;
                    next = kvp.Key;
                }
            }

            objects[index] = next;
            points[index] = highest;

            itemRegistry.Remove(next);

            index++;
        }

        //Turn on HUD elements

        FillSprites();

        for(int i = 0; i < objects.Length; i++)
        {
            hud.icons[i].gameObject.SetActive(true);
            hud.points[i].gameObject.SetActive(true);
        }
    }

    void FillSprites()
    {
        // copy from allSprites into sprites, based on the order of our objects

        for(int i = 0; i < objects.Length; i++)
        {
            sprites[i] = GetSpriteFromObject(objects[i]);
        }
    }
    
    Sprite GetSpriteFromObject(GameObject go)
    {
        Sprite s = null;
        switch(go.name)
        {
            case "GroundTile":
                s = allSprites[0];
                break;
            case "Fan":
                s = allSprites[1];
                break;
            case "hMover":
                s = allSprites[2];
                break;
            case "LaserPointer":
                s = allSprites[3];
                break;
            case "Poker":
                s = allSprites[4];
                break;
            case "Spinner":
                s = allSprites[5];
                break;
            case "vMover":
                s = allSprites[6];
                break;
            case "Bouncer":
                s = allSprites[7];
                break;
            case "Dropper":
                s = allSprites[8];
                break;
            case "Stopper":
                s = allSprites[9];
                break;
        }
        return s;
    }
	
	// Update is called once per frame
	void Update () {

        if (sprites.Length == 0)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        sr.sprite = sprites[objectIndex];

        transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        bool press = Input.GetMouseButtonDown(0) && points[objectIndex] > 0;

        if (points[objectIndex] > 0)
        {
            sr.color = new Color(255, 255, 255, 0.5f);

        }
        else
        {
            sr.color = new Color(1, 0.5f, 0.5f, 0.5f);
        }

        string obj = objects[objectIndex].name;
        switch (obj)
        {
            case "GroundTile":
                

                if (press)
                {
                    
                    GameObject g = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                    g.transform.position = new Vector3(g.transform.position.x,
                                                       g.transform.position.y,
                                                       0);

                    points[objectIndex] -= 1;

                    GameManager.instance.PlaySoundPlace();
                }

                break;
            case "Bouncer":


                if (press)
                {

                    GameObject g = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                    g.transform.position = new Vector3(g.transform.position.x,
                                                       g.transform.position.y,
                                                       0);

                    points[objectIndex] -= 1;

                    GameManager.instance.PlaySoundPlace();
                }

                break;
            case "Dropper":


                if (press)
                {

                    GameObject g = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                    g.transform.position = new Vector3(g.transform.position.x,
                                                       g.transform.position.y,
                                                       0);

                    points[objectIndex] -= 1;

                    GameManager.instance.PlaySoundPlace();
                }

                break;
            case "Fan":
                RaycastHit h;
                bool addFan = false;
                Transform t = null;
                mousePos.z = 0;

                int blockCount = 0;

                /*Debug.DrawLine(mousePos, mousePos + Vector3.up * 2, Color.red);
                Debug.DrawLine(mousePos, mousePos + Vector3.right * 2, Color.red);
                Debug.DrawLine(mousePos, mousePos - Vector3.up * 2, Color.red);
                Debug.DrawLine(mousePos, mousePos - Vector3.right * 2, Color.red);*/

                if (Physics.Raycast(mousePos, Vector3.up, out h, 2.0f))
                {
                    if (h.transform.name.Contains("GroundTile"))
                    {
                        blockCount += 1;
                    }
                }
                
                if (Physics.Raycast(mousePos, Vector3.down, out h, 2.0f))
                {
                    if (h.transform.name.Contains("GroundTile"))
                    {
                        blockCount += 1;
                    }
                }

                if (Physics.Raycast(mousePos, Vector3.left, out h, 2.0f))
                {
                    if (h.transform.name.Contains("GroundTile"))
                    {
                        blockCount += 1;
                    }
                }

                if (Physics.Raycast(mousePos, Vector3.right, out h, 2.0f))
                {
                    if (h.transform.name.Contains("GroundTile"))
                    {
                        blockCount += 1;
                    }
                }
                Debug.Log(blockCount);
                addFan = blockCount == 2;

                Collider[] cols = (Physics.OverlapSphere(mousePos, 1.0f));
                foreach(Collider c in cols)
                {
                    if (c.name.Contains("Tile") || c.name.Contains("Laser") || c.name.Contains("Fan"))
                        addFan = false;
                }

                if (!addFan)
                    sr.color = new Color(1, 0.5f, 0.5f, 0.5f);

                if (press && addFan)
                {
                    GameObject f = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                    f.transform.position = new Vector3(f.transform.position.x,
                                                       f.transform.position.y,
                                                       0);

                    points[objectIndex] -= 1;
                    GameManager.instance.PlaySoundPlace();
                }
                break;
            case "Poker":
                break;
            case "LaserPointer":
                RaycastHit hit;
                bool canAdd = false;
                Transform target = null;
                mousePos.z = 0;
                // must be under a block
                if (Physics.Raycast(mousePos, Vector3.up, out hit, 1.0f))
                {
                    if (hit.transform.name.Contains("GroundTile"))
                    {
                        // can't put it inside a block
                        if (!Physics.Raycast(mousePos, Vector3.down, 3.0f))
                        {
                            // can't overlap lasers
                            if (hit.transform.GetComponentInChildren<LaserPointer>() == null)
                            {
                                // must be above player
                                if (mousePos.y > GameObject.Find("Player").transform.position.y)
                                {
                                    canAdd = true;
                                    target = hit.transform;
                                }
                            }
                        }
                    }
                }

                if(!canAdd)
                    sr.color = new Color(1, 0.5f, 0.5f, 0.5f);

                if (press && canAdd && target != null)
                {
                    
                    GameObject f = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                    f.transform.parent = target;
                    f.transform.position = new Vector3(target.position.x, 
                                                       target.position.y-2,
                                                       target.position.z);
                    /*f.transform.position = new Vector3(f.transform.position.x,
                                                       f.transform.position.y,
                                                       0);*/


                    points[objectIndex] -= 1;
                    GameManager.instance.PlaySoundPlace();

                }
                break;
            case "Spinner":

                Collider[] potentialTargets = (Physics.OverlapSphere(new Vector3(mousePos.x,
                                                                                 mousePos.y,
                                                                                 0), 0.1f));
                bool found = false;
                foreach (Collider c in potentialTargets)
                {
                    
                    Debug.Log("CHECKING");
                    if (c.transform.name.Contains("GroundTile") ||
                        c.transform.name.Contains("Fan") ||
                        c.transform.name.Contains("LaserPointer"))
                    {
                        found = true;
                        if (press)
                        {
                            GameObject s = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                            s.transform.position = new Vector3(c.transform.position.x,
                                                               c.transform.position.y,
                                                               0);
                            s.GetComponent<Spinner>().target = c.transform;
                            Debug.Log("Approved");

                            points[objectIndex] -= 1;
                            GameManager.instance.PlaySoundPlace();

                        }
                        break;
                    }

                    
                }
                if (!found)
                {
                    sr.color = new Color(1, 0.5f, 0.5f, 0.5f);
                }

                break;
            case "hMover":

                Collider[] potentialTargetsH = (Physics.OverlapSphere(new Vector3(mousePos.x,
                                                                                 mousePos.y,
                                                                                 0), 0.1f));
                bool acceptable = false;
                foreach (Collider c in potentialTargetsH)
                {
                    
                    //Debug.Log("CHECKING");
                    if (c.transform.name.Contains("GroundTile") ||
                        c.transform.name.Contains("Fan") ||
                        c.transform.name.Contains("LaserPointer") ||
                        c.transform.name.Contains("Bouncer") ||
                        c.transform.name.Contains("Dropper"))
                    {
                        acceptable = true;
                        if (press)
                        {
                            GameObject s = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                            s.transform.position = new Vector3(c.transform.position.x,
                                                               c.transform.position.y,
                                                               0);
                            s.GetComponent<Mover>().target = c.transform;
                            s.transform.parent = c.transform;
                            Debug.Log("Approved");

                            points[objectIndex] -= 1;
                            GameManager.instance.PlaySoundPlace();
                        }
                        break;
                    }
                }

                if (!acceptable)
                {
                    sr.color = new Color(1, 0.5f, 0.5f, 0.5f);
                }


                break;
            case "vMover":

                Collider[] potentialTargetsV = (Physics.OverlapSphere(new Vector3(mousePos.x,
                                                                                 mousePos.y,
                                                                                 0), 0.1f));
                bool didFind = false;
                foreach (Collider c in potentialTargetsV)
                {
                    Debug.Log("CHECKING");
                    if (c.transform.name.Contains("GroundTile") ||
                        c.transform.name.Contains("Fan") ||
                        c.transform.name.Contains("LaserPointer") ||
                        c.transform.name.Contains("Bouncer") ||
                        c.transform.name.Contains("Dropper"))
                    {
                        didFind = true;
                        if (press)
                        {
                            GameObject s = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                            s.transform.position = new Vector3(c.transform.position.x,
                                                               c.transform.position.y,
                                                               0);
                            s.GetComponent<Mover>().target = c.transform;
                            s.transform.parent = c.transform;
                            Debug.Log("Approved");

                            points[objectIndex] -= 1;
                            GameManager.instance.PlaySoundPlace();
                        }

                        break;
                    }
                }

                if (!didFind)
                {
                    sr.color = new Color(1, 0.5f, 0.5f, 0.5f);
                }


                break;

            case "Stopper":

                Collider[] potentialTargetsS = (Physics.OverlapSphere(new Vector3(mousePos.x,
                                                                                 mousePos.y,
                                                                                 0), 0.1f));
                bool didFindS = false;
                foreach (Collider c in potentialTargetsS)
                {
                    Debug.Log("CHECKING");
                    if (c.transform.name.Contains("GroundTile") ||
                        c.transform.name.Contains("Fan") ||
                        c.transform.name.Contains("LaserPointer") ||
                        c.transform.name.Contains("Bouncer") ||
                        c.transform.name.Contains("Dropper"))
                    {
                        bool hasComponent = false;

                        if (c.GetComponentInChildren<Mover>() != null)
                            hasComponent = true;

                        if (c.GetComponentInChildren<Spinner>() != null)
                            hasComponent = true;

                        if (!hasComponent)
                            break;

                        didFindS = true;
                        if (press)
                        {
                            GameObject s = Instantiate(objects[objectIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                            s.transform.position = new Vector3(c.transform.position.x,
                                                               c.transform.position.y,
                                                               0);
                            //s.GetComponent<Stopper>().target = c.transform;
                            if (c.GetComponentInChildren<Mover>() != null)
                                c.GetComponentInChildren<Mover>().speed = 0;
                            if (c.GetComponentInChildren<Spinner>() != null)
                                c.GetComponentInChildren<Spinner>().rotSpeed = 0;
                            s.transform.parent = c.transform;
                            Debug.Log("Approved");

                            points[objectIndex] -= 1;
                            GameManager.instance.PlaySoundPlace();
                        }

                        break;
                    }
                }

                if (!didFindS)
                {
                    sr.color = new Color(1, 0.5f, 0.5f, 0.5f);
                }


                break;
        }

            
        

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(objects.Length > 1)
                objectIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (objects.Length > 2)
                objectIndex = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (objects.Length > 3)
                objectIndex = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (objects.Length > 4)
                objectIndex = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (objects.Length > 5)
                objectIndex = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (objects.Length > 6)
                objectIndex = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (objects.Length > 7)
                objectIndex = 7;
        }

        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) >= 0.1f)
        {
            objectIndex -= (int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"));
            if (objectIndex > objects.Length - 1)
                objectIndex = 0;
            if (objectIndex < 0)
                objectIndex = objects.Length - 1;
        }

        totalPoints = 0;
        foreach (int i in points)
            totalPoints += i;

        if(totalPoints == 0)
        {
            GameObject key = GameObject.Find("Key");
            if (key != null)
            {
                Color c = new Color(255, 255, 255, 1f);
                if (key.GetComponent<SpriteRenderer>().color != c)
                    GameManager.instance.PlaySoundSwish();
                key.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                key.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
            }
        }
    }
}
