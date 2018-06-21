using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour {
    public float Speed = 1f;
    private float direction = 1f;
    private Image panel;
    public void FadeIn()
    {
        direction = 1f;
        if (panel != null) panel.enabled = true;
    }
    public void FadeOut()
    {
        direction = -1f;
        if(panel != null) panel.enabled = true;
    }
    private void Start()
    {
        var c = GetComponents<Component>();
        panel = GetComponent<Image>();
    }
    void Update () {
        Color color = panel.color;
        color.a = Mathf.Clamp(color.a + Speed * direction * Time.deltaTime, 0f, 1f);
        panel.color = color;
        if(color.a==0)
        {
            panel.enabled = false;
        }
    }
}