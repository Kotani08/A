using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = this.transform.position;

        //移動　右
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed;
        }

        //移動　左
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed;
        }
        transform.position = pos;
    }
}
