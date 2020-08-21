using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
using System.Collections.Specialized;

public class TestMovement : MonoBehaviour
{
    private float mytime = 0.0F;


    // Start is called before the first frame update
    void Start()
    {
        // idk what to do with this yet
        transform.position = new Vector3(0.5F, 0.5F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        mytime += Time.deltaTime;
        float moveHor = 0;
        float moveVer = 0;

        if (mytime >= 0.25) {
            moveHor = Input.GetAxis("Horizontal");
            moveVer = Input.GetAxis("Vertical");
            if (moveHor > 0){
                moveHor = 1;
            }
            else if (moveHor < 0) {
                moveHor = -1;
            }

            if (moveVer > 0)
            {
                moveVer = 1;
            }
            else if (moveVer < 0)
            {
                moveVer = -1;
            }

            mytime = 0.0F;
        }

        /*
        // button hold = 1 move max
        if (Input.GetKeyDown("a")){
            moveHor -= 1;
        }
        if (Input.GetKeyDown("d")) {
            moveHor += 1;
        }

        if (Input.GetKeyDown("s")){
            moveVer -= 1;
        }
        if (Input.GetKeyDown("w")){
            moveVer += 1;
        }
        */
        Vector3 move = new Vector3(moveHor, moveVer, 0);
        Vector3 newpos = transform.position + move;
        if (Math.Abs(newpos[0])>11){
            newpos[0] -= move[0];
        }
        if (Math.Abs(newpos[1]) > 5){
            newpos[1] -= move[1];
        }

        transform.position = newpos;
    }
}
