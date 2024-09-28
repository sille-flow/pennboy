using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Card[] hand;
    public List<int> deck;

    private void Start()
    {
        hand = new Card[5];
        deck = new List<int>();

        generateDeck();

        for (int i = 0; i < hand.Length; i++)
        {
            int cardType = deck[i];
            hand[i] = new Card();
        }
    }

    private void generateDeck()
    {

    }
    
    private Card generateCard(int cardId)
    {
        return new Card();
    }

    /// <summary>
    /// Uses the card with number cardNumber from the deck and puts it back at the bottom of the deck.
    /// </summary>
    /// <param name="cardNumber"></param>
    public void UseCard(int cardNumber)
    {
        Card card = hand[cardNumber];
        hand[cardNumber] = generateCard(deck[0]);
        
    }

    //private static Card[] CardTypes = 
    //{
        
    //}
}
