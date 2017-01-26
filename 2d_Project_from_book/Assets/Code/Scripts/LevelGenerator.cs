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
}
