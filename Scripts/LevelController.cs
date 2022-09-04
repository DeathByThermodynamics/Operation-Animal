using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    //Level Camera: 0, 0, -16
    //Level Selection Camera: 0, 15, -16

    public List<string> inventory;
    public int goal;
    public List<int> sol1;
    public List<int> sol2;
    //public string levelname;
    public List<string> unlockslevels;
    public bool complete = false;
    public bool unlocked = true;
    public Dictionary<int, int> solutions1;
    public int achievedStars = 0;
    public bool solvedBig = false;
    public bool solvedHalf = false;
    GameObject LineVisual;

    public static bool CanPress = true;
    Renderer renderer;
    void Start()
    {
        //levelname = gameObject.name;
        LineVisual = GameObject.Find("LineConnector");
        solutions1 = new Dictionary<int, int>();
        foreach (var item in sol1)
        {
            solutions1.Add(item, 0);
        }
        renderer = GetComponent<Renderer>();

        if (gameObject.name != "TUTBUTTON")
        {
            for (var i = 0; i < unlockslevels.Count; i++)
            {
                /*
                var tempobject = Instantiate(GameObject.Find("LineRenderer"), new Vector3(0, 0, 0), Quaternion.identity);
                var temp = GameObject.Find(unlockslevels[i]).transform.position;
                var sars = tempobject.AddComponent<LineRenderer>();
                sars.material.SetColor("_Color", Color.black);
                sars.startWidth = 0.25f;
                sars.SetPosition(0, transform.position);
                sars.SetPosition(1, temp);
                */
                DrawLine(transform.position, GameObject.Find(unlockslevels[i]).transform.position);

            }
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        var temp3 = GetComponent<Glow>();

        if (!unlocked)
        {
            renderer.material.color = Color.red;


        }
        else if (!complete)
        {
            renderer.material.color = Color.yellow;
        }
        else
        {
            renderer.material.color = Color.blue;
        }

    }

    void OnMouseDown()
    {
        GameObject.Find("Audio").GetComponent<Audio>().PlayLevelProxyClip();
        if (unlocked && LevelController.CanPress)
        {
            StartCoroutine(SolveCanPress());
            var temp1 = GameObject.Find("LevelMaster").GetComponent<Butterflies>();
            temp1.PlayButterflyAnimation();
            StartCoroutine(OnMouseDown2());
        }

    }

    IEnumerator SolveCanPress()
    {
        LevelController.CanPress = false;
        yield return new WaitForSeconds(3f);
        LevelController.CanPress = true;
    }
    IEnumerator OnMouseDown2()
    {

        yield return new WaitForSeconds(2f);
        MasterLevelStart();
    }

    public void DebugLevelStart() // make a StartLevel function later to combine with OnMouseDown
    {
        if (unlocked)
        {
            var sol_combined = new Dictionary<int, int>();
            for (var i = 0; i < sol1.Count; i++)
            {
                sol_combined.Add(sol1[i], sol2[i]);
            }
            var temp = GameObject.Find("LevelMaster").GetComponent<Master>();
            Camera.main.transform.position = new Vector3(0, 0, -16);
            var paw = GameObject.Find("TutorialPieces");
            paw.GetComponent<Tutorial>().MasterStart();
            temp.beginLevel(inventory, sol_combined, goal, gameObject, "0");
            Logger.WriteString("Started tutorial");
        }
    }

    public void OnMouseDown2Bridge()
    {
        
        StartCoroutine(OnMouseDown2());
    }

    public void MasterLevelStart()
    {

        var sol_combined = new Dictionary<int, int>();
        for (var i = 0; i < sol1.Count; i++)
        {
            sol_combined.Add(sol1[i], sol2[i]);
        }
        var temp = GameObject.Find("LevelMaster").GetComponent<Master>();
        Master.CameraXPos = Camera.main.transform.position.x;
        Camera.main.transform.position = new Vector3(0, 0, -16);
        var worldnum1 = gameObject.name.ToCharArray();
        var worldnum = worldnum1[0].ToString();
        Logger.WriteString("Started level " + gameObject.name);
        temp.beginLevel(inventory, sol_combined, goal, gameObject, worldnum);
    }

    



    void DrawLine(Vector3 pos1, Vector3 pos2)
    {
        var worldpos = pos1;
        var secondpos = worldpos - pos2;
        float angle;
        var centerpoint = worldpos - new Vector3(secondpos.x * 0.5f, secondpos.y * 0.5f, secondpos.z * 0.5f);
        if (secondpos.x < 0 && secondpos.y < 0)
        {
            angle = Vector3.Angle(new Vector3(-1.0f * secondpos.x, -1.0f * secondpos.y, 0.0f), new Vector3(1.0f, 0.0f, 0.0f));
        }
        else if (secondpos.x < 0 && secondpos.y > 0)
        {
            angle = Vector3.Angle(new Vector3(1.0f * secondpos.x, -1.0f * secondpos.y, 0.0f), new Vector3(1.0f, 0.0f, 0.0f));
        }
        else if (secondpos.x > 0 && secondpos.y < 0)
        {
            angle = Vector3.Angle(new Vector3(-1.0f * secondpos.x, 1.0f * secondpos.y, 0.0f), new Vector3(1.0f, 0.0f, 0.0f));
        }
        else
        {
            angle = Vector3.Angle(new Vector3(secondpos.x, secondpos.y, 0.0f), new Vector3(1.0f, 0.0f, 0.0f));
        }

        //Debug.Log(angle);
        var distance = secondpos.magnitude;
        GameObject newline = Instantiate(LineVisual, centerpoint, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle * 1.0f)));
        newline.transform.parent = gameObject.transform;
        newline.transform.localPosition += new Vector3(0, 0, 0.5f);
        var originalscale = newline.transform.localScale;
        newline.transform.localScale = new Vector3(distance, originalscale.y, originalscale.z * 2.0f);

    }
}
