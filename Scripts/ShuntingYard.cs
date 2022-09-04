using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShuntingYard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool Validate(List<string> lister)
    {
        var validated = new List<string>();
        var operators = new List<string>();
        operators.Add("+");
        operators.Add("-");
        operators.Add("×");
        operators.Add("÷");
        for (var i = 0; i < lister.Count; i++)
        {
            /* if ((i != 0) && (i != lister.Count-1) && (lister[i] == "÷"))
            {
                if (!(operators.Contains(lister[i-1]) || lister[i - 1] == "(" || lister[i - 1] == ")") && !(operators.Contains(lister[i + 1]) || lister[i + 1] == "(" || lister[i + 1] == ")"))
                {
                    if (((int.Parse(lister[i-1]) / int.Parse(lister[i+1])) * int.Parse(lister[i+1])) != int.Parse(lister[i-1]))
                    {
                        return false;
                    }
                }
            } */
            if (validated.Count == 0)
            {
                if (lister[i] == "+" || lister[i] == "-" || lister[i] == "×" || lister[i] == "÷" || lister[i] == ")")
                {
                    return false;
                }
                else if ((i == lister.Count - 1) && lister[i] == "(")
                {
                    return false;
                }
                else
                {
                    validated.Add(lister[i]);
                }
            }
            else if (i == lister.Count - 1)
            {
                if (operators.Contains(lister[i]) || lister[i] == "(")
                {
                    return false;
                }
                else
                {
                    validated.Add(lister[i]);
                }
                if (validated.Contains("(") && !validated.Contains(")"))
                {
                    return false;
                }
            }
            else if (operators.Contains(validated[i - 1]) || validated[i - 1] == "(")
            {
                if (operators.Contains(lister[i]) || lister[i] == ")")
                {
                    return false;
                }
                else
                {
                    validated.Add(lister[i]);
                }
            }
            else
            {
                if ((!operators.Contains(lister[i])) && lister[i] != ")")
                {
                    return false;
                }
                else
                {
                    validated.Add(lister[i]);
                }
            }
            
            
            if (!validated.Contains("(") && validated.Contains(")"))
            {
                return false;
            }
        }

        if (validated.Count % 2 == 0)
        {
            return false;
        }
        return true;
    }

    public static List<object> Shunt(List<string> lister)
    {
        Dictionary<string, int> precedence = new Dictionary<string, int>();
        precedence.Add("+", 2);
        precedence.Add("-", 2);
        precedence.Add("×", 3);
        precedence.Add("÷", 3);
        precedence.Add("(", 1);
        var output = new List<object>();
        var stacker = new Stack<string>();
        for (var i = 0; i < lister.Count; i++)
        {
            /* switch(lister[i])
            {
                case "+";
                    break;
                case "-";
                    break;
                case "×";
                    break;
                case "÷";
                    break;
                default:
                    break;
            } */

            if (lister[i] == "+" || lister[i] == "-" || lister[i] == "×" || lister[i] == "÷")
            {
                if (stacker.Count == 0)
                {
                    stacker.Push(lister[i]);
                }
                else if (precedence[lister[i]] > precedence[stacker.Peek()])
                {
                    stacker.Push(lister[i]);
                }
                else
                {
                    while ((stacker.Count != 0) && !(precedence[lister[i]] > precedence[stacker.Peek()]))
                    {
                        output.Add(stacker.Pop());
                    }
                    stacker.Push(lister[i]);
                }
            } 
            else if (lister[i] == "(") {
                stacker.Push(lister[i]);
            }
            else if (lister[i] == ")") {
                while (stacker.Peek() != "(")
                {
                    output.Add(stacker.Pop());
                }
                stacker.Pop();

            }
            else
            {
                output.Add(lister[i]);
            }

        }
        var etc = stacker.Count;
        for (var j = 0; j < etc; j++)
        {
            output.Add(stacker.Pop());
        }


        return output;
    }

    public static int ParseRPN(List<object> lister, out List<string> output_list)
    {
        int output = 0;
        output_list = new List<string>();
        //Debug.Log(string.Join(" ", lister.ToArray()));
        while (lister.Count != 1) {
            int i = 0;
            var ib = lister[i].ToString();
            while (!((ib == "+" || ib == "-" || ib == "×" || ib == "÷")))
            {
                i += 1;
                ib = lister[i].ToString();
            }
            var one = lister[i - 2].ToString();
            var two = lister[i - 1].ToString();
            if (lister[i].GetType() == typeof(string))
            {
                switch (lister[i].ToString())
                {
                    case "+":
                        lister[i - 2] = int.Parse(one) + int.Parse(two);
                        break;
                    case "-":
                        lister[i - 2] = int.Parse(one) - int.Parse(two);
                        break;
                    case "×":
                        lister[i - 2] = int.Parse(one) * int.Parse(two);
                        break;
                    case "÷":
                        lister[i - 2] = int.Parse(one) / int.Parse(two);
                        if (int.Parse(two) * (int) lister[i-2] != int.Parse(one))
                        {
                            output = -900;
                        }
                        break;
                }

                output_list.Add(lister[i].ToString());
                lister.RemoveAt(i);
                lister.RemoveAt(i - 1);
                
            }
            
            if (i > lister.Count - 1)
            {
                i = 0;
            }
        }
        for (var i = 0; i < lister.Count; i++)
        {
            //Debug.Log(lister[i]);
            output += int.Parse(lister[i].ToString());
        }
        //
          // DISABLE WHEN GENERATING / ENABLE WHEN PLAYING
        //
        return output;
    }

    public void halfstep(List<string> steps, string msg)
    {
        StartCoroutine(ShowStep(steps, msg));
    }

    IEnumerator FloatUpBalloon(GameObject ichika)
    {
        var canvas = ichika.transform.Find("BALLOON").Find("bone_1").Find("Canvas").gameObject;
        var sakibye = canvas.transform.Find("Results").gameObject;
        sakibye.GetComponent<Text>().text = "";
        
        var timeElapsed = 0f;
        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;
            ichika.transform.position += new Vector3(0, 5f * Time.deltaTime, 0);
            yield return null;
        }
        
        Destroy(ichika);
        yield return null;
    }

    IEnumerator FloatUpBalloon2(GameObject saki, int steps)
    {
        yield return new WaitForSeconds(steps * 0.5f);
        var timeElapsed = 0f;
        while (timeElapsed < 2.0f)
        {
            timeElapsed += Time.deltaTime;
            saki.transform.position += new Vector3(0, 5f * Time.deltaTime, 0);
            yield return null;
        }
        saki.transform.parent = GameObject.Find("LevelMaster").transform;
        saki.name = "OutputSlot";
        yield return null;
    }

    IEnumerator ShowStep(List<string> steps, string msg)
    {
        var ichika = Master.mainobjects[0];
        
        StartCoroutine(FloatUpBalloon(ichika));
        var saki1 = Instantiate(ichika, ichika.transform.position - new Vector3(0f, 10f, 0f), Quaternion.identity);
        Master.mainobjects[0] = saki1;
        StartCoroutine(FloatUpBalloon2(saki1, steps.Count));
        var canvas = saki1.transform.Find("BALLOON").Find("bone_1").Find("Canvas").gameObject;
        var sakibye = canvas.transform.Find("Results").gameObject;
        sakibye.GetComponent<Text>().text = msg;
        var templist = new List<string>(Master.prepared);
        var templist2 = new List<int>();

        for (var k = 0; k < Master.prepared_gameobject.Count; k++)
        {
            Master.prepared_gameobject[k].GetComponent<InvBox>().enabled = false;
        }
        for (var i = 0; i < steps.Count; i++)
        {
            var temp1 = templist.IndexOf(steps[i]);
            templist.Remove(steps[i]);
            templist.Insert(temp1, "NULL");
            templist2.Add(temp1);
            if (Master.prepared_gameobject[temp1] == null)
            {
                break;
            }
            var temp2 = Master.prepared_gameobject[temp1].transform.GetChild(2).gameObject;
            var saki = temp2.transform.GetChild(0).Find("operator").Find("shape").gameObject;

            var timeElapsed = 0.0;
            var timeTaken = 1f;
            var thing = 3.0;
            while (timeElapsed < timeTaken)
            {
                timeElapsed += Time.deltaTime;
                saki.GetComponent<Bloomer>().IncreaseColorByIncrement((float) Math.Pow(thing, (double) Time.deltaTime / timeTaken));
                yield return null;
            }

            yield return new WaitForSeconds(.01f);
        }

        yield return new WaitForSeconds(1f);


        var timeElapsed1 = 0.0;
        var timeTaken1 = 0.25f;
        var thing1 = 0.333333;
        while (timeElapsed1 < timeTaken1)
        {
            for (var i = 0; i < templist2.Count; i++)
            {


                if (Master.prepared_gameobject[templist2[i]] == null)
                {
                    continue;
                }
                var temp2 = Master.prepared_gameobject[templist2[i]].transform.GetChild(2).gameObject;
                var saki = temp2.transform.GetChild(0).Find("operator").Find("shape").gameObject;
                saki.GetComponent<Bloomer>().IncreaseColorByIncrement((float)Math.Pow(thing1, (double)Time.deltaTime / timeTaken1));

            }
            timeElapsed1 += Time.deltaTime;
            yield return null;
        }
        

        for (var k = 0; k < Master.prepared_gameobject.Count; k++)
        {
            Master.prepared_gameobject[k].GetComponent<InvBox>().enabled = true;
        }

        yield return null;



    }
}
