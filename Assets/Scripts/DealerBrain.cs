using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DealerBrain : MonoBehaviour
{
    public State currentState;
    public CardSpawner cardSpawner;

    public Transform[] dealerCardSpawns;
    //public Transform spawn1;
    //public Transform spawn2;

    //public Transform playerSpawn1;
    //public Transform playerSpawn2;
    //public Transform playerSpawn3;

    //make an array of playercardspawns
    public Transform[] playerCardSpawns;

    public GameObject dealerWinText;
    public GameObject playerWinText;
    public GameObject drawText;
    public GameObject foldButton;

    public float dealerCountDown;

    //public int cardValue;

    public enum State
    {
        Dealing,
        Play,
        Dealer,
        Fold
    }

    public List<PlayingCard> dealerCards;
    public List<PlayingCard> playersCards;


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Dealing);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Dealer)
        {
            if (dealerCountDown > 0)
            {
                dealerCountDown -= Time.deltaTime;
                if (dealerCountDown <= 0)
                {
                    //make it when the card is on the players table make the dealer put a card on their table
                    if (GetTotalDealerValue() < 15)
                    {
                        cardSpawner.SpawnCard();
                        //put card in dealers list
                        dealerCards.Add(cardSpawner.holdingCard);

                        //move it to dealers table
                        cardSpawner.holdingCard.transform.position = NextDealerCardPos();
                    }

                    if (CheckForWin() == false) // this could cause a win/loss if either player is over 21
                        ChangeState(State.Fold); // this will compare totals if nothj <=21
                }
            }
        }
    }

    public void ChangeState(State newState)
    {
        currentState = newState;

        if (currentState == State.Dealing)
        {
            //cardSpawner.SpawnCards();
            //instantiate two cards for the dealer
            SpawnBeginnerCards();


            if (CheckForWin() == false)
            {
                ChangeState(State.Play);
            }
        }
        if (currentState == State.Dealer)
            dealerCountDown = 2.0f;

        if (currentState == State.Fold)
        {
            //evaluate both players to see who wins
            if (GetTotalDealerValue() > GetTotalPlayerValue() && GetTotalDealerValue() <= 21)
                Lose();

            else if (GetTotalPlayerValue() > GetTotalDealerValue() && GetTotalPlayerValue() <= 21)
                Win();

            else if (GetTotalDealerValue() == GetTotalPlayerValue() && GetTotalDealerValue() <= 21)
                Draw();
        }
    }

    public bool CheckForWin()
    {
        if (GetTotalDealerValue() > 21)
        {
            ChangeState(State.Fold);
            Win();
            return true;
        }
        else if (GetTotalPlayerValue() > 21)
        {
            ChangeState(State.Fold);
            Lose();
            return true;
        }
        return false;
    }

    public void SpawnBeginnerCards()
    {
        dealerCards = new List<PlayingCard>();

        //deals two cards to dealer
        cardSpawner.Shuffle();
        GameObject s1 = Instantiate(cardSpawner.card, dealerCardSpawns[0].transform.position, Quaternion.Euler(-87, 271, -86));
        GameObject s2 = Instantiate(cardSpawner.card, dealerCardSpawns[1].transform.position, Quaternion.Euler(-87, 271, -86));

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
        GameObject p1 = Instantiate(cardSpawner.card, playerCardSpawns[0].transform.position, Quaternion.Euler(-87, 271, -86));
        GameObject p2 = Instantiate(cardSpawner.card, playerCardSpawns[1].transform.position, Quaternion.Euler(-87, 271, -86));

        PlayingCard _playingCard1 = p1.GetComponent<PlayingCard>();
        PlayingCard _playingCard2 = p2.GetComponent<PlayingCard>();

        _playingCard1.suit = cardSpawner.deck[0].suit;
        _playingCard1.number = cardSpawner.deck[0].number;

        _playingCard2.suit = cardSpawner.deck[1].suit;
        _playingCard2.number = cardSpawner.deck[1].number;
        cardSpawner.deck.RemoveAt(0);
        cardSpawner.deck.RemoveAt(0);

        playersCards.Add(_playingCard1);
        playersCards.Add(_playingCard2);
    }

    public int GetTotalDealerValue()
    {
        int total = 0;
        foreach (PlayingCard playingCard in dealerCards)
        {
            total += playingCard.GetCardValue();
        }

        return total;
    }

    public int GetTotalPlayerValue()
    {
        int total = 0;
        foreach (PlayingCard playingCard in playersCards)
        {
            total += playingCard.GetCardValue();
        }

        return total;
    }

    void Lose()
    {
        //if dealer gets 21 place text saying dealer wins
        dealerWinText.SetActive(true);
    }

    void Win()
    {
        playerWinText.SetActive(true);
    }

    void Draw()
    {
        drawText.SetActive(true);
    }

    public Vector3 NextPlayerCardPos()
    {
        int nextpos = playersCards.Count - 1;

        //TODO go to spawn 4 once player has spawned 3
        return playerCardSpawns[nextpos].position;
    }
    public Vector3 NextDealerCardPos()
    {
        int nextpos = dealerCards.Count - 1;

        //TODO go to spawn 4 once player has spawned 3
        return dealerCardSpawns[nextpos].position;
    }
}
