using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMod : MonoBehaviour
{
    public string[] mod;
    public Text text;
    int i;
    bool delay;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        delay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!delay) {
            t += Time.deltaTime;
        }

        if (t > 1)
        {
            delay = true;
            t = 0;
        }

    }

    public void modChange() {
        if (delay)
        {
            i = i + 1;
            if (i == mod.Length)
            {
                i = 0;
            }
            text.text = mod[i];

        }
        delay = false;
    }

    public void modChangeUp()
    {
        if (delay)
        {
            if (i - 1 < 0)
            {
                i = mod.Length-1;
            }
            else {
                i = i - 1;
            }
            text.text = mod[i];
         
        }
        delay = false;
    }

}
