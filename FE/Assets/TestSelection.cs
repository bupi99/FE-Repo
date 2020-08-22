using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;

public class TestSelection : MonoBehaviour
{
    private Vector3 menuspawn = new Vector3(9, 1, 0);
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu Temp");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 TLeftCorn = new Vector2(transform.position[0] - 0.5F, transform.position[1] + 0.5F);
        Vector2 BRightCorn = new Vector2(transform.position[0] + 0.5F, transform.position[1] - 0.5F);
        int numOver = Physics2D.OverlapAreaAll(TLeftCorn, BRightCorn).Length;

        if (Input.GetKeyDown(".") && numOver == 1) {
            GameObject tmpObj = GameObject.Instantiate(menu, menuspawn, Quaternion.identity) as GameObject;
        }

    }
}
