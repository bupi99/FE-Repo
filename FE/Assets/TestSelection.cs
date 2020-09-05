using System;
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
    public GameObject[] Allies;
    public GameObject[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu Temp");
        tile = GameObject.Find("Highlighted Tile");
        att_tile = GameObject.Find("Highlighted Tile ATT");

        //Create the list of Allies and enemies
        //Maybe tag gameobjects in the future?
        Allies = new GameObject[2] { GameObject.Find("Girl Sword Test"), GameObject.Find("Guy Dagger Test") };
        Enemies = new GameObject[2] { GameObject.Find("Girl Sword Test (1)"), GameObject.Find("Guy Dagger Test (1)") };

        FogReset();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Enemies.Length);
        Collider2D[] overlapElems = FindOverlapElems(new Vector2(transform.position[0], transform.position[1]));
        int numElems = overlapElems.Length;

        if (Input.GetKeyDown(".")){
            if (numElems >= 1){
                string nameElem = overlapElems[0].name;
                bool found = false;
                for (int i=0; i<Allies.Length; i++)
                {
                    if (Allies[i].name == nameElem)
                    {
                        found = true;
                    }
                }
                if (found){
                    if (numElems > 1){
                        if (overlapElems[1].name == "Highlighted Tile(Clone)"){
                            //Remove the highlighted tiles
                            DestroyTiles("Highlighted Tile(Clone)");
                            //Spawn Attack Tiles
                            //Debug.Log("spawning ATT Tiles");
                            SpawnTiles(lastselected.GetComponent<TestStats>().att_range, transform.position, "Highlighted Tile ATT");
                        }
                    }
                    else
                    {
                        // Remove Highlighted Tiles
                        DestroyTiles("Highlighted Tile(Clone)");
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
                    
                    //Reset Fog
                    FogReset();
                    
                    //Spawn Attack Tiles
                    SpawnTiles(lastselected.GetComponent<TestStats>().att_range, transform.position, "Highlighted Tile ATT");
                    
                    //Remove the attacking tile on the character
                    
                    Collider2D[] playertileoverlap = FindOverlapElems(new Vector2(transform.position[0], transform.position[1]));
                    
                    for (int i=0; i<playertileoverlap.Length; i++) { 
                        if (playertileoverlap[i].name == "Highlighted Tile ATT(Clone)"){
                            GameObject.DestroyImmediate(GameObject.Find("Highlighted Tile ATT(Clone)"));
                        }
                    }

                }
                
                if (nameElem == "Highlighted Tile ATT(Clone)"){
                    if (numElems > 1) {
                        //Attack the character there?
                        //Debug.Log("Attacking");
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
        Collider2D[] overlapElems = FindOverlapElems(pos);
        bool found = false;
        tile = GameObject.Find(tile_type);

        // check if tiles already there / for allies
        for (int i = 0; i < overlapElems.Length; i++)
        {
            if (overlapElems[i].name == tile_type + "(Clone)")
            {
                found = true;
            }
            for (int j = 0; j < Allies.Length; j++)
            {
                if (overlapElems[i].name == Allies[j].name)
                {
                    found = true;
                }
            }
        }
        if (!found){
            if (tile_type == "Highlighted Tile")
            {
                GameObject.Instantiate(tile, pos, Quaternion.identity);
            }
            else
            {
                found = false;
                for (int i = 0; i < overlapElems.Length; i++)
                {
                    for (int j = 0; j < Enemies.Length; j++)
                    {
                        if (overlapElems[i].name == Enemies[j].name)
                        {
                            found = true;
                        }
                    }
                }
                if (found)
                {
                    GameObject.Instantiate(tile, pos, Quaternion.identity);
                }
            }
        }
        if (mvnt <= 0) { return; }
        else {
            // TODO: bounds checks
            Vector2 LeftVector = new Vector2(pos[0] - 1, pos[1]);
            Vector2 DownVector = new Vector2(pos[0], pos[1] - 1);
            Vector2 RightVector = new Vector2(pos[0] + 1, pos[1]);
            Vector2 UpVector = new Vector2(pos[0], pos[1] + 1);
            Vector2[] AllVector = new Vector2[] { LeftVector, DownVector, RightVector, UpVector };

            Collider2D[] LeftBlockElems = FindOverlapElems(LeftVector);
            Collider2D[] DownBlockElems = FindOverlapElems(DownVector);
            Collider2D[] RightBlockElems = FindOverlapElems(RightVector);
            Collider2D[] UpBlockElems = FindOverlapElems(UpVector);
            Collider2D[][] AllBlockElems = new Collider2D[][] { LeftBlockElems, DownBlockElems, RightBlockElems, UpBlockElems };
            bool LeftFound = false;
            bool DownFound = false;
            bool RightFound = false;
            bool UpFound = false;
            bool[] AllFound = new bool[] { LeftFound, DownFound, RightFound, UpFound };

            // note if length>=1 then has to be a character but check all elems regardless
            // go through each block and check if theres an character on it
            for (int i=0; i<4; i++) { 
                for (int j=0; j < AllBlockElems[i].Length; j++) { 
                    
                    for (int k=0; k<Enemies.Length; k++)
                    {
                        //Debug.Log(AllBlockElems[i][j].name);
                        if (AllBlockElems[i][j].name == Enemies[k].name)
                        {
                            AllFound[i] = true;
                        }
                    }
                }
            }

            for (int i=0; i<4; i++){
                // if highlighted tile we have someone blocking
                if (tile_type == "Highlighted Tile"){
                    if (!AllFound[i]){
                        SpawnTiles(mvnt - 1, new Vector3(AllVector[i][0], AllVector[i][1], 0), tile_type);
                    }
                }
                // otherwise we display some attacking tiles where they are
                else {
                    SpawnTiles(mvnt - 1, new Vector3(AllVector[i][0], AllVector[i][1], 0), tile_type);
                }
            }
        }
    }

    void DestroyTiles(string tile_type)
    {
        for (int i = 0; i < 10000; i++){
            GameObject temp = GameObject.Find(tile_type);
            GameObject.DestroyImmediate(temp);
        }
    }

    Collider2D[] FindOverlapElems(Vector2 pos)  
    {
        Vector2 TLeftCorn = new Vector2(pos[0] - 0.5F, pos[1] + 0.5F);
        Vector2 BRightCorn = new Vector2(pos[0] + 0.5F, pos[1] - 0.5F);
        return Physics2D.OverlapAreaAll(TLeftCorn, BRightCorn);
    }

    void FogReset()
    {
        GameObject FogTile = GameObject.Find("Fog");
        //Destroy old tiles so no overlap
        DestroyTiles("Fog(Clone)");
        for (int x=-11; x < 11; x++)
        {
            for (int y=-5; y < 5; y++)
            {
                GameObject.Instantiate(FogTile, new Vector3(x + 0.5F, y + 0.5F, 0), Quaternion.identity);
            }
        }

        void FogDelete(Vector2 pos, int vis)
        {
            Collider2D[] overlapElems = FindOverlapElems(pos);
            for (int i=0; i<overlapElems.Length; i++)
            {
                if (overlapElems[i].name == "Fog(Clone)")
                {

                    GameObject.DestroyImmediate(overlapElems[i].gameObject);
                }
            }
            if (vis == 0){return;}
            else
            {
                FogDelete(new Vector2(pos[0] - 1, pos[1]), vis - 1);
                FogDelete(new Vector2(pos[0], pos[1] - 1), vis - 1);
                FogDelete(new Vector2(pos[0] + 1, pos[1]), vis - 1);
                FogDelete(new Vector2(pos[0], pos[1] + 1), vis - 1);
            }
        }
        // Go through ally list and remove fog around them
        GameObject Ally;
        int Ally_sight;

        for (int i=0; i < Allies.Length; i++)
        {

            Ally = Allies[i];
            Ally_sight = Ally.GetComponent<TestStats>().sight;
            FogDelete(Ally.transform.position, Ally_sight);
        }        
    }

}
