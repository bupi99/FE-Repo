using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class TestSelection : MonoBehaviour
{
    private Vector3 menuspawn = new Vector3(9, 1, 0);
    public GameObject menu;
    public GameObject lastselected;
    public GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu Temp");
        tile = GameObject.Find("Highlighted Tile");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 TLeftCorn = new Vector2(transform.position[0] - 0.5F, transform.position[1] + 0.5F);
        Vector2 BRightCorn = new Vector2(transform.position[0] + 0.5F, transform.position[1] - 0.5F);
        Collider2D[] overlapElems = Physics2D.OverlapAreaAll(TLeftCorn, BRightCorn);
        int numElems = overlapElems.Length;

        if (Input.GetKeyDown(".")){
            if (numElems >= 1){
                string nameElem = overlapElems[0].name;
                Debug.Log(nameElem);
                if (nameElem == "Girl Sword Test"){
                    lastselected = GameObject.Find(nameElem);
                    //Spawn Movement Tiles
                    SpawnTiles(overlapElems[0].GetComponent<TestStats>().mvnt, overlapElems[0].transform.position);
                }
                if (nameElem == "Highlighted Tile(Clone)"){
                    //Move Character to spot
                    lastselected.transform.position = transform.position;
                    //Remove the highlighted tiles
                    DestroyTiles();
                }
            }
            else{
                //Remove the highlighted tiles
                DestroyTiles();
            }
        }

        //Spawn Menu
        //GameObject.Instantiate(menu, menuspawn, Quaternion.identity);

    }
    void SpawnTiles(int mvnt, Vector3 pos)
    {
        GameObject.Instantiate(tile, pos, Quaternion.identity);
        if (mvnt == 0) { return; }
        else {
            // TODO: bounds checks
            SpawnTiles(mvnt - 1, new Vector3(pos[0] - 1, pos[1], pos[2]));
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1] - 1, pos[2]));
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1], pos[2] - 1));
            SpawnTiles(mvnt - 1, new Vector3(pos[0] + 1, pos[1], pos[2]));
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1] + 1, pos[2]));
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1], pos[2] + 1));
        }
    }

    void DestroyTiles()
    {
        for (int i = 0; i < 10000; i++){
            GameObject temp = GameObject.Find("Highlighted Tile(Clone)");
            GameObject.DestroyImmediate(temp);
        }
    }
}
