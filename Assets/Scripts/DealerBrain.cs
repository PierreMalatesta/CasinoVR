using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DealerBrain : MonoBehaviour
{
    public State currentState;
    CardSpawner cardSpawner;

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
            cardSpawner.SpawnCards();

            //if the two cards are equal to 21 then call fold state
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
