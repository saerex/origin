using UnityEngine;
using System.Collections;

public class InstAndDestroyBubble : MonoBehaviour {

    public GameObject bubble;
    public GameObject player;

	void Start () {
        StartCoroutine(Inst());
	}
	
	IEnumerator Inst()
    {
        yield return new WaitForSeconds(1f);
        GameObject ob = Instantiate(bubble, gameObject.transform.position, Quaternion.identity) as GameObject;
        
            DeleteObject(ob);
        Repeat();
    }
     void Repeat()
    {
        StartCoroutine(Inst());
    }
    void DeleteObject(GameObject ob)
    {
        
        Destroy(ob, 2f);
    }
}
