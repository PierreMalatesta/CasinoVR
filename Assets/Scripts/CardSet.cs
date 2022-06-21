using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSet", menuName = "CardSet/CardSet", order = 1)]
public class CardSet : ScriptableObject
{
    //get card prefab
    public GameObject cardPrefab;

    //make array for all suits
    public Texture[] diamonds;
    public Texture[] hearts;
    public Texture[] clubs;
    public Texture[] spades;

    public Texture GetCardTexture(PlayingCard.Suit suit, int number)
    {
        //this is so if the number is not within the array of number cards ace to 13(queen) then we return null
        if (number <= 0 || number >= 14)
            return null;  

        //switching suit and returning the respected suit with its number 
        switch (suit)
        {
            case PlayingCard.Suit.Clubs:
                return clubs[number-1];
                break;

            case PlayingCard.Suit.Spades:
                return spades[number - 1];
                break;

            case PlayingCard.Suit.Diamonds:
                return diamonds[number - 1];
                break;

            case PlayingCard.Suit.Hearts:
                return hearts[number - 1];
                break;
        }

        return null;

    }

}
