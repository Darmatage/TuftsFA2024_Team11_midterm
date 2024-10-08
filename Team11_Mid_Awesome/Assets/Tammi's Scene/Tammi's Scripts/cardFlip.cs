using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardFlip : MonoBehaviour
{
    public float flipSpeed = 5f; 
    private bool isFlipping = false; 
    private bool isFaceUp = false;

    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);  // Logs the object the ray hits
        }

        if (Physics.Raycast(ray, out hit) && hit.transform.IsChildOf(transform) && !isFlipping)
        {
            Debug.Log("Card clicked, starting flip!");
            StartCoroutine(FlipCard());
        }
    }
}
    private IEnumerator FlipCard()
{
    Debug.Log("FlipCard coroutine started!");  
    isFlipping = true;
    float flipProgress = 0f;
    float totalFlipAngle = 180f;

    Quaternion startRotation = transform.rotation;
    Quaternion endRotation = startRotation * Quaternion.Euler(0f, totalFlipAngle, 0f);

    while (flipProgress < 1f)
    {
        flipProgress += Time.deltaTime * flipSpeed;
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, flipProgress);
        yield return null;
    }

    isFaceUp = !isFaceUp;
    isFlipping = false;
}
}
