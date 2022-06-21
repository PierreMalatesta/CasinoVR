using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : MonoBehaviour
{

    public enum Suit
    {
        Clubs, 
        Diamonds, 
        Hearts, 
        Spades
    }

    public Suit suit;
    public int number;

    // Start is called before the first frame update
    void Start()
    {
        UpdateImage();
    }

    void UpdateImage()
    {
        // get the meshrenderer for the front quad

        // query our scriptable obvject for the correct texture

        // set the texture
    }
}
