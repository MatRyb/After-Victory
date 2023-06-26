using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    public bool ChangeDirection = false;

    private float slashTimestamp = 0;
    // Start is called before the first frame update
    void Start()
    {
        //slashTimestamp = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = GetComponentInChildren<Renderer>();
        Shader shader = Shader.Find("Unlit/SlashShader");
        rend.material.SetFloat("_SlashDirection", ChangeDirection ? -1.0f : 1.0f);
        rend.material.SetFloat("_SlashProgress", Time.realtimeSinceStartup - slashTimestamp);
        Debug.Log("wat");
    }

    public void Slash()
    {
        slashTimestamp = Time.realtimeSinceStartup;
    }
}
