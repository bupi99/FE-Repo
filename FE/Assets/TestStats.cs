using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStats : MonoBehaviour
{
    public int mvnt;
    public int strength;
    public int idk;
    public int att_range;
    public int health;
    public int dmg;
    public int sight;
    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GettingAttacked(int damage) {
        health -= damage;
        CheckDeath();
    }
    
    void CheckDeath()
    {
        if (health <= 0)
        {
            //GameObject.DestroyImmediate(this.gameObject);
            // yeet off the screen to delete makes life easier

            transform.position = new Vector3(100, 100, 0);
        }
    }
}
