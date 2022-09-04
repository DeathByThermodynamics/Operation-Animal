using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightSelection : MonoBehaviour
{
    public bool goesleft;
    public static int totalpages = 1; // totalpages is totalpages + 1
    public static int currentpage = 0;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnMouseDown()
    {
        var movedObject = GameObject.Find("NavigateLevelSelect");
        if (goesleft)
        {
            if (LeftRightSelection.currentpage != 1)
            {
                movedObject.transform.Translate(new Vector3(-20, 0, 0));
            }
            LeftRightSelection.currentpage -= 1;
            Button.levelSelectPage -= 20;
            Camera.main.transform.Translate(new Vector3(-20, 0, 0));
        } else
        {
            if (LeftRightSelection.currentpage != totalpages -1)
            {
                movedObject.transform.Translate(new Vector3(20, 0, 0));
            }
            LeftRightSelection.currentpage += 1;
            Button.levelSelectPage += 20;
            Camera.main.transform.Translate(new Vector3(20, 0, 0));
        }
    } */

    void OnMouseOver()
    {
        var movedObject = GameObject.Find("NavigateLevelSelect");
        if (goesleft)
        {
            if (Camera.main.transform.position.x > 0)
            {
                movedObject.transform.Translate(new Vector3(-20, 0, 0) * Time.deltaTime);
                Camera.main.transform.Translate(new Vector3(-20, 0, 0) * Time.deltaTime);
            }

        }
        else
        {
            if (Camera.main.transform.position.x < 60)
            {
                movedObject.transform.Translate(new Vector3(20, 0, 0) * Time.deltaTime);
                Camera.main.transform.Translate(new Vector3(20, 0, 0) * Time.deltaTime);
            }
        }
    }

    void OnMouseExit()
    {

    }
}
