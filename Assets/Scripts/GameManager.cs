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

    // public Text to Access and Update - Hub
    public Text standBtnText;
    private int standClick = 0;
    public Text scoreText;
    public Text dealerScoreText;
    public Text betsText;
    public Text cashText;
    public Text MainText;
    // hiding dealer's 2nd card; 
    public GameObject hideCard;

    // how much is bet
    int pot = 0;

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
        // Reset round, hide text, prep for new hand
        player.ResetHand();
        dealer.ResetHand();
        // Hide deal hand score at the start of deal
        MainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        // shuffle the deck first
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        player.StartHand();
        dealer.StartHand();
        // Update the scores displayed
        scoreText.text = "Hand: " + player.handValue.ToString();
        dealerScoreText.text = "Hand: " + dealer.handValue.ToString();
        // Enable to hide one of dealer's cards
        hideCard.GetComponent<Renderer>().enabled = true;
        // Adjust buttons visibility
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";
        // Set standard pot size
        pot = 40;
        betsText.text = "Bets: $" + pot.ToString();
        player.AdjustMoney(-20);
        cashText.text = "$" + player.getMoney().ToString();
        // check black jack and end the game
        // player black jack, or dealer black jack, or both black jack
        if ( (player.handValue == 21 && dealer.handValue != 21) || 
            (dealer.handValue != 21 && dealer.handValue == 21) ||
            (dealer.handValue == 21 && dealer.handValue == 21) )
        {
            RoundOver();
        }
    }
    private void HitClicked()
    {
        // check if there is still room on the table
        if(player.cardIndex <= 10)
        {
            player.GetCard();
            scoreText.text = "Hand: " + player.handValue.ToString();
            if (player.handValue > 20) 
                RoundOver();
        }
    }
    private void StandClicked()
    {
        standClick++;
        if (standClick > 1)
            RoundOver();
        
        // Dealer turn
        HitDealer();
        standBtnText.text = "Call";
    }

    private void HitDealer()
    {
        // if less than 16, they must hit
        while(dealer.handValue < 16 && dealer.cardIndex < 10)
        {
            dealer.GetCard();
            // update the dealer score
            dealerScoreText.text = "Hand: " + dealer.handValue.ToString();
        }
        RoundOver();
    }

    // Add money to pot if bet clicked
    private void BetClicked()
    {
        Text newBet =  betBtn.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        player.AdjustMoney(-intBet);
        cashText.text = "$" + player.getMoney().ToString();
        pot += (intBet * 2);
        betsText.text = "Bets: $" + pot.ToString();
    }

    // check for winner and loser, hand is over
    void RoundOver()
    {
        // Booleans for bust and black Jack / 21
        bool playerBust = player.handValue > 21;
        bool dealerBust = dealer.handValue > 21;
        bool player21 = player.handValue == 21;
        bool dealer21 = dealer.handValue == 21;

        // If stand has been clicked less than twice, no 21 or busts
        if (standClick < 2 && !playerBust && !dealerBust && !player21 && !dealer21) 
            return;
        bool roundOver = true;
        // check black jack
        // if all bust, bets returned
        if (playerBust && dealerBust)
        {
            MainText.text = "All Bust: Bets returned";
            player.AdjustMoney(pot / 2);

        }
        // if player bust and dealer didn't, or if deal has more points, deal wins
        else if (playerBust || (!dealerBust && dealer.handValue > player.handValue) )
        {
            MainText.text = "Dealer wins!";
        }
        // if dealer bust, player didn't, or player has more points, player wins
        else if(dealerBust || (!playerBust && player.handValue > dealer.handValue) )
        {
            MainText.text = "You wins!";
            player.AdjustMoney(pot);
        }
        // check for tie, return bets
        else if(player.handValue == dealer.handValue) 
        {
            MainText.text = "Push: Bets returned";
            player.AdjustMoney(pot / 2);
        }
        else
        {
            roundOver = false;
        }

        // Set UI up for next move / hand / turn
        if (roundOver)
        {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            MainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + player.getMoney().ToString();
            standClick = 0;
        }
    }

}
