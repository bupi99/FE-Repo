using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStats : MonoBehaviour
{
    public int mvnt;
    public int strength;
    public int stamina;
    public int base_stamina;
    public int att_range;
    public int base_health;
    public int health;
    public int dmg;
    public int sight;
    public GameObject cursor;
    public GameObject bar;
    public GameObject HP_mask;
    public GameObject ST_mask;
    public GameObject bar_instance;
    public bool bar_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor");
        bar = GameObject.Find("Health and Stamina Bar");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (isOverlap(pos) & !bar_flag)
        {
            pos[1] += 1;
            bar_instance = GameObject.Instantiate(bar, pos, Quaternion.identity);

            HP_mask = bar_instance.transform.GetChild(0).gameObject;
            ST_mask = bar_instance.transform.GetChild(1).gameObject;
            HP_mask.transform.localPosition = new Vector3(2.2f * (System.Convert.ToSingle(health) / System.Convert.ToSingle(base_health)) - 2.2f, 0.25f, 0);
            ST_mask.transform.localPosition = new Vector3(2.2f * (System.Convert.ToSingle(stamina) / System.Convert.ToSingle(base_stamina)) - 2.2f, -0.25f, 0);

            bar_flag = true;
        }
        else if (bar_flag & !isOverlap(pos))
        {
            while (GameObject.Find("Health and Stamina Bar(Clone)"))
            {
                GameObject.DestroyImmediate(GameObject.Find("Health and Stamina Bar(Clone)"));
            }
            bar_flag = false;
        }
    }

    bool isOverlap(Vector3 pos){
        if (cursor.transform.position == pos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GettingAttacked(int damage) {
        health -= damage;
        isDead();
    }
    
    void isDead()
    {
        if (health <= 0)
        {
            //GameObject.DestroyImmediate(this.gameObject);
            // yeet off the screen to delete makes life easier

            transform.position = new Vector3(100, 100, 0);
        }
    }
}
