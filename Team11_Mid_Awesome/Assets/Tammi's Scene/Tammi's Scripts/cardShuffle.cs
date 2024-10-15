using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffler : MonoBehaviour
{
    // Reference to all the card objects in the scene
    public GameObject[] cards;  // Holds all the card GameObjects

    // Reference to the slots or positions where the cards will be placed
    public Transform[] cardSlots;

    private void Start()
    {
        ShuffleCards();  // Shuffle cards as soon as the game starts
    }

    public void ShuffleCards()
    {
        // Create a list of indices representing the positions
        List<int> availableSlots = new List<int>();

        for (int i = 0; i < cardSlots.Length; i++)
        {
            availableSlots.Add(i);
        }

        // Shuffle the cards by assigning random positions
        foreach (GameObject card in cards)
        {
            // Pick a random available slot
            int randomIndex = Random.Range(0, availableSlots.Count);
            int slotIndex = availableSlots[randomIndex];

            // Place the card at the corresponding slot position
            card.transform.position = cardSlots[slotIndex].position;

            // Remove the used slot from the list of available slots
            availableSlots.RemoveAt(randomIndex);
        }

        Debug.Log("Cards shuffled at the start!");
    }
}
