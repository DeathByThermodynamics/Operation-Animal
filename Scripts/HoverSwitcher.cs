using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSwitcher : MonoBehaviour
{
    public GameObject passive;
    public GameObject active;
    public bool CustomDisable = false;
    static bool CanPress = true;
    // Start is called before the first frame update
    void Start()
    {
        passive = transform.parent.GetChild(1).gameObject;
        active = transform.parent.GetChild(0).gameObject;

        active.transform.localPosition = new Vector3(500, 0, active.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        if (HoverSwitcher.CanPress)
        {
            active.transform.localPosition = new Vector3(0, 0, active.transform.localPosition.z);
            passive.transform.localPosition = new Vector3(500, 0, passive.transform.localPosition.z);
        }
        
    }

    void OnMouseDown()
    {
        if (!CustomDisable)
        {
            SolveCanPressFunction(4.0f);
        }
        
    }

    void OnMouseExit()
    {
        active.transform.localPosition = new Vector3(500, 0, active.transform.localPosition.z);
        passive.transform.localPosition = new Vector3(0, 0, passive.transform.localPosition.z);
    }

    IEnumerator SolveCanPress(float num)
    {
        HoverSwitcher.CanPress = false;
        yield return new WaitForSeconds(num);
        HoverSwitcher.CanPress = true;
    }

    void SolveCanPressFunction(float num)
    {
        StartCoroutine(SolveCanPress(num));
    }
}
