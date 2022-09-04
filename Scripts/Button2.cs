using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour
{
    public bool CanPress = true;
    public string Function = "";
    LevelEndMenu menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("LevelMaster/LevelEndMenu").GetComponent<LevelEndMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        ManageButtonPress();
    }
    void ManageButtonPress()
    {
        if (CanPress)
        {
            
            switch (Function)
            {
                case "StartTutorial":
                    GameObject.Find("WoodKnock").GetComponent<Audio>().PlayLevelProxyClip();
                    GameObject.Find("TUTBUTTON").GetComponent<LevelController>().DebugLevelStart();
                    break;
                case "FinishLevelPress":
                    Logger.WriteString("Player exits level");
                    GameObject.Find("WoodKnock").GetComponent<Audio>().PlayLevelProxyClip();
                    FinishLevelPress();
                    break;
                case "ClearSelection":
                    Logger.WriteString("Player cleared selection");
                    GameObject.Find("WoodKnock").GetComponent<Audio>().PlayLevelProxyClip();
                    ClearSelection();
                    break;
                case "ToLevelSelection":
                    GameObject.Find("WoodKnock").GetComponent<Audio>().PlayLevelProxyClip();
                    ToLevelSelect();
                    break;
                case "SubmitAnswer":
                    GameObject.Find("WoodKnock").GetComponent<Audio>().PlayLevelProxyClip();
                    ButtonPressed();
                    break;
                case "KillMenu":
                    Logger.WriteString("Player chooses to continue for 3 stars");
                    GameObject.Find("Audio").GetComponent<Audio>().PlayLevelProxyClip();
                    KillMenu();
                    break;
                case "ReturnFromMenu":
                    Logger.WriteString("Player chooses to return to level selection");
                    GameObject.Find("Audio").GetComponent<Audio>().PlayLevelProxyClip();
                    FinishLevelPress();
                    break;
                case "NextLevel":
                    Logger.WriteString("Player chooses to go to next level");
                    GameObject.Find("Audio").GetComponent<Audio>().PlayLevelProxyClip();
                    NextLevel();
                    break;
                case "MainMenu":
                    Logger.WriteString("Player goes back to main menu");
                    GameObject.Find("Audio").GetComponent<Audio>().PlayLevelProxyClip();
                    GoHome();
                    break;
                case "DisableAudio":
                    GameObject.Find("Main Camera/VolumeButtonTwo").transform.localPosition = new Vector3(8f, -4f, 16f);
                    GameObject.Find("Main Camera/VolumeButtonOne").transform.localPosition = new Vector3(358f, -4f, 16f);
                    var saki = GameObject.Find("Main Camera/VolumeButtonOne/ZZZDebugger").GetComponent<HoverSwitcher>();
                    saki.active.transform.localPosition = new Vector3(500, 0, saki.active.transform.localPosition.z);
                    saki.passive.transform.localPosition = new Vector3(0, 0, saki.passive.transform.localPosition.z);
                    DisableAudio();
                    break;
                case "EnableAudio":
                    GameObject.Find("Main Camera/VolumeButtonOne").transform.localPosition = new Vector3(8f, -4f, 16f);
                    GameObject.Find("Main Camera/VolumeButtonTwo").transform.localPosition = new Vector3(358f, -4f, 16f);
                    var ichie = GameObject.Find("Main Camera/VolumeButtonTwo/ZZZDebugger").GetComponent<HoverSwitcher>();
                    ichie.active.transform.localPosition = new Vector3(500, 0, ichie.active.transform.localPosition.z);
                    ichie.passive.transform.localPosition = new Vector3(0, 0, ichie.passive.transform.localPosition.z);
                    EnableAudio();

                    break;
            }
        }
        
    }

    IEnumerator SolveCanPress(float num)
    {
        CanPress = false;
        yield return new WaitForSeconds(num);
        CanPress = true;
    }

    void SolveCanPressFunction(float num)
    {
        StartCoroutine(SolveCanPress(num));
    }

    public void KillMenu()
    {
        SolveCanPressFunction(1f);
        menu.ResetMenu();


    }

    public void DisableAudio()
    {
        GameObject.Find("Main Camera/World1BG").GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("Main Camera/World2BG").GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("Main Camera/World3BG").GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("Main Camera/World4BG").GetComponent<AudioSource>().volume = 0f;
        //AudioListener.volume = 0f;
    }

    public void EnableAudio()
    {
        //AudioListener.volume = 1f;
        GameObject.Find("Main Camera/World1BG").GetComponent<AudioSource>().volume = 0.3f;
        GameObject.Find("Main Camera/World2BG").GetComponent<AudioSource>().volume = 0.3f;
        GameObject.Find("Main Camera/World3BG").GetComponent<AudioSource>().volume = 0.3f;
        GameObject.Find("Main Camera/World4BG").GetComponent<AudioSource>().volume = 0.3f;
    }

    public void GoHome()
    {
        if (CanPress)
        {
            SolveCanPressFunction(3f);
            var temp1 = GameObject.Find("LevelMaster").GetComponent<Butterflies>();
            temp1.PlayButterflyAnimation();
            StartCoroutine(GoHomeAnimation());
        }
    }

    IEnumerator GoHomeAnimation()
    {
        yield return new WaitForSeconds(2f);
        Camera.main.transform.position = new Vector3(0, -15, -16);
    }
    public void NextLevel()
    {
        if (CanPress)
        {
            SolveCanPressFunction(3f);
            var temp1 = GameObject.Find("LevelMaster").GetComponent<Butterflies>();
            temp1.PlayButterflyAnimation();
            StartCoroutine(FinishLevelPress2());
            var temp2 = GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble.GetComponent<LevelController>();
            var next = GameObject.Find(temp2.unlockslevels[0]).GetComponent<LevelController>();

            next.OnMouseDown2Bridge();


        }

    }

    public void ButtonPressed()
    {
        if (CanPress)
        {

            SolveCanPressFunction(2f);
            Master.prepareshunting();
        }
    }

    public void FinishLevelPress()
    {
        if (CanPress)
        {
            SolveCanPressFunction(3f);
            var temp1 = GameObject.Find("LevelMaster").GetComponent<Butterflies>();
            temp1.PlayButterflyAnimation();
            StartCoroutine(FinishLevelPress2());
        }
    }

    IEnumerator FinishLevelPress2()
    {
        yield return new WaitForSeconds(2f);
        menu.ResetMenu();
        Debug.Log("Finished level!");

        var temp2 = GameObject.Find("FinishLevelButton");
        temp2.transform.position = new Vector3(temp2.transform.position.x, temp2.transform.position.y, 5);
        for (var i = 0; i < Master.instantiated.Count; i++)
        {
            Destroy(Master.instantiated[i]);
        }
        for (var i = 0; i < Master.instantiated2.Count; i++)
        {
            Destroy(Master.instantiated2[i]);
        }
        for (var i = 0; i < Master.bubbles.Count; i++)
        {
            Destroy(Master.bubbles[i]);
        }
        for (var i = 0; i < Master.mainobjects.Count; i++)
        {
            Destroy(Master.mainobjects[i]);
        }
        Camera.main.transform.position = new Vector3(Master.CameraXPos, 15, -16);
    }

    public void ToLevelSelect()
    {
        var temp1 = GameObject.Find("LevelMaster").GetComponent<Butterflies>();
        temp1.CloseCurtainsExternal();
        StartCoroutine(toLevelSelectBridge());
    }

    IEnumerator toLevelSelectBridge()
    {
        yield return new WaitForSeconds(1.5f);
        Camera.main.transform.position = new Vector3(Master.CameraXPos, 15, -16);
    }

    public void ClearSelection()
    {
        if (CanPress)
        {
            SolveCanPressFunction(3f);
            for (var i = 0; i < Master.instantiated.Count; i++)
            {
                var InvComp = Master.instantiated[i].GetComponent<InvBox>();
                Master.instantiated[i].transform.position = InvComp.OriginalPosition;
                InvComp.slotted = null;
            }
            for (var i = 0; i < Master.instantiated2.Count; i++)
            {
                var SlotComp = Master.instantiated2[i].GetComponent<BoxSlot>();
                SlotComp.slotted = null;
            }
        }

    }
}
