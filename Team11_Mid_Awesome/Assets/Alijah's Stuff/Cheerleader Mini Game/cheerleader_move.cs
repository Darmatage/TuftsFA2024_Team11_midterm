using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheerleader_move : MonoBehaviour
{

    Rigidbody2D rb;
    public float moveSpeed;
    public float difficulty;
    float direction = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 10f;
        difficulty = 1f;
    }

    // Update is called once per frame
    void Update() {
        //changes direction of cheerleader
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(rb.position);

        if(screenPosition.x < 0 || screenPosition.x > 1){
            //bounds the chearleader to move left and right
            rb.position = Camera.main.ViewportToWorldPoint(new Vector2(Mathf.Clamp(screenPosition.x, 0.0f, 1f), screenPosition.y));
            turnAround();
        }

        float screenBound = 0.1f;
        float screenRotLerp = 1f;
        if(screenPosition.x < screenBound){
            screenRotLerp = screenPosition.x/screenBound;
        }else if(screenPosition.x > (1f - screenBound)){
            screenRotLerp = (1f - screenPosition.x) / screenBound;
        }
        transform.localRotation = Quaternion.Euler(0, (90f * (direction * screenRotLerp) - 90f), 0f);

        //moves the position
        Vector2 movement = new Vector2(direction, 0f);

        Debug.Log(rb.position + " : " + screenPosition + " -> " + Input.GetAxisRaw("Horizontal"));

        rb.MovePosition(rb.position + movement * moveSpeed * fixedTime());
        
        
    }

    void turnAround(){
        direction = direction * -1;
    }
    


    float fixedTime(){
        return Time.fixedDeltaTime;
    }

}
