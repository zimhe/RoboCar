using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var left = Input.GetAxis("Horizontal");

        var fwd = Input.GetAxis("Backward");

        print(fwd);
    }
}
