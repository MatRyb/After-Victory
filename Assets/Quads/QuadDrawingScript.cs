using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadDrawingScript : MonoBehaviour
{
    public Color color;
    public bool isEnabled;

    public float CooldownProgress = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        Shader shader = Shader.Find("Unlit/QuadShader");
        rend.material.SetColor("_AddColor", color);
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = GetComponent<Renderer>();
        Shader shader = Shader.Find("Unlit/QuadShader");
        rend.material.SetColor("_AddColor", color);
        rend.material.SetFloat("_PulseScale", isEnabled ? 1.0f : 0.0f);
        rend.material.SetFloat("_CooldownProgress", CooldownProgress);
    }
}
