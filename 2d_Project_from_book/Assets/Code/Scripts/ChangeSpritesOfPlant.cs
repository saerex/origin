using UnityEngine;
using System.Collections;

public class ChangeSpritesOfPlant : MonoBehaviour {

    public Sprite sprite1; 
    public Sprite sprite2;

    float timer = 1f;
    float delay = 1f;
    float speedOfAnimation = 3f;

    private void Update()
    {
        timer -= Time.deltaTime * speedOfAnimation;

        if(timer <= 0)
        {
            if(this.gameObject.GetComponent<SpriteRenderer>().sprite == sprite1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                timer = delay;
                return;
            }
            else if (this.gameObject.GetComponent<SpriteRenderer>().sprite == sprite2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
                timer = delay;
                return;
            }
        }
    }
}
