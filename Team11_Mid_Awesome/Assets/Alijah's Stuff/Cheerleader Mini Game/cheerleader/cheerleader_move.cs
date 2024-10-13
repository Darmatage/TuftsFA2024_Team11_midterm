using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheerleader_move : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float difficulty;
    public float particleHitAmount;
    float direction = 1f;
    bool isTurningAround = false;
    bool isGoingToTurn = false;
    Coroutine turnAroundCoroutine;

    private float turnsQueded = 0f;

    //LOGIC

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        difficulty = CheerleaderGameVariables.Difficulty;
        moveSpeed = DifficultyLerp(8f, 16f);
        particleHitAmount = 0.005f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //DEBUG VALUES
        difficulty = CheerleaderGameVariables.Difficulty;
        moveSpeed = DifficultyLerp(8f, 16f);

        // Changes direction of cheerleader
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(rb.position);

        if (screenPosition.x < 0 || screenPosition.x > 1)
        {
            // Bounds the cheerleader to move left and right
            rb.position = Camera.main.ViewportToWorldPoint(new Vector2(Mathf.Clamp(screenPosition.x, 0.0f, 1f), screenPosition.y));
            turnAround();
        }

        // Moves the position
        Vector2 movement = new Vector2(direction, 0f);
        rb.MovePosition(rb.position + movement * moveSpeed * fixedTime());

    }

    // Logic for when cheerleader is hit by a particle
    void OnParticleHit()
    {
        CheerleaderGameVariables.Progress = Mathf.Clamp(CheerleaderGameVariables.Progress + particleHitAmount, 0, 1f);
        if(!isGoingToTurn){
            turnAroundCoroutine = StartCoroutine(RandomTurnAround());
        } 
    }

    // Coroutine for turning around cheerleader
    void turnAround()
    {
        if (!isTurningAround)
        {
            StartCoroutine(SmoothTurnAround());
        }
    }

    IEnumerator RandomTurnAround()
    {   
        isGoingToTurn = true;
        float waitTime = Random.Range(1f, 3f) * (1f - difficulty); // Random time based on difficulty
        yield return new WaitForSeconds(waitTime);
        turnAround();
        isGoingToTurn = false;
    }

    IEnumerator SmoothTurnAround()
    {
        isTurningAround = true;
        float duration = 1f; // Duration of the turn-around
        float elapsedTime = 0f;

        float startDirection = direction;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, transform.eulerAngles.y + 180f, 0);

        while (elapsedTime <= duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            direction = Mathf.Lerp(startDirection, -startDirection, elapsedTime / duration);
            elapsedTime += Time.deltaTime * DifficultyLerp(0f, 2f, 1f);
            yield return null;
        }

        isTurningAround = false;
    }

    //HELPER FUNCTIONS
    float fixedTime()
    {
        return Time.fixedDeltaTime;
    }

    float DifficultyLerp(float a, float b)
    {
        return Mathf.Lerp(a, b, difficulty);
    }

    float DifficultyLerp(float a, float b, float start)
    {
        return start + DifficultyLerp(a, b);
    }
}
