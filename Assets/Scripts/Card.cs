using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] string cardName;
    [SerializeField] string cardSubtitle;
    [SerializeField] [TextArea(10, 100)] string cardDescription;
    [SerializeField] Sprite cardImage;
    [SerializeField] string cardType;

    public string GetCardName { get { return cardName; } }
    public string GetCardSubtitle { get { return cardSubtitle; } }
    public string GetCardDescription { get { return cardDescription; } }
    public Sprite GetCardImage { get { return cardImage; } }
    public string GetCardType { get { return cardType; } }

}
