using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    // value of card, 5 of clubs = 5, etc
    public int valueOfCard = 0;
    
    public int GetValueOfCard()
    {
        return valueOfCard;
    }

    public void SetValueOfCard(int value)
    {
        valueOfCard = value; 
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetSprite(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        valueOfCard = 0;
    }
}
