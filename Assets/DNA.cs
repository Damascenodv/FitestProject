using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    //gene for color
    public float r;
    public float g;
    public float b;

    public float size;

    bool dead = false;

    public float timeToDie = 0;

    SpriteRenderer spriteRenderer;
    Collider2D collider;

     void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead At:" +  timeToDie);
        spriteRenderer.enabled = false;
        collider.enabled = true;

        
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        spriteRenderer.color = new Color(r, g, b);
        this.transform.localScale = new Vector3(size, size, size);





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
