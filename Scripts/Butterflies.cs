using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterflies : MonoBehaviour
{
    public float time = 9.0f;
    public float intervals = 5;
    public int maxButterflies = 5;
    public Vector3 startingline;
    public int orderNum = 201;
    public float maxSize;
    public float minSize;
    GameObject oscar;

    GameObject butterflyObject1;
    GameObject butterflyObject2;
    GameObject butterflyObject3;
    GameObject butterflyObject4;
    GameObject butterflyObject5;
    List<GameObject> butterflies;

    // Start is called before the first frame update
    void Start()
    {
        //oscar = GameObject.Find("Blocker");
        //oscar.transform.localPosition = new Vector3(500, 0, -0.9f);

        butterflyObject1 = GameObject.Find("Butterflies/ButterflyOrange");
        butterflyObject2 = GameObject.Find("Butterflies/ButterflyBlue");
        butterflyObject3 = GameObject.Find("Butterflies/ButterflyGreen");
        butterflyObject4 = GameObject.Find("Butterflies/ButterflyPink");
        butterflyObject5 = GameObject.Find("Butterflies/ButterflyYellow");

        butterflies = new List<GameObject>();
        butterflies.Add(butterflyObject1);
        butterflies.Add(butterflyObject2);
        butterflies.Add(butterflyObject3);
        butterflies.Add(butterflyObject4);
        butterflies.Add(butterflyObject5);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButterflyAnimation()

    {
        orderNum = 100;
        //oscar.transform.localPosition = new Vector3(0, 0, -0.9f);
        StartCoroutine(ButterflyAnimation());
    }

    public void CloseCurtainsExternal()
    {
        StartCoroutine(CloseCurtains());
    }

    IEnumerator ButterflyAnimation()
    {
       
        var butterflies = 1f;
        for (var i = 0; i < intervals; i++)
        {

            SpawnButterflies(butterflies);
            butterflies += 1f;
            if (butterflies > maxButterflies)
            {
                butterflies = maxButterflies;
            }
            yield return new WaitForSeconds(0.6f * time / intervals);
        }
        StartCoroutine(CloseCurtains());
        for (var i = 0; i < intervals; i++)
        {

            SpawnButterflies(maxButterflies);
            yield return new WaitForSeconds(0.8f * time / intervals);
        }
        //oscar.transform.localPosition = new Vector3(500, 0, -0.9f);
        for (var i = 0; i < intervals; i++)
        {
            if (butterflies == 1)
            {
                butterflies = 2;
            }
            SpawnButterflies(butterflies);
            butterflies -= 1f;
            yield return new WaitForSeconds(0.6f * time / intervals);
        }
        
        yield return null;
    }

    public void SpawnButterflies(float num)
    {
        var position = Camera.main.transform.position;
        
        for (var k = 0; k < num; k++)
        {
            orderNum += 1;
            var saki = Random.Range(1, 6);
            var tempbutterfly = butterflies[saki -1];
            var ichika = Instantiate(tempbutterfly, new Vector3(Random.Range(position.x - 8.5f, position.x + 8.5f), position.y - 9, position.z + 1), Quaternion.identity);
            ichika.GetComponent<Butterfly>().StartMoving(minSize, maxSize);
            ichika.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = orderNum;
        }
    }

    public void SpawnButterflyMain()
    {
        var position = Camera.main.transform.position;

        orderNum += 1;
        var tempbutterfly = GameObject.Find("BigButterfly");
        var ichika = Instantiate(tempbutterfly, new Vector3(position.x, position.y - 16, position.z + 1), Quaternion.identity);
        ichika.GetComponent<Butterfly>().StartMoving2();
        ichika.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 99;
    }

    IEnumerator CloseCurtains()
    {
        var left = GameObject.Find("Main Camera/Left");
        var right = GameObject.Find("Main Camera/Right");
        var ichika = left.transform.localPosition;
        var saki = right.transform.localPosition;
        var distance = 10;
        var timeElapsed = 0f;
        while (timeElapsed < 1.0f)
        {
            left.transform.localPosition += new Vector3(15 * Time.deltaTime, 0, 0);
            right.transform.localPosition -= new Vector3(15 * Time.deltaTime, 0, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0f;
        yield return new WaitForSeconds(0.5f);
        while (timeElapsed < 1.0f)
        {
            left.transform.localPosition -= new Vector3(15 * Time.deltaTime, 0, 0);
            right.transform.localPosition += new Vector3(15 * Time.deltaTime, 0, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        left.transform.localPosition = ichika;
        right.transform.localPosition = saki;
    }


}
