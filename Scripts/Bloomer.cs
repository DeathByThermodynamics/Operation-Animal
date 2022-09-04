using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloomer : MonoBehaviour
{

    Material m_Material;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        originalColor = m_Material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnMouseEnter()
    {
        Debug.Log("skejgbskbeg");
        m_Material.SetColor("_EmissionColor", originalColor * 3);
    }

    void OnMouseExit()
    {
        m_Material.SetColor("_EmissionColor", originalColor);
    } */

    public void ResetColor()
    {
        m_Material.SetColor("_EmissionColor", originalColor);
    }

    public void IncreaseColorByIncrement(float intensity)
    {
        var color = m_Material.GetColor("_EmissionColor");
        m_Material.SetColor("_EmissionColor", color * intensity);
    }


}
