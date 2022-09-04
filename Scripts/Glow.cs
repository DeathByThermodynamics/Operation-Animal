using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    Renderer m_renderer;
    public Color whatever;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        whatever = m_renderer.material.color;
        originalColor = m_renderer.material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        m_renderer.material.SetColor("_Color", new Color(whatever.r + 0.3f, whatever.g + 0.3f, whatever.b + 0.3f));
        m_renderer.material.SetColor("_EmissionColor", originalColor * 4);
    }

    void OnMouseExit()
    {
        m_renderer.material.SetColor("_EmissionColor", originalColor);
        m_renderer.material.SetColor("_Color", new Color(whatever.r, whatever.g, whatever.b));
    }
}
