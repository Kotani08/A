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

        //à⁄ìÆÅ@âE
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed;
        }

        //à⁄ìÆÅ@ç∂
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed;
        }
        transform.position = pos;
    }
}
