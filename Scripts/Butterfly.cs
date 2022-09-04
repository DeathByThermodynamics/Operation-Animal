using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    
    float timeToMove = 1.5f;
    float distanceToMove = 16.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMoving(float minSize, float maxSize)
    {
        StartCoroutine(MoveToTop(minSize, maxSize));
    }


    IEnumerator MoveToTop(float minSize, float maxSize)
    {
        transform.parent = Camera.main.transform;
        var hi = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(hi, hi, hi);
        var timeElapsed = 0f;
        while (timeElapsed < timeToMove)
        {
            timeElapsed += Time.deltaTime;
            gameObject.transform.localPosition += new Vector3(0, distanceToMove * Time.deltaTime / timeToMove, 0);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void StartMoving2()
    {
        StartCoroutine(MoveToTop2());
    }


    IEnumerator MoveToTop2()
    {
        transform.parent = Camera.main.transform;
        var hi = 8f;
        transform.localScale = new Vector3(hi, hi, hi);
        var timeElapsed = 0f;
        while (timeElapsed < timeToMove * 1.5f)
        {
            timeElapsed += Time.deltaTime;
            gameObject.transform.localPosition += new Vector3(0, (distanceToMove * 1.5f) * Time.deltaTime / timeToMove, 0);
            yield return null;
        }
        Destroy(gameObject);
    }
}
