using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Master : MonoBehaviour
{
    public static bool showinv = false;
    Transform _transform;
    public static List<string> inventory;
    public static List<string> prepared;
    public static List<GameObject> prepared_gameobject;
    public static List<List<string>> postedsolutions;
    GameObject itembox;
    GameObject itemslot;
    GameObject outputslot;
    GameObject targetslot;
    GameObject bubble;
    public static List<GameObject> instantiated;
    public static List<GameObject> instantiated2;
    public static List<GameObject> mainobjects;
    public static List<GameObject> bubbles;
    public static Dictionary<int, int> solutions;
    public static Dictionary<int, int> solutions1;
    public static int goal;
    public static float CameraXPos = 0f;


    public static int totalStars = 0;

    public List<string> numbers;

    public GameObject levelbubble;
    int numOfSolutions;
    // Start is called before the first frame update
    void Start()
    {
        /*
        var placeholderinv = new List<string>();
        placeholderinv.Add("2");
        placeholderinv.Add("3");
        placeholderinv.Add("4");
        placeholderinv.Add("6");
        placeholderinv.Add("+");
        placeholderinv.Add("-");
        placeholderinv.Add("×");

        var placeholdersolutions = new Dictionary<int, int>(); // first is length, second is # of solutions
        placeholdersolutions.Add(3, 1);
        placeholdersolutions.Add(5, 4);
        placeholdersolutions.Add(7, 1);
        beginLevel(placeholderinv, placeholdersolutions);
        */
        numbers.Add("1");
        numbers.Add("2");
        numbers.Add("3");
        numbers.Add("4");
        numbers.Add("5");
        numbers.Add("6");
        numbers.Add("7");
        numbers.Add("8");
        numbers.Add("9");
        numbers.Add("0");
        numbers.Add("+");
        numbers.Add("-");
        numbers.Add("×");
        numbers.Add("(");
        numbers.Add(")");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beginLevel(List<string> level_inv, Dictionary<int, int> level_sols, int targetvalue, GameObject selectedbubble, string worldnum)
    {
        numOfSolutions = 0;
        instantiated = new List<GameObject>();
        instantiated2 = new List<GameObject>();
        bubbles = new List<GameObject>();
        mainobjects = new List<GameObject>();
        postedsolutions = new List<List<string>>();
        itembox = GameObject.Find("InvBox");
        itemslot = GameObject.Find("Boxslot");
        outputslot = GameObject.Find("OutputSlot");
        targetslot = GameObject.Find("TargetSlot");
        bubble = GameObject.Find("GoalBubble");
        _transform = gameObject.transform;
        inventory = level_inv;

        solutions = level_sols;
        solutions1 = selectedbubble.GetComponent<LevelController>().solutions1; // first is length, second is # of solutions
        var weirdstring = "";
        foreach (var item in solutions.Keys)
        {
            weirdstring = weirdstring + item.ToString() + ": \n";
            numOfSolutions += solutions[item];
        }
        //var showsols = GameObject.Find("GoalCanvas/Text").GetComponent<Text>();
        //showsols.text = weirdstring;



        levelbubble = selectedbubble;
        goal = targetvalue;

        var showtargetvalue = GameObject.Find("TargetSlot/Canvas/Results").GetComponent<Text>();
        showtargetvalue.text = goal.ToString();

        var temp = GameObject.Find("FinishLevelButton");
        temp.GetComponent<Canvas>().enabled = false;


        var starComp = GameObject.Find("LevelMaster").GetComponent<Star>();
        starComp.ResetStars();
        for (var i = 1; i < levelbubble.GetComponent<LevelController>().achievedStars + 1; i++)
        {
            starComp.TeleportStar(i.ToString());
        }
        /*
        if (levelbubble.GetComponent<LevelController>().achievedStars >= 1)
        {
            finishLevel(levelbubble.GetComponent<LevelController>().achievedStars);
        } */

        manageBG(worldnum);
        showSpots(worldnum);
        showBubbles();
        updateBubbles();
        showInv();
    }

    public void manageBG(string worldnum)
    {
        var bg1 = GameObject.Find("Backgrounds/LevelBG/World1LevelBG");
        var bg2 = GameObject.Find("Backgrounds/LevelBG/World2LevelBG");
        var bg3 = GameObject.Find("Backgrounds/LevelBG/World3LevelBG");
        var bg4 = GameObject.Find("Backgrounds/LevelBG/World4LevelBG");

        bg1.transform.localPosition = new Vector3(500, 500, 500);
        bg2.transform.localPosition = new Vector3(500, 500, 500);
        //bg3.transform.localPosition = new Vector3(500, 500, 500);
        //bg4.transform.localPosition = new Vector3(500, 500, 500);


        var one = GameObject.Find("Main Camera/World1BG").GetComponent<Audio>();
        var two = GameObject.Find("Main Camera/World2BG").GetComponent<Audio>();
        var three = GameObject.Find("Main Camera/World3BG").GetComponent<Audio>();
        var four = GameObject.Find("Main Camera/World4BG").GetComponent<Audio>();
        one.PauseLevelProxyClip();
        two.PauseLevelProxyClip(); 
        three.PauseLevelProxyClip();
        four.PauseLevelProxyClip();
        switch (worldnum)
        {
            case "0":
                bg1.transform.localPosition = new Vector3(-2.25999999f, -8.25f, 57.9944572f);
                one.PlayLevelProxyClip();
                break;
            case "1":
                bg2.transform.localPosition = new Vector3(-2f, -9.13000011f, 57.9944572f);
                two.PlayLevelProxyClip();
                break;
            case "2":
                bg3.transform.localPosition = new Vector3(-2.71000004f, -8.51000023f, 57.9944572f);
                three.PlayLevelProxyClip();
                break;
            case "3":
                bg4.transform.localPosition = new Vector3(-2.56999993f, -9.69999981f, 57.9944572f);
                four.PlayLevelProxyClip();
                break;

        }


    }

    public void showInv()
    {
        //gameObject.transform.position = new Vector3(0, -4.5f, -4);
        //Debug.Log("fiwhvbkszgbkfvsdjbvfksbgkf");
        var distance = 2f;
        var newScale = 1f;
        var startPoint = 1f;
        if (inventory.Count >= 9)
        {
            distance = 1.5f;
            newScale = 0.9f;
            startPoint = 2.1f;
        }
        for (var i = 0; i < inventory.Count; i++)
        {
            var temp = inventory[i];
            var newBox = Instantiate(itembox, new Vector3(startPoint - (inventory.Count) + distance * i, -2.3f, -5), Quaternion.identity);
            
            newBox.name = i.ToString() + " box";
            var canvas = newBox.transform.Find("Canvas").gameObject;
            var text1 = canvas.transform.Find("Descriptor").gameObject.GetComponent<Text>();
            text1.text = temp.ToString();
            var newBoxe = newBox.GetComponent<InvBox>();
            newBoxe.descriptor = temp.ToString();
            instantiated.Add(newBox);
            if (numbers.Contains(temp.ToString()))
            {
                string path = "Animals/" + temp.ToString();
                //Debug.Log(path);
                var animalPath = GameObject.Find(path);
                var animal = Instantiate(animalPath, new Vector3(0, 0, -1), Quaternion.identity);
                
                animal.transform.parent = newBox.transform;
                newBox.transform.localScale = new Vector3(newScale, newScale, newScale);
                animal.transform.localPosition = new Vector3(0, -1, -1);
            }
                

        }


    }

    public void showBubbles()
    {
        var startPoint = 6.0f;
        if (inventory.Count >= 9)
        {
            startPoint = 5.4f;
        }
        var help = GameObject.Find("GoalSilhouette");
        for (var i = 0; i < solutions.Count; i++)
        {
            List<int> keys = new List<int>(solutions.Keys);
            for (var a = 0; a < keys[i]; a++)
            {
                var f = Instantiate(help, new Vector3(-0.4f - startPoint - a * 0.35f, 4.5f - i * 0.8f, -3), Quaternion.identity);
                f.name = i.ToString() + " row " + "chick " + a.ToString();
                mainobjects.Add(f);
            }
            for (var k = 0; k < solutions[keys[i]]; k++)
            {
                var t = Instantiate(bubble, new Vector3(0.2f - startPoint + k * 0.7f, 4.5f - i * 0.8f, -3), Quaternion.identity);
                t.name = i.ToString() + " row " + k.ToString();
                bubbles.Add(t);
            }
                

        }
    }

    public static void updateBubbles()
    {
        for (var i = 0; i < solutions1.Count; i++)
        {
            List<int> keys = new List<int>(solutions.Keys);
            for (var k = 0; k < solutions1[keys[i]]; k++)
            {
                var renderer = GameObject.Find(i.ToString() + " row " + k.ToString()).GetComponent<SpriteRenderer>();
                renderer.color = new Color(0.1260559f, 0.735849f, 0.1f, 1);
            }
        }
    }

    public void showSpots(string worldnum)
    {
        var distance = 1.8f;
        var newScale = 1f;
        var startPoint = 0.2f;
        var vertPoint = -0.5f;
        if (inventory.Count >= 9)
        {
            distance = 1.6f;
            newScale = 0.9f;
            startPoint = 1.2f;
            vertPoint = -0.17f;
        }
        itemslot = GameObject.Find("LevelMaster/Boxslots/Boxslot" + worldnum);
        for (var i = 0; i < inventory.Count; i++)
        {
            var k = Instantiate(itemslot, new Vector3(startPoint - (inventory.Count) + distance * i, vertPoint, -4), Quaternion.identity);
            k.name = i.ToString() + " slot";
            k.transform.localScale = new Vector3(newScale, newScale, newScale);
            instantiated2.Add(k);

        }
        mainobjects.Add(Instantiate(outputslot, new Vector3(7.5f, 0, -4), Quaternion.identity));
        mainobjects.Add(Instantiate(targetslot, new Vector3(0f, 4, -4), Quaternion.identity));
    }

    public static void finishLevel(int stars)
    {
        //var temp = GameObject.Find("FinishLevelButton");
        //temp.GetComponent<Canvas>().enabled = true;


        var temp2 = GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble.GetComponent<LevelController>();
        temp2.complete = true;


        var saki = GameObject.Find("LevelMaster/LevelEndMenu").GetComponent<LevelEndMenu>();
        var mars = GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble.name.ToCharArray();
        saki.EnterMenu(stars, ((temp2.unlockslevels.Count > 0) && mars[mars.Length-1].ToString() != "0"));

        if (mars[mars.Length - 1].ToString() == "0")
        {
            var ichi = GameObject.Find("Backgrounds/World" + mars[0].ToString() + "BG/gate");
            Destroy(ichi);
        }


        for (var i = 0; i < temp2.unlockslevels.Count; i++)
        {
            GameObject temp31 = GameObject.Find("LevelScreen/" + temp2.unlockslevels[i]);
            var temp3 = temp31.GetComponent<LevelController>();

            temp3.unlocked = true;
            temp31.AddComponent<Glow>();
        }

        if (GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble.name == "TUTBUTTON")
        {
            GameObject.Find("TutorialPieces").GetComponent<Tutorial>().SecondStart();
        }
    }

    public static void logPrepared()
    {
        var prepared1 = new List<string>();
        for (var i = 0; i < instantiated2.Count; i++)
        {
            var temp1 = instantiated2[i].GetComponent<BoxSlot>();
            if (temp1 != null)
            {
                // Debug.Log(temp1.slotted);
                if (temp1.slotted != null)
                {
                    var temp = temp1.slotted.GetComponent<InvBox>();
                    prepared1.Add(temp.descriptor);
                }
            }

        }
        Logger.WriteString("Player updated equation: " + String.Join(" ", prepared1));
    }
    public static void prepareshunting()
    {
        prepared = new List<string>();
        prepared_gameobject = new List<GameObject>();
        for (var i = 0; i < instantiated2.Count; i++)
        {
            var temp1 = instantiated2[i].GetComponent<BoxSlot>();
            if (temp1 != null)
            {
                // Debug.Log(temp1.slotted);
                if (temp1.slotted != null)
                {
                    var temp = temp1.slotted.GetComponent<InvBox>();
                    prepared.Add(temp.descriptor);
                    prepared_gameobject.Add(temp1.slotted);
                }
            }

        }
        var ichika = mainobjects[0];
        var canvas = ichika.transform.Find("BALLOON").Find("bone_1").Find("Canvas").gameObject;
        var saki = canvas.transform.Find("Results").gameObject.GetComponent<Text>();

        string message = "";
        List<string> outputs = new List<string>();
        if (ShuntingYard.Validate(prepared))
        {
            
            var result = ShuntingYard.ParseRPN(ShuntingYard.Shunt(prepared), out outputs);
            if (result < -100)
            {

                message = "D";
            } else
            {
                message = result.ToString();
            }
            
            if (result == goal)
            {
                //Debug.Log("irehnlie");
                var chance = true;
                for (var k = 0; k < postedsolutions.Count; k++)
                {
                    if (postedsolutions[k].Count == prepared.Count)
                    {
                        var set = new HashSet<string>(postedsolutions[k]);
                        if (set.SetEquals(prepared))
                        {
                            chance = false;
                            Logger.WriteString("Player submitted - already submitted. Submitted answer: " + String.Join(" ", prepared));
                            message = "A";
                        }
                    }
                }
                if (chance) {
                    
                    solutions1[prepared.Count] += 1;
                    //GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble.GetComponent<LevelController>().solutions1[prepared.Count] += 1;
                    postedsolutions.Add(prepared);

                    // manage adding stars
                    var temp2 = GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble.GetComponent<LevelController>();
                    if ((prepared.Count == inventory.Count) && !temp2.solvedBig)
                    {

                    }

                    // update bubbles
                    updateBubbles();

                    var thing = true;
                    foreach (var item in solutions.Keys)
                    {
                        if (solutions[item] != solutions1[item])
                        {
                            thing = false;
                        }
                    }
                    Logger.WriteString("Player submitted - correct. Submitted answer: " + String.Join(" ", prepared));
                    GameObject.Find("LevelMaster").GetComponent<Master>().DoPostBridge(prepared.Count, thing);
                    
                    // Originally finish level was here.
                } else
                {
                    Logger.WriteString("Player submitted - incorrect. Submitted answer: " + String.Join(" ", prepared));
                }
                
            }

        }
        else
        {
            Logger.WriteString("Player submitted - invalid. Submitted answer: " + String.Join(" ", prepared));
            message = "X";
        }

        GameObject.Find("LevelMaster").GetComponent<ShuntingYard>().halfstep(outputs, message);

        //Debug.Log(ichika);
        //Debug.Log(canvas);
        //Debug.Log(saki);
        //Debug.Log(ShuntingYard.ParseRPN(ShuntingYard.Shunt(prepared)).ToString());
    }

    void DoPostBridge(int answerLength, bool complete)
    {
        StartCoroutine(DoPostSubmitUpdates(answerLength, complete));
    }
    IEnumerator DoPostSubmitUpdates(int answerLength, bool complete)
    {
        var temp1 = GameObject.Find("LevelMaster").GetComponent<Master>().levelbubble;

        var temp2 = temp1.GetComponent<LevelController>();
        var starComp = GameObject.Find("LevelMaster").GetComponent<Star>();

        var thingy = false;
        if (temp2.achievedStars > 0)
        {
            thingy = true;
        }
        if ((answerLength == inventory.Count) && !temp2.solvedBig)
        {
            temp2.achievedStars += 1;
            Master.totalStars += 1;

            temp2.solvedBig = true;
            starComp.ShowStar(temp2.achievedStars.ToString());
            if (temp2.achievedStars == 2)
            {
                yield return new WaitForSeconds(1.5f);
            }
            else
            {
                yield return new WaitForSeconds(3.1f);
            }

        }
        var solvedSols = 0f;
        foreach (var item in solutions1.Values)
        {
            solvedSols += item;
        }

        if (((float)solvedSols >= (((float)numOfSolutions) / 2)) && !temp2.solvedHalf)
        {
            temp2.achievedStars += 1;
            Master.totalStars += 1;

            temp2.solvedHalf = true;
            starComp.ShowStar(temp2.achievedStars.ToString());
            if (temp2.achievedStars == 2)
            {
                yield return new WaitForSeconds(1.5f);
            } else
            {
                yield return new WaitForSeconds(3.1f);
            }

        }
        if (complete)
        {
            Master.totalStars += 1;
            temp2.achievedStars += 1;

            starComp.ShowStar(temp2.achievedStars.ToString());
            yield return new WaitForSeconds(1.5f);
        }

        if (temp2.achievedStars >= 1)
        {
            yield return new WaitForSeconds(1f);

            if (thingy == false || (thingy == true & temp2.achievedStars != 2))
            {
                Logger.WriteString("Player completes level with " + temp2.achievedStars + " stars.");
                finishLevel(temp2.achievedStars);
            }
            
        }
    }

    
}
