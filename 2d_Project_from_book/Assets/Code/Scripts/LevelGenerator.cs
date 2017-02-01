using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator instance;
    //will store all different level pieces that we want the generator to copy from
    public List<LevelPiece> levelPrefabs = new List<LevelPiece>();
    // position of the very first level piece in the level
    public Transform levelStartPoint;
    // store all level pieces that are in the game at the time
    public List<LevelPiece> pieces = new List<LevelPiece>(); 

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GenerateInitialPieces();
    }
    public void GenerateInitialPieces()
    {
        for(int i = 0; i < 2; i++)
        {
            AddStartPieces(i);
        }
    }

    public void AddPiece()
    {
        int randomIndex = Random.Range(0, levelPrefabs.Count);// выбрать случайное число
        // создать копию случайного levelPrefab
        LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[randomIndex]);
        piece.transform.SetParent(this.transform, false);

        Vector3 spawnPosition = Vector3.zero;

        //position
        if(pieces.Count == 0)
        {
            spawnPosition = levelStartPoint.position; // first piece
        }
        else
        {
            //take exit point from last piece as a spawn point to new piece
            spawnPosition = pieces[pieces.Count - 1].exitPoint.position;
        }

        piece.transform.position = spawnPosition;
        pieces.Add(piece);
    }

    public void AddStartPieces(int n)
    {
        
        // создать копию  levelPrefab
        LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[n]);
        
        piece.transform.SetParent(this.transform, false);
        
        Vector3 spawnPosition = Vector3.zero;

        //position
        if (pieces.Count == 0)
        {
            spawnPosition = levelStartPoint.position; // first piece
        }
        else
        {
            //take exit point from last piece as a spawn point to new piece
            spawnPosition = pieces[0].exitPoint.position;
        }


        piece.transform.position = spawnPosition;
        pieces.Add(piece);



    }
    //удаление старого куска сцены, чтобы не накапливались
    public void RemoveOldestPiece()
    {
        LevelPiece oldestPiece = pieces[0];
        pieces.Remove(oldestPiece);
        Destroy(oldestPiece.gameObject);
    }
    public void RemoveAllPieces()
    {
        foreach(LevelPiece piece in pieces)
        {
            Destroy(piece.gameObject);
        }
    } 
}
