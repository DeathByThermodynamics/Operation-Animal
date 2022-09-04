using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvBox : MonoBehaviour
{
    bool following = false;
    bool snappable = false;
    public GameObject slotted;
    public string descriptor;
    public Vector3 OriginalPosition;
    public Vector3 SnappedPosition;
    GameObject SnappedObject;
    Vector3 mouseposition;
    List<GameObject> collided;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = transform.position;
        SnappedPosition = OriginalPosition;
        collided = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            Vector3 mousePos = Input.mousePosition;
            mouseposition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -5));
            gameObject.transform.position = new Vector3(mouseposition.x, mouseposition.y, -5);
        } 
    }

    void OnMouseDown()
    {
        following = true;
    }

    void OnMouseUp()
    {
        following = false;
        if (collided.Count != 0)
        {
            gameObject.transform.position = SnappedPosition;

            var temp = SnappedObject.GetComponent<BoxSlot>();
            if (temp.slotted == null)
            {
                temp.slotted = gameObject;
                slotted = SnappedObject;
                Master.logPrepared();
            } else if (temp.slotted != gameObject)
            {
                GameObject kickedOff = temp.slotted;
                var kickComp = kickedOff.GetComponent<InvBox>();
                temp.slotted = gameObject;
                slotted = SnappedObject;
                kickComp.slotted = null;
                kickedOff.transform.position = kickComp.OriginalPosition;
            }
            

        }
        else
        {
            gameObject.transform.position = OriginalPosition;
        }

        if (slotted == null)
        {
            gameObject.transform.position = OriginalPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("fuiwhvliboi");
        if (col.gameObject.CompareTag("inputbox"))
        {
            collided.Add(col.gameObject);
            SnappedObject = col.gameObject;
            if (SnappedObject != null)
            {
                var temp = SnappedObject.GetComponent<BoxSlot>();
                //if (temp.slotted == null)
                //{
                    SnappedPosition = col.gameObject.transform.position + new Vector3(0, 0, -1);
                //}
            }
           


        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log("fuiwhvliboifwefw");
        if (col.gameObject.CompareTag("inputbox"))
        {
            collided.Remove(col.gameObject);
            var temp = SnappedObject.GetComponent<BoxSlot>();
            if (temp.slotted == gameObject)
            {
                temp.slotted = null;
                slotted = null;
            }

        }
    }
}
