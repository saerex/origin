using UnityEngine;
using System.Collections;

public class BubbleTriger : MonoBehaviour {

    public static BubbleTriger instance;
    public GameObject bubble;
    public float speedOfBubble;
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelection;
    [HideInInspector]
    public bool stopBubbleMove;

    private void Awake()
    {
        stopBubbleMove = false;
        instance = this;
    }
    private void Start()
    {
        currentPoint = points[pointSelection];
        
    }
    private void Update()
    {
        try
        {
            if (GameManager.instance.currentGameState == GameState.inGame)
            {
                if (bubble != null)
                    bubble.transform.position = Vector3.MoveTowards(bubble.transform.position, currentPoint.position, Time.deltaTime * speedOfBubble);
            }
        }
        catch (MissingReferenceException)
        {
            bubble = null;
        }
        
             
    }
}

