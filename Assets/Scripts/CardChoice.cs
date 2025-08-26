using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardChoice : MonoBehaviour
{
    // Hey Musashi! Only switch to the Card screen when XP reaches the threshold and you Level Up.
    // Use CardChoice to display and manage card selection
    // Call RenderCard() when you need to display the card selection!
    // If you need it in another script just make it public

    Card card; // Placeholder for card data
    [SerializeField] List<Card> cardPool;
    [SerializeField] Button chooseButton;
    [SerializeField] TextMeshProUGUI cardNameText;
    [SerializeField] TextMeshProUGUI cardSubtitleText;
    [SerializeField] TextMeshProUGUI cardDescriptionText;
    [SerializeField] Image cardImage;
    string cardType;
    bool hasRerolled;

    void Awake()
    {
        // chooseButton = GetComponentInChildren<Button>();
        // cardNameText = GetComponentInChildren<TextMeshProUGUI>();
        // cardSubtitleText = GetComponentInChildren<TextMeshProUGUI>();
        // cardDescriptionText = GetComponentInChildren<TextMeshProUGUI>();
        // cardImage = GetComponentInChildren<Image>();
    }

    void Start()
    {
        chooseButton.onClick.AddListener(OnChooseButtonClicked);
        RenderCard(SelectRandomCard());
    }

    void RenderCard(Card card)
    {
        cardNameText.text = card.GetCardName;
        cardSubtitleText.text = card.GetCardSubtitle;
        cardDescriptionText.text = card.GetCardDescription;
        cardImage.sprite = card.GetCardImage;
        cardType = card.GetCardType;

        PlayCardAnimation();
        Debug.Log("Card Rendered: " + card.GetCardName);
    }

    void OnChooseButtonClicked()
    {
        if (cardType == "Buff")
        {
            // TODO: Apply buff
            Debug.Log("Buff card chosen: " + card.GetCardName);
            // TODO: Switch to Manufacturing scene with a scene transition
        }
        else if (cardType == "Module")
        {
            // TODO: Add module to module list
            Debug.Log("Module card chosen: " + card.GetCardName);
            // TODO: Switch to Manufacturing scene with a scene transition
        }
    }

    void PlayCardAnimation()
    {
        // eventually implement card animation
        // pop card in & then play a flashing animation between all the different cards (we can fake it if its too hard)
    }

    public void RerollCard()
    {
        if (!hasRerolled)
        {
            RenderCard(SelectRandomCard());
            hasRerolled = true;
        }
        else
        {
            Debug.Log("You can only reroll once!");
            Debug.Log("TODO: Show message to user and update this button's text.");
        }
    }

    Card SelectRandomCard()
    {
        int randomIndex = Random.Range(0, cardPool.Count);
        card = cardPool[randomIndex];
        return card;
    }
}
