using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : MonoBehaviour
{
    public MeshRenderer frontSide;
    Material cardMaterial;
    public CardSet cardSet;

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
        cardMaterial = GetComponent<Material>();
    }

    void UpdateImage()
    {

        // query our scriptable object for the correct texture
        Texture texture = cardSet.GetCardTexture(suit, number);

        // set the texture
        frontSide.material.SetTexture("_BaseMap", texture);
    }

    public int GetCardValue()
    {
        //dealing with jack, queen and king
        if (number > 10)
            return 10;

        //TODO ace sometimes equals 1
        if (number == 1)
            return 11;
        
        return number;
    }
}
