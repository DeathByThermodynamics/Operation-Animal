using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Turtle;
    GameObject Bubble;
    GameObject Overlay;

    bool scene1 = false;
    bool scene2 = false;
    bool scene3 = false;
    bool scene4 = false;
    bool scene5 = false;

    void Start()
    {
        Turtle = transform.GetChild(0).gameObject;
        Bubble = Turtle.transform.GetChild(0).gameObject;
        Overlay = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (scene5)
            {
                gameObject.transform.position += new Vector3(500, 0, 0);
                scene5 = false;
            }
            if (scene4)
            {
                gameObject.transform.position += new Vector3(500, 0, 0);
                scene4 = false;
            }
            if (scene3)
            {
                scene3 = false;
                StartTutorial4();
            }
            if (scene2)
            {
                scene2 = false;
                StartTutorial3();
            }
            if (scene1)
            {
                scene1 = false;
                StartTutorial2();
            }

            
        }
    }

    IEnumerator TutorialCorout()
    {
        Turtle.transform.localPosition = new Vector3(8.84000015f, -3.72000003f, -15f);
        yield return new WaitForSeconds(2.2f);

        StartTutorial1();
    }

    IEnumerator TutorialCorout2()
    {
        
        Turtle.transform.localPosition = new Vector3(8.84000015f, -3.72000003f, -15f);
        yield return new WaitForSeconds(2f);
        gameObject.transform.position -= new Vector3(500, 0, 0);
        StartTutorial5();
    }

    public void MasterStart()
    {
        StartCoroutine(TutorialCorout());
    }

    public void SecondStart()
    {
        StartCoroutine(TutorialCorout2());
    }
    public void StartTutorial1()
    {

        Overlay.transform.position = new Vector3(0f, 4, -10);
        var paw = Bubble.GetComponent<Animator>();
        Bubble.transform.localPosition = new Vector3(-15, 7, -1f);
        Bubble.transform.localScale = new Vector3(2.8f, 2.8f, 1);
        paw.Play("TUTORIALspeech1");
        scene1 = true;
    }


    public void StartTutorial2()
    {
        var paw = Bubble.GetComponent<Animator>();
        Overlay.transform.localPosition = new Vector3(-6.4000001f, 3.79999995f, -10);
        Bubble.transform.GetChild(0).localPosition = new Vector3(500f, 0f, 0f);
        Bubble.transform.Find("Msg2").localPosition = new Vector3(0.449999988f, 1.38999999f, 0);
        Bubble.transform.localPosition = new Vector3(-15, 7, -1f);
        Bubble.transform.localScale = new Vector3(2.8f, 2.8f, 1);
        paw.Play("TUTORIALspeech1 0");
        scene2 = true;
    }

    public void StartTutorial3()
    {
        var paw = Bubble.GetComponent<Animator>();
        Overlay.transform.localPosition = new Vector3(-6.4000001f, 3.79999995f, 100);
        Bubble.transform.GetChild(1).localPosition = new Vector3(500f, 0f, 0f);
        Bubble.transform.Find("Msg3").localPosition = new Vector3(0.449999988f, 1.38999999f, 0);
        Bubble.transform.localPosition = new Vector3(-15, 7, -1f);
        Bubble.transform.localScale = new Vector3(2.8f, 2.8f, 1);
        paw.Play("TUTORIALspeech1");
        scene3 = true;
    }

    public void StartTutorial4()
    {
        var paw = Bubble.GetComponent<Animator>();
        Overlay.transform.localPosition = new Vector3(-6.4000001f, 3.79999995f, 100);
        Bubble.transform.GetChild(2).localPosition = new Vector3(500f, 0f, 0f);
        Bubble.transform.Find("Msg4").localPosition = new Vector3(0.449999988f, 1.38999999f, -2);
        Bubble.transform.localPosition = new Vector3(-15, 7, -1f);
        Bubble.transform.localScale = new Vector3(2.8f, 2.8f, 1);
        paw.Play("TUTORIALspeech1 0");
        scene4 = true;
    }

    public void StartTutorial5()
    {
        var paw = Bubble.GetComponent<Animator>();
        Overlay.transform.localPosition = new Vector3(-6.4000001f, 3.79999995f, 100);
        Bubble.transform.GetChild(3).localPosition = new Vector3(500f, 0f, 0f);
        Bubble.transform.Find("Msg5").localPosition = new Vector3(0.449999988f, 1.38999999f, -2);
        Bubble.transform.localPosition = new Vector3(-15, 7, -1f);
        Bubble.transform.localScale = new Vector3(2.8f, 2.8f, 1);
        paw.Play("TUTORIALspeech1");
        scene5 = true;
    }
}
