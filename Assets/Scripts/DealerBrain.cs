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

    public int cardValue;

    public enum State
    {
        Dealing,
        Play,
        Fold
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Dealing;

        if (currentState == State.Dealing)
        {
            //instantiate two cards for the dealer
            SpawnBeginnerCards();

            
            //if the two cards are equal to 21 then call fold state
            if (cardValue >= 21)
            {
                currentState = State.Fold;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO add a card value that gets current cards and adds the numbers
        cardValue += cardSpawner.currentCard.number;
    }

    void SpawnBeginnerCards()
    {
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
        cardSpawner.deck.RemoveAt(1);
    }

    void Win()
    {
        if (currentState == State.Fold)
        {

        }
    }
}
