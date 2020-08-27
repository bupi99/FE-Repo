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

    // Start is called before the first frame update
    void Start()
    {
        mvnt = 2;
        strength = 4;
        att_range = 1;
        health = 10;
        dmg = 3;
        sight = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            GameObject.DestroyImmediate(gameObject);
        }
    }
    public void GettingAttacked(int damage) {
        health -= damage;
    }
}
