using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

    public static int levelSelectPage = 0;
    public bool CanPress = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ButtonPressed()
    {
        if (CanPress)
        {

            SolveCanPressFunction(4f);
            Debug.Log("lmao");
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
