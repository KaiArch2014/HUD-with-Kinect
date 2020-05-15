using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class border : MonoBehaviour
{
    public float left;
    public float right;
    public float up;
    public float down;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < left) {

            this.transform.position = new Vector3(left ,this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > right)
        {

            this.transform.position = new Vector3(right, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.y > up)
        {

            this.transform.position = new Vector3(this.transform.position.x, up, this.transform.position.z);
        }

        if (this.transform.position.y < down)
        {

            this.transform.position = new Vector3(this.transform.position.x, down, this.transform.position.z);
        }
    }
}
