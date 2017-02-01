using UnityEngine;
using System.Collections;

public class LeaveTrigger : MonoBehaviour {
    //при взаимодествии с этим игровым объектом создается новый кусок сцены и удаляется старый
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelGenerator.instance.AddPiece();
        LevelGenerator.instance.RemoveOldestPiece();
    }
}
