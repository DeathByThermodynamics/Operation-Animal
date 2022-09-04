using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static List<List<string>> listing;
    // Start is called before the first frame update
    void Start()
    {
        listing = new List<List<string>>();

        var placeholder = new List<string>();
        placeholder.Add("4");
        placeholder.Add("×");
        placeholder.Add("-");
        placeholder.Add(")");
        placeholder.Add("3");
        placeholder.Add("(");
        placeholder.Add("5"); 
        Debug.Log(ShuntingYard.Validate(placeholder));
        // × ÷
        var example = new List<string>();
        example.Add("(");
        example.Add("3");
        example.Add("+");
        example.Add("4");
        example.Add(")");
        Debug.Log(ShuntingYard.ParseRPN(ShuntingYard.Shunt(example), out var hi));

        FindSolutions(placeholder);

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<List<string>> CreatePermutations(List<string> lister)
    {
        var finallist = new List<List<string>>();
        for (var i = 0; i < lister.Count + 1; i += 2) // length of equation ( 1, 3, 5, etc.)
        {
            
            var v = i + 1;
            var templist = FindPermutationPos(lister, v);
            for (var j = 0; j < templist.Count; j++)
            {
                if (!finallist.Contains(templist[j]))
                {
                    finallist.Add(templist[j]);
                }
            }
        }
        
        return finallist;
    }

    public List<List<string>> FindPermutationPos(List<string> lister, int length)
    {
        var finallist = new List<List<string>>();
        if (length > 0)
        {
            var troll = new List<string>();
            for (var i = 0; i < lister.Count; i++)
            {
                if (!troll.Contains(lister[i]))
                {
                    var listcopy = new List<string>(lister);
                    listcopy.Remove(lister[i]);
                    var reverse = FindPermutationPos(listcopy, length - 1);
                    for (var j = 0; j < reverse.Count; j++)
                    {
                        reverse[j].Insert(0, lister[i]);
                        if (!finallist.Contains(reverse[j]))
                        {
                            finallist.Add(reverse[j]);
                        }

                    }
                    troll.Add(lister[i]);
                }
                
            }
        }
        else if (length == 0) ;
        {
            for (var i = 0; i < lister.Count; i++)
            {
                var templist1 = new List<string>();
                templist1.Add(lister[i]);
                finallist.Add(templist1);
            }
            
        }
        return finallist;
    }



    void FindSolutions(List<string> lister)
    {
        listing = CreatePermutations(lister);
        var listing2 = new List<List<string>>();
        var listing3 = new List<List<string>>();
        Debug.Log(listing.Count);

        Dictionary<int, int> results = new Dictionary<int, int>();

        var counter = 0;
        for (var i = 0; i < listing.Count; i++)
        {
            if (ShuntingYard.Validate(listing[i])) {
                listing2.Add(listing[i]);
            }
        }

        for (var i = 0; i < listing2.Count; i++)
        {
            var ichika = ShuntingYard.ParseRPN(ShuntingYard.Shunt(listing2[i]), out var hi);
            if (!results.ContainsKey(ichika))
            {
                Debug.Log(string.Join(" ", listing2[i].ToArray()) + " = " + ichika.ToString());
                results.Add(ichika, 1);
                listing3.Add(listing2[i]);
            } else
            {
                var chance = false;
                for (var k = 0; k < listing3.Count; k++)
                {

                        var set = new HashSet<string>(listing3[k]);
                        
                        var equals = set.SetEquals(listing2[i]);

                        if (ichika == ShuntingYard.ParseRPN(ShuntingYard.Shunt(listing3[k]), out var hi11) && equals)
                        {
                            chance = true;
                        }


                    
                }

                if (!chance)
                {
                    Debug.Log(string.Join(" ", listing2[i].ToArray()) + " = " + ichika.ToString()) ;
                    results[ichika] += 1;
                    listing3.Add(listing2[i]);
                }
                
            }
        }

        foreach (KeyValuePair<int, int> kvp in results)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            Debug.Log(string.Format("Target: {0}, Solutions: {1}", kvp.Key, kvp.Value));

        }
    }
}
