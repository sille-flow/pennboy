using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Card[] hand;
    [SerializeField] private List<int> deck;
    [SerializeField] private Card[] CardTypes;
    private bool[] isUsingCard;

    private void Start()
    {
        hand = new Card[5];
        deck = new List<int>();
        isUsingCard = new bool[5];

        generateDeck();
    }

    private void generateDeck()
    {
        // actually generate deck
        List<DeckPair> deckCopy = new List<DeckPair>(deckBreakupByIndex);
        while (deckCopy.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, deckCopy.Count);
            deck.Add(deckCopy[index].cardId);
            deckCopy[index].count--;
            if (deckCopy[index].count == 0)
                deckCopy.RemoveAt(index);
        }

        // Generate hand
        for (int i = 0; i < hand.Length; i++)
        {
            int cardType = deck[0];
            deck.RemoveAt(0);
            hand[i] = generateCard(i);
        }
    }
    
    private Card generateCard(int cardId)
    {
        return null;
    }

    /// <summary>
    /// Removes the given card from the hand and generates a new card in its place.  
    /// </summary>
    /// <param name="card"></param>
    public void removeCardFromHand(Card card)
    {
        for (int i = 0; i < 5; i++)
        {
            if (hand[i] == card)
            {
                hand[i] = generateCard(deck[0]);
                deck.RemoveAt(0);
                isUsingCard[i] = false;
                deck.Add(card.id);
                Destroy(card);
                return;
            }
        }
    }

    /// <summary>
    /// Uses the card with number cardNumber from the current hand. If already in use, does nothing.
    /// </summary>
    /// <param name="cardNumber"></param>
    public void UseCard(int cardNumber)
    {
        if (isUsingCard[cardNumber]) return;
        isUsingCard[cardNumber] = hand[cardNumber].Use();
    }

    private class DeckPair
    {
        public int count;
        public int cardId;
        public DeckPair(int count, int cardId)
        {
            this.count = count;
            this.cardId = cardId;
        }

        public override string ToString()
        {
            return "CardId: " + cardId + "  Count: " + count;
        }
    }
    // Breakup of deck by index, where (3,0) represents 3 cards of id 0
    private static DeckPair[] deckBreakupByIndex =
    {
        new DeckPair (3, 0),
        new DeckPair (2, 1),
        new DeckPair (4, 2),
        new DeckPair (1, 3)
    };
}
