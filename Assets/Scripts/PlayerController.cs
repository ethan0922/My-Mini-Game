using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    public LayerMask detectLayer;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection = Vector2.right;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection = Vector2.left;
        if(Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection = Vector2.up;
        if(Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection = Vector2.down;
        if(moveDirection != Vector2.zero && CanMove(moveDirection)){
            transform.Translate(moveDirection);
        }
        moveDirection = Vector2.zero;
    }
    
    
    private bool CanMove(Vector2 d)
    {
        RaycastHit2D hit = Raycast(Vector2.zero, d, 1.0f, detectLayer);
        if(!hit) return true;
        if(hit && hit.collider.GetComponent<Box>() != null) return hit.collider.GetComponent<Box>().CanMove(d); 
        else return false;
    }


    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos+offset, rayDirection * length, color, 0.2f);
        return hit;
    }
}