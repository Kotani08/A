using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoolMov : MonoBehaviour
{
    [SerializeField]
    private float i = 0;

    // Update is called once per frame
    void Update()
    {
        if(i <=1000f){
        this.transform.localPosition = this.transform.localPosition + new Vector3(0,0.05f,0);
        i++;
        }
        else
        {
            this.transform.localPosition = this.transform.localPosition + new Vector3(0,-0.05f,0);
            i++;
            if(i >= 2000f)
            {
                i=0;
            }
        }
    }
}
