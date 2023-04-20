using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Color startColor;
    private Color endColor = new Color(2.55f, 2.55f, 2.55f);
    
    
    // Start is called before the first frame update
    void Start()
    {
        startColor = GetComponent<SpriteRenderer>().color;
        if(this.CompareTag("Target")) FindObjectOfType<GameManager>().totalBoxes ++;
    }
    
    void update()
    {
        RaycastHit2D hit1 = Raycast(transform.position, Vector2.left, 0.8f);
        RaycastHit2D hit2 = Raycast(transform.position, Vector2.right, 0.8f);
        RaycastHit2D hit3 = Raycast(transform.position, Vector2.up, 0.8f);
        RaycastHit2D hit4 = Raycast(transform.position, Vector2.down, 0.8f);
    }
    
    
    public bool CanMove(Vector2 d)
    {
        RaycastHit2D hit = Raycast((Vector2) d*0.4f, d, 0.7f);
        if(!hit || hit.collider.tag=="Target"){
            transform.Translate(d);
            return true;
        }
        else return false;
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Target") || this.CompareTag("Target") )
        {
            GetComponent<SpriteRenderer>().color = endColor;
            FindObjectOfType<GameManager>().doneBoxes++;
            FindObjectOfType<GameManager>().IfDone();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.CompareTag("Target") || this.CompareTag("Target") )
        {
            FindObjectOfType<GameManager>().doneBoxes--;
            GetComponent<SpriteRenderer>().color = startColor;
        }
    }
    
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos+offset, rayDirection * length, color, 0.2f);
        return hit;
    }
}
