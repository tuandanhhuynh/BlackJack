using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;

    void Start()
    {
        getCardValues();
    }

    void getCardValues()
    {
        int num = 0;
        // loop to aside all values to the cards
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            // King, Queen, Jack, Ace count 10
            if(num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
    }

    public void shuffle()
    {
        // array data swapping
        for(int i = cardSprites.Length - 1; i > 0; i--)
        {
            //get random position
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * (cardSprites.Length - 1)) + 1;
            
            // swap position for Sprites
            Sprite spriteHolder = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = spriteHolder;

            // swap position for values
            int holder = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = holder;
        }
        currentIndex = 1;
    }

    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValueOfCard(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
