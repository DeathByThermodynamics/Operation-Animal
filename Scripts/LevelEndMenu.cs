using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndMenu : MonoBehaviour
{
    List<GameObject> instantiatedStars;

    GameObject overlay;
    GameObject menu1;
    GameObject menu2;
    // Start is called before the first frame update
    void Start()
    {
        instantiatedStars = new List<GameObject>();
        menu1 = GameObject.Find("LevelMaster/LevelEndMenu/lvlcomplete1");
        menu2 = GameObject.Find("LevelMaster/LevelEndMenu/lvlcomplete2");
        overlay = GameObject.Find("MenuOverlay");
        transform.position = new Vector3(250, 0, 0);
        ResetMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetMenu()
    {
        menu1.transform.localPosition = new Vector3(500, 0, 0);
        menu2.transform.localPosition = new Vector3(500, 0, 0);
        
        for (var i = 0; i < instantiatedStars.Count; i++)
        {
            Destroy(instantiatedStars[i]);
        }
        transform.position = new Vector3(-40, 0, 0);
        overlay.transform.position = new Vector3(380, -26, 0);
    }

    public void SetUpMenu(int stars, bool hasNext)
    {
        var star = GameObject.Find("LevelMaster/LevelEndMenu/CompletedStar");
        for (var i = 0; i < stars; i++)
        {
            var t = Instantiate(star, new Vector3(0, 0, -0.25f), Quaternion.identity);
            t.transform.parent = gameObject.transform;
            t.transform.localPosition = new Vector3(-1f + i, 0.66f, -1.2f);
            t.transform.localScale = new Vector3(40, 40,40);
            instantiatedStars.Add(t);
        }

        var continueButton = GameObject.Find("LevelMaster/LevelEndMenu/ForwardButton");
        continueButton.transform.localPosition = new Vector3(500, 0, 0);
        if (hasNext)
        {
            continueButton.transform.localPosition = new Vector3(1.45f, -1.25f, 0);
        }
        Debug.Log(stars);
        if (stars == 3)
        {
            menu2.transform.localPosition = new Vector3(0, 0, 0);
        } else
        {
            menu1.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void EnterMenu(int stars, bool hasNext)
    {
        GameObject.Find("FinishChime").GetComponent<Audio>().PlayLevelProxyClip();
        transform.position = new Vector3(0, -10, -12f);
        SetUpMenu(stars, hasNext);
        overlay.transform.position = new Vector3(0, -26, -11.5f);

        StartCoroutine(MoveIntoPlace());

    }

    IEnumerator MoveIntoPlace()
    {
        var timeElapsed = 0f;

        while (timeElapsed < 0.6f)
        {
            timeElapsed += Time.deltaTime;
            transform.position += new Vector3(0, 10f * Time.deltaTime / 0.6f, 0);
            yield return null;
        }

    }
}
