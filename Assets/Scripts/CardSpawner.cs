using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{

    public GameObject cardSpawnPoint;
    public GameObject card;

    private bool isEmpty = true;
    private bool isCreated;

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
    }

    private void OnMouseDown()
    {
        SpawnCards();
    }

    void Shuffle(/*List<Card> deck*/)
    {
        //makes list of cards and adds all cards and suits in order from clubs to spades
        int index = 0;
        deck = new List<Card>();
        //TODO Shuffle the suits
        for (PlayingCard.Suit suit = (PlayingCard.Suit)Random.Range(0, deck.Count); suit <= PlayingCard.Suit.Spades; suit++)
        {

            //shuffle the number of the card
            for (int number = Random.Range(0, 13); number <= 13; number++)
            { 
                Card card;
                card.suit = suit;
                card.number = number;
                deck.Add(card);
                index++;

                //TODO Shuffle the deck
                //for (int i = deck.Count - 1; i < 0; i++)
                //{
                //    int randomCard = UnityEngine.Random.Range(0, i);
                //    Card temp = deck[i];

                //    deck[i] = deck[randomCard];
                //    deck[randomCard] = temp;
                //}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Spawn Card")]
    private void SpawnCards()
    {
        isEmpty = false; 

        if (isEmpty == false)
        {
            //spawn a new card and storing it in a local variable
            GameObject g = Instantiate(card, cardSpawnPoint.transform.position, Quaternion.Euler(-87, 270, -86));
            PlayingCard playingCard = g.GetComponent<PlayingCard>();
            //suit and number is equal to the deck
            playingCard.suit = deck[0].suit;
            playingCard.number = deck[0].number;
            //remove the top card from the deck
            deck.RemoveAt(0);
            isEmpty = true;
        }

        else if (isEmpty == true)
            isCreated = false;

    }    
}
