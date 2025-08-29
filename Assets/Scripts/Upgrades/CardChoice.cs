using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
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
    [SerializeField] Image panelImage;
    [SerializeField] Image cardFrame;
    Card.CardType cardType;
    bool hasRerolled;
    [SerializeField] TextMeshProUGUI rerollText;

    SceneSwitcher sceneSwitcher;
    UpgradeHandler upgradeHandler;

    void Awake()
    {
        // chooseButton = GetComponentInChildren<Button>();
        // cardNameText = GetComponentInChildren<TextMeshProUGUI>();
        // cardSubtitleText = GetComponentInChildren<TextMeshProUGUI>();
        // cardDescriptionText = GetComponentInChildren<TextMeshProUGUI>();
        // cardImage = GetComponentInChildren<Image>();

        upgradeHandler = FindAnyObjectByType<UpgradeHandler>();
        sceneSwitcher = FindAnyObjectByType<SceneSwitcher>();
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
        cardFrame.sprite = card.GetCardFrame;
        panelImage.sprite = card.GetPanelImage;


        PlayCardAnimation();
        Debug.Log("Card Rendered: " + card.GetCardName);
    }

    void OnChooseButtonClicked()
    {
        if (cardType == Card.CardType.Buff)
        {
            // TODO: Apply buff
            Debug.Log("Buff card chosen: " + card.GetCardName);
            upgradeHandler.AddBuff(card.GetPrefab);
            sceneSwitcher.LoadCombatScene();
        }
        else if (cardType == Card.CardType.Module)
        {
            // TODO: Add module to module list
            Debug.Log("Module card chosen: " + card.GetCardName);
            upgradeHandler.AddModule(card.GetPrefab);
            sceneSwitcher.LoadCombatScene();
            // TODO: Switch to Manufacturing scene with a scene transition
        }
        else if (cardType == Card.CardType.Weapon)
        {
            // TODO: Handle weapon card
            Debug.Log("Weapon card chosen: " + card.GetCardName);
            // TODO: Switch to appropriate scene with a scene transition
            upgradeHandler.AddWeapon(card.GetPrefab);
            sceneSwitcher.LoadCombatScene();
        }
        else if (cardType == Card.CardType.Upgrade)
        {
            // TODO: Handle upgrade card
            Debug.Log("Upgrade card chosen: " + card.GetCardName);
            Debug.Log("Create Upgrade Weapon Card handling");
            sceneSwitcher.LoadCombatScene();
            // TODO: Switch to appropriate scene with a scene transition
        }
        else if (cardType == Card.CardType.Gacha)
        {
            // TODO: Handle module card
            Debug.Log("Gacha card chosen: " + card.GetCardName);

        }
        else
        {
            Debug.Log("Unknown or null card type chosen: " + card.GetCardName);
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
            rerollText.text = "Reroll (x0)";
        }
    }

    Card SelectRandomCard()
    {
        int randomIndex = Random.Range(0, cardPool.Count);
        card = cardPool[randomIndex];
        return card;
    }
}
