using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DealerBrain : MonoBehaviour
{
    public State currentState;
    public CardSpawner cardSpawner;
    public Transform spawn1;
    public Transform spawn2;

    public Transform playerSpawn1;
    public Transform playerSpawn2;



    //public int cardValue;

    public enum State
    {
        Dealing,
        Play,
        Fold
    }

    List<PlayingCard> dealerCards;
    List<PlayingCard> playersCards;


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Dealing);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ChangeState(State newState)
    {
        currentState = newState;

        if (currentState == State.Dealing)
        {

            //instantiate two cards for the dealer
            SpawnBeginnerCards();


            //if the two cards are equal to 21 then call fold state
            if (GetTotalValue() >= 21)
            {
                ChangeState(State.Fold);
            }
            else
            {
                ChangeState(State.Play);
            }
        }
    }

    void SpawnBeginnerCards()
    {
        dealerCards = new List<PlayingCard>();

        //deals two cards to dealer
        cardSpawner.Shuffle();
        GameObject s1 = Instantiate(cardSpawner.card, spawn1.transform.position, Quaternion.Euler(-87, 271, -86));
        GameObject s2 = Instantiate(cardSpawner.card, spawn2.transform.position, Quaternion.Euler(-87, 271, -86));

        PlayingCard playingCard1 = s1.GetComponent<PlayingCard>();
        PlayingCard playingCard2 = s2.GetComponent<PlayingCard>();

        playingCard1.suit = cardSpawner.deck[0].suit;
        playingCard1.number = cardSpawner.deck[0].number;

        playingCard2.suit = cardSpawner.deck[1].suit;
        playingCard2.number = cardSpawner.deck[1].number;
        cardSpawner.deck.RemoveAt(0);
        cardSpawner.deck.RemoveAt(0);

        dealerCards.Add(playingCard1);
        dealerCards.Add(playingCard2);

        //deals two cards to player
        //NOTE this is adding both dealer and player cards which will always be 21
        playersCards = new List<PlayingCard>();
        GameObject p1 = Instantiate(cardSpawner.card, playerSpawn1.transform.position, Quaternion.Euler(-87, 271, -86));
        GameObject p2 = Instantiate(cardSpawner.card, playerSpawn2.transform.position, Quaternion.Euler(-87, 271, -86));

        PlayingCard _playingCard1 = p1.GetComponent<PlayingCard>();
        PlayingCard _playingCard2 = p2.GetComponent<PlayingCard>();

        _playingCard1.suit = cardSpawner.deck[0].suit;
        _playingCard1.number = cardSpawner.deck[0].number;

        _playingCard2.suit = cardSpawner.deck[1].suit;
        _playingCard2.number = cardSpawner.deck[1].number;
        cardSpawner.deck.RemoveAt(0);
        cardSpawner.deck.RemoveAt(0);

        dealerCards.Add(_playingCard1);
        dealerCards.Add(_playingCard2);

        cardSpawner.SpawnCards();
    }

    public int GetTotalValue()
    {
        int total = 0;
        foreach(PlayingCard playingCard in dealerCards)
        {
            total += playingCard.GetCardValue();
        }

        return total;
    }

    void Win()
    {
        if (currentState == State.Fold)
        {

        }
    }
}
