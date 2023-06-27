using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Game Buttons
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;

    // Access to Player and Dealer objects
    public PlayerScript player;
    public PlayerScript dealer;

    void Start()
    {
        // Adds on click listeners to the buttons
        dealBtn.onClick.AddListener( () => DealClicked());
        hitBtn.onClick.AddListener( () => HitClicked());
        standBtn.onClick.AddListener( () => StandClicked());
        betBtn.onClick.AddListener( () => BetClicked());

    }

    // Deal like start game button
    private void DealClicked()
    {
        //shuffle the deck first
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        player.StartHand();
        dealer.StartHand();
    }
    private void HitClicked()
    {

    }
    private void StandClicked()
    {

    }
    private void BetClicked()
    {
        throw new NotImplementedException();
    }

}
