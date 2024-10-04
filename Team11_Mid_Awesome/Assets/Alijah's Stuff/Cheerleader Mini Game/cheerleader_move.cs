using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheerleader_move : MonoBehaviour
{

    Rigidbody2D rb;
    float moveSpeed = 5f;
    float direction = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //changes direction of cheerleader
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
    
        if(screenPosition.x < 0 || screenPosition.x > 1){
            turnAround();
        }
        transform.localRotation = Quaternion.Euler(0, (90f * direction - 90f), 0f);

        //moves the position
        Vector2 movement = new Vector2(direction, 0f);
        rb.MovePosition(rb.position + movement * moveSpeed * fixedTime());
        
    }

    void turnAround(){
        direction = direction * -1;
        //Debug.Log(screenPosition.x);
    }
    


    float fixedTime(){
        return Time.fixedDeltaTime;
    }

}
