using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class CardFlip : MonoBehaviour
{
    public float flipSpeed = 5f;
    private bool isFlipping = false;
    private bool isFaceUp = false;
    public string cardID;  // Unique identifier for each card (set this in the Inspector)

    private Quaternion targetRotation;

    // Static list to keep track of flipped cards (shared across all instances)
    private static List<CardFlip> flippedCards = new List<CardFlip>();

    // Static counter to track matched cards
    private static int matchedPairs = 0;

    // Reference to the "Puzzle Completed" text
    public TMPro.TextMeshProUGUI puzzleCompletedText;  // TextMeshPro version for UI text

    // Total number of card pairs (set this in the Inspector or initialize it)
    public int totalPairs = 3;

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

            // Check if the player is clicking on this card and it's not already flipping
            if (Physics.Raycast(ray, out hit) && hit.transform.IsChildOf(transform) && !isFlipping)
            {
                // Only allow flipping if less than 2 cards are currently flipped
                if (flippedCards.Count < 2)
                {
                    Debug.Log("Card clicked, starting flip!");
                    StartCoroutine(FlipCard());
                }
            }
        }
    }

    private IEnumerator FlipCard()
    {
        isFlipping = true;

        // Flip to face-up position
        yield return StartCoroutine(FlipToState(true));

        // Add this card to the list of flipped cards
        flippedCards.Add(this);

        // Check if two cards are flipped
        if (flippedCards.Count == 2)
        {
            // Wait for 1 second to show the cards before checking if they match
            yield return new WaitForSeconds(1f);

            // Check if the two cards match
            if (flippedCards[0].cardID == flippedCards[1].cardID)
            {
                Debug.Log("Cards match! Keeping them flipped.");

                // Clear the flipped cards list without flipping them back
                flippedCards.Clear();

                // Increment the matched pairs counter
                matchedPairs++;

                // Check if all pairs are matched
                if (matchedPairs == totalPairs)
                {
                    Debug.Log("Puzzle Completed!");
                    ShowPuzzleCompletedText();
                    // Load the main scene after a short delay
                    yield return new WaitForSeconds(2f);
                    SceneManager.LoadScene("ClassroomBook");  // Replace "MainScene" with the actual scene name
                }
            }
            else
            {
                Debug.Log("Cards do not match! Flipping them back.");

                // Flip both cards back after showing them
                yield return StartCoroutine(flippedCards[0].FlipToState(false));
                yield return StartCoroutine(flippedCards[1].FlipToState(false));

                // Clear the flipped cards list
                flippedCards.Clear();
            }
        }

        isFlipping = false;
    }

    private IEnumerator FlipToState(bool flipToFaceUp)
    {
        float flipProgress = 0f;
        float totalFlipAngle = flipToFaceUp ? 180f : -180f;  // 180° for face-up, -180° to go back to face-down

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, totalFlipAngle, 0f);

        // Smoothly rotate the card to the target rotation
        while (flipProgress < 1f)
        {
            flipProgress += Time.deltaTime * flipSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, flipProgress);
            yield return null;
        }

        isFaceUp = flipToFaceUp;
    }

    private void ShowPuzzleCompletedText()
    {
        if (puzzleCompletedText != null)
        {
            // Enable or display the "Puzzle Completed" text
            puzzleCompletedText.gameObject.SetActive(true);  // This makes the text visible
        }
    }
}
