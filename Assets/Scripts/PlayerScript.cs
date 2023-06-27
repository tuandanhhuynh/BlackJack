using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // -- Player Script attachs to Dealer and Player
    public CardScript card;
    public DeckScript deck;
    // total value of player/dealer's hand
    public int handValue = 0;
    private int money = 1000;
    // Array of card objects on table
    public GameObject[] hand;
    // Index of next card to be turned over
    public int cardIndex = 0;
    // Tracking ace for 1 or 11
    List<CardScript> aceList = new List<CardScript>();

    public void StartHand()
    {
        // add 2 cards to the hand when the game start
        GetCard();
        GetCard();
    }

    // Add a card to the player/dealer's hand
    // return hand value after added card's value.
    public int GetCard()
    {
        //Get a Card, use deal card to assign sprite and value to card on table
        int cardValue = deck.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Add card value to running total of the hand
        handValue += cardValue;
        // check if that card is Ace or not (Ace == 1)
        if(cardValue == 1)
        {
            // add it to ace List
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        // Check if we should use 11 or 1 for Ace card
        AceCheck();
        cardIndex++;
        return handValue;
    }

    public int getMoney()
    {
        return money;
    }
    //Add or subtract from money
    public void AdjustMoney(int amount)
    {
        money += amount;
    }
    // check if ace needed for conversions, 1 or 11 or vice versa 
    public void AceCheck()
    {
        foreach(CardScript ace in aceList)
        {
            if(handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
                ace.SetValueOfCard(11);
                handValue += 10;
            } else if(handValue > 21 && ace.GetValueOfCard() == 11)
            {
                ace.SetValueOfCard(1);
                handValue -= 10;
            }
        }
    }

    // Hides all cards, resets the needed variables
    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<Renderer>().enabled = false;
            hand[i].GetComponent<CardScript>().ResetCard();
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
