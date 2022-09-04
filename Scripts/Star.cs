using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Dictionary<string, Vector3> positionsDict;
    // Start is called before the first frame update
    void Start()
    {
        positionsDict = new Dictionary<string, Vector3>();
        positionsDict.Add("1", new Vector3(15.8179998f, 4.02299976f, -1f));
        positionsDict.Add("2", new Vector3(16.8309994f, 4.02299976f, -1f));
        positionsDict.Add("3", new Vector3(17.8439999f, 4.02299976f, -1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStar(string num) // num between 1-3 PLS
    {
        var star = GameObject.Find("LevelMaster/Stars/star" + num);
        var temp1 = GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble;
        var levelstar = GameObject.Find("LevelSelectStar");

        var starAnimator = star.GetComponent<Animator>();
        GameObject.Find("BrightChime").GetComponent<Audio>().PlayLevelProxyClip();
        starAnimator.Play("star" + num);
        star.transform.position = positionsDict[num];
        star.transform.localScale = new Vector3(50, 50, 100);

        switch (num)
        {
            case "1":
                var tempstar1 = Instantiate(levelstar, temp1.transform.position, Quaternion.identity, temp1.transform);
                tempstar1.transform.position += new Vector3(-0.5f, -0.8f, -0.5f);
                break;
            case "2":
                var tempstar2 = Instantiate(levelstar, temp1.transform.position, Quaternion.identity, temp1.transform);
                tempstar2.transform.position += new Vector3(0, -0.8f, -0.5f);
                break;
            case "3":
                var tempstar3 = Instantiate(levelstar, temp1.transform.position, Quaternion.identity, temp1.transform);
                tempstar3.transform.position += new Vector3(0.5f, -0.8f, -0.5f);
                break;
        }
    }

    public void TeleportStar(string num)
    {
        var star = GameObject.Find("LevelMaster/Stars/star" + num.ToString());
        var starAnimator = star.GetComponent<Animator>();
        starAnimator.Play("tper" + num.ToString());
        starAnimator.Play("tperr" + num.ToString());
    }

    public void ResetStars()
    {
        for (var i = 1; i < 4; i++)
        {
            var star = GameObject.Find("LevelMaster/Stars/star" + i.ToString());
            var starAnimator = star.GetComponent<Animator>();
            starAnimator.Play("Ghost");
        }
        
    }
}
