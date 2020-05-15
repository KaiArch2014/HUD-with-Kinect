using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{

    InterfaceAnimManager anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<InterfaceAnimManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disappear(float t) {

        Invoke("dis", t);
    }

    void dis() {
        anim.startDisappear();
    }

    public void Appear(float t) {

        Invoke("app", t);
    }

    void app() {

        anim.startAppear();
    }
}
