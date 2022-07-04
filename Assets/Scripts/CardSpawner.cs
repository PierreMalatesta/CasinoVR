using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardSpawner : MonoBehaviour
{

    public GameObject cardSpawnPoint;
    public GameObject card;

    public bool isEmpty = false;
    private bool isCreated;

    public bool grabCard = false;

    PlayingCard holdingCard;

    public InputActionReference drawCardAction;

    public DealerBrain dealer;

    [System.Serializable]
    public struct Card
    {
        public PlayingCard.Suit suit;
        public int number;
    }

    public List<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
        SpawnCard();
        //this is equivalent to clicking the button and releasing it
        if (drawCardAction != null)
            drawCardAction.action.performed += DrawCard;
        if (drawCardAction != null)
            drawCardAction.action.canceled += ReleaseCard;
    }

    //these two context menus are debug to draw and release card without using vr headset
    [ContextMenu("Draw Card")]
    void DebugDrawCard()
    {
        DrawCard(new InputAction.CallbackContext());
    }

    [ContextMenu("Release Card")]
    void DebugReleaseCard()
    {
        ReleaseCard(new InputAction.CallbackContext());
    }


    public void DrawCard(InputAction.CallbackContext context)
    {
        //if we're in play mode, draw a card
        if (dealer.currentState == DealerBrain.State.Play && grabCard == false)
        {
            SpawnCard();
            grabCard = true;
        }
    }

    // this gets called when you let go of a drawn card
    public void ReleaseCard(InputAction.CallbackContext context)
    {
        if (grabCard == true)
        {
            // put it in players list
            dealer.playersCards.Add(holdingCard);

            // move it to the player's table
            holdingCard.transform.position = dealer.NextCardPos();

            grabCard = false;
        }
    }

    public void Shuffle()
    {
        //makes list of cards and adds all cards and suits in order from clubs to spades
        int index = 0;
        deck = new List<Card>();
        // create an ordered deck
        for (PlayingCard.Suit suit = PlayingCard.Suit.Clubs; suit <= PlayingCard.Suit.Spades; suit++)
        {
            for (int number = 1 ; number <= 13; number++)
            { 
                Card card;
                card.suit = suit;
                card.number = number;
                deck.Add(card);
                index++;
            }
        }

        // we now have an ordered deck. 
        // lets shuffle it
        //this shuffle gets two of the random cards and swaps them and it does this 1000 times
        for (int i = 0; i < 1000; i++)
        {
            int index1 = Random.Range(0, deck.Count);
            int index2 = Random.Range(0, deck.Count);
            Card temp = deck[index1];
            deck[index1] = deck[index2];
            deck[index2] = temp;
        }

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //[ContextMenu("Spawn Card")]
    public void SpawnCard()
    { 
        isEmpty = true;

        if (isEmpty == true)
        {
            //spawn a new card and storing it in a local variable
            GameObject g = Instantiate(card, cardSpawnPoint.transform.position, Quaternion.Euler(-87, 270, -86));
            PlayingCard playingCard = g.GetComponent<PlayingCard>();
            //suit and number is equal to the deck
            playingCard.suit = deck[0].suit;
            playingCard.number = deck[0].number;
            //remove the top card from the deck
            deck.RemoveAt(0);
            if (!cardSpawnPoint)
                isEmpty = false;

            holdingCard = playingCard;
        }

        else if (isEmpty == false)
            isCreated = true;
    }
}
