using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueCtr : MonoBehaviour
{
    Slider slider;
    bool isOn;
    public Image[] image;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        isOn = true;
        slider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            t += Time.deltaTime;
            for (int i = 0; i < 3; i++)
            {
                if (image[i].color.a > 0)
                {
                    image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, image[i].color.a - (t * 0.5f));
                }
                else
                {
                    image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, 0);
                }
            }

        }
        isOn = false;
       

    }

    public void SliderOn(float value) {
        isOn = true;
        for (int i = 0; i < 3; i++)
        {          
                image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, 1);      
        }

        slider.value = value;

    }
}
