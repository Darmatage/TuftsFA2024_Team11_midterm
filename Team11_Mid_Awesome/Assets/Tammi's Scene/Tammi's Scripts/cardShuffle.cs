using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CardShuffler : MonoBehaviour
{
    // Reference to all the card objects in the scene
    public Transform[] cardSlots;  // Holds the positions where the cards will be placed
    public GameObject[] cards;     // Holds all the card GameObjects

    // Function to shuffle the cards
    public void ShuffleCards()
    {
        // Create a list of indices representing the positions
        System.Random random = new System.Random();
        for (int i = 0; i < cards.Length; i++)
        {
            // Pick a random index to swap with
            int randomIndex = random.Next(i, cards.Length);

            // Swap the current card with the randomly chosen card
            Vector3 tempPosition = cards[i].transform.position;
            cards[i].transform.position = cards[randomIndex].transform.position;
            cards[randomIndex].transform.position = tempPosition;
        }

        Debug.Log("Cards shuffled!");
    }
}

