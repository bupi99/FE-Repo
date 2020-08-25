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
    public GameObject att_tile;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu Temp");
        tile = GameObject.Find("Highlighted Tile");
        att_tile = GameObject.Find("Highlighted Tile ATT");
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
                if (nameElem == "Girl Sword Test"){
                    if (numElems > 1){
                        if (overlapElems[1].name == "Highlighted Tile(Clone)"){
                            //Remove the highlighted tiles
                            DestroyTiles("Highlighted Tile(Clone)");
                            //Spawn Attack Tiles
                            SpawnTiles(lastselected.GetComponent<TestStats>().att_range, transform.position, "Highlighted Tile ATT");
                        }
                    }
                    else
                    {
                        // Remove Attacking tiles
                        DestroyTiles("Highlighted Tile ATT(Clone)");
                        // Set last selected character
                        lastselected = GameObject.Find(nameElem);
                        //Spawn Movement Tiles
                        SpawnTiles(overlapElems[0].GetComponent<TestStats>().mvnt, overlapElems[0].transform.position, "Highlighted Tile");
                    }
                }
                if (nameElem == "Highlighted Tile(Clone)"){
                    //Move Character to spot
                    lastselected.transform.position = transform.position;
                    //Remove the highlighted tiles
                    DestroyTiles("Highlighted Tile(Clone)");
                    //Spawn Attack Tiles
                    SpawnTiles(lastselected.GetComponent<TestStats>().att_range, transform.position, "Highlighted Tile ATT");
                    //Remove the attacking tile on the character
                    Vector2 TopLeft = new Vector2(transform.position[0] - 0.5F, transform.position[1] + 0.5F);
                    Vector2 BotRight = new Vector2(transform.position[0] + 0.5F, transform.position[1] - 0.5F);
                    Collider2D[] playertileoverlap = Physics2D.OverlapAreaAll(TopLeft, BotRight);
                    
                    for (int i=0; i<playertileoverlap.Length; i++) { 
                        if (playertileoverlap[i].name == "Highlighted Tile ATT(Clone)"){
                            GameObject.DestroyImmediate(GameObject.Find("Highlighted Tile ATT(Clone)"));
                        }
                    }

                }
                
                if (nameElem == "Highlighted Tile ATT(Clone)"){
                    if (numElems > 1) {
                        //Attack the character there?
                        Debug.Log("Attacking");
                        int damage = lastselected.GetComponent<TestStats>().dmg;
                        GameObject Enemy = GameObject.Find(overlapElems[1].name);
                        Enemy.GetComponent<TestStats>().GettingAttacked(damage);
                    }
                    // Destroy tiles at the end 
                    DestroyTiles("Highlighted Tile ATT(Clone)");
                }
                
            }
            else{
                //Remove the highlighted tiles
                DestroyTiles("Highlighted Tile(Clone)");
                DestroyTiles("Highlighted Tile ATT(Clone)");
            }
        }

        //Spawn Menu
        //GameObject.Instantiate(menu, menuspawn, Quaternion.identity);

    }
    void SpawnTiles(int mvnt, Vector3 pos, string tile_type)
    {
        Vector2 TLeftCorn = new Vector2(pos[0] - 0.5F, pos[1] + 0.5F);
        Vector2 BRightCorn = new Vector2(pos[0] + 0.5F, pos[1] - 0.5F);
        Collider2D[] overlapElems = Physics2D.OverlapAreaAll(TLeftCorn, BRightCorn);
        bool found = false;
        tile = GameObject.Find(tile_type); 

        for (int i=0; i<overlapElems.Length; i++){
            if (overlapElems[i].name == tile_type+"(Clone)"){
                found = true;
            }
        }

        if (!found){
            GameObject.Instantiate(tile, pos, Quaternion.identity);
        }
        if (mvnt == 0) { return; }
        else {
            // TODO: bounds checks
            SpawnTiles(mvnt - 1, new Vector3(pos[0] - 1, pos[1], pos[2]), tile_type);
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1] - 1, pos[2]), tile_type);
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1], pos[2] - 1), tile_type);
            SpawnTiles(mvnt - 1, new Vector3(pos[0] + 1, pos[1], pos[2]), tile_type);
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1] + 1, pos[2]), tile_type);
            SpawnTiles(mvnt - 1, new Vector3(pos[0], pos[1], pos[2] + 1), tile_type);
        }
    }

    void DestroyTiles(string tile_type)
    {
        for (int i = 0; i < 10000; i++){
            GameObject temp = GameObject.Find(tile_type);
            GameObject.DestroyImmediate(temp);
        }
    }
}
