using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bucket_move : MonoBehaviour
{



    Rigidbody2D rb;
    public float moveSpeed = 10f;
    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(rb.position);
        
        float screenBound = 0.05f;
        if(screenPosition.x < screenBound || screenPosition.x > (1f - screenBound)){
            //bounds the bucket to the screen
            rb.position = Camera.main.ViewportToWorldPoint(new Vector2(Mathf.Clamp(screenPosition.x, screenBound, (1f - screenBound)), screenPosition.y));
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    

}
