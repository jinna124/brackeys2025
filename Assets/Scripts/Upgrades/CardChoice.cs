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
    // Auto-assigned by sibling index: 0 = Weapon, 1 = Buff, 2 = Module
    Card.CardType assignedCardType;

    // Auto-loaded from Resources/Cards based on CardType
    List<Card> weaponCards;
    List<Card> buffCards;
    List<Card> moduleCards;

    [Header("UI References")]
    [SerializeField] Button chooseButton;
    [SerializeField] TextMeshProUGUI cardNameText;
    [SerializeField] TextMeshProUGUI cardSubtitleText;
    [SerializeField] TextMeshProUGUI cardDescriptionText;
    [SerializeField] Image cardImage;
    [SerializeField] Image panelImage;
    [SerializeField] Image cardFrame;
    Card.CardType cardType;
    Card.CardType currentRollType;
    bool hasRerolled;
    [SerializeField] TextMeshProUGUI rerollText;

    SceneSwitcher sceneSwitcher;
    UpgradeHandler upgradeHandler;
    void Awake()
    {
        AssignCardType();
        LoadCardPools();
    }

    void AssignCardType()
    {
        int index = transform.GetSiblingIndex();
        switch (index)
        {
            case 0: assignedCardType = Card.CardType.Weapon; break;
            case 1: assignedCardType = Card.CardType.Buff; break;
            case 2: assignedCardType = Card.CardType.Module; break;
            default: assignedCardType = Card.CardType.Weapon; break;
        }
        Debug.Log($"CardChoice slot {index} assigned to {assignedCardType}");
    }

    void LoadCardPools()
    {
        weaponCards = new List<Card>();
        buffCards = new List<Card>();
        moduleCards = new List<Card>();

        Card[] allCards = Resources.LoadAll<Card>("Cards");
        foreach (Card c in allCards)
        {
            switch (c.GetCardType)
            {
                case Card.CardType.Weapon: weaponCards.Add(c); break;
                case Card.CardType.Buff:   buffCards.Add(c); break;
                case Card.CardType.Module: moduleCards.Add(c); break;
            }
        }
        Debug.Log($"Card pools loaded: {weaponCards.Count} weapons, {buffCards.Count} buffs, {moduleCards.Count} modules");
    }


    public void Start()
    {
        upgradeHandler = UpgradeHandler.instance;
        sceneSwitcher = SceneSwitcher.instance;
        Debug.Log("Upgrade Handler: " + upgradeHandler);
        RenderCard(SelectRandomCard(assignedCardType));
        ResetReroll();
    }

    public void RollNewCard() { RenderCard(SelectRandomCard(assignedCardType)); ResetReroll(); }

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

    public void OnChooseButtonClicked()
    {
        if (cardType == Card.CardType.Buff)
        {
            Debug.Log("Buff card chosen: " + card.GetCardName);
            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            Debug.Log("Upgrade Handler: " + upgradeHandler);
            upgradeHandler.AddBuff(card.GetCardName);
            sceneSwitcher.LoadModuleShop();
        }
        else if (cardType == Card.CardType.Module)
        {
            // TODO: Add module to module list
            Debug.Log("Module card chosen: " + card.GetCardName);
            upgradeHandler.AddModule(card.GetPrefab);
            Debug.Log("Upgrade Handler: " + upgradeHandler);
            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            sceneSwitcher.LoadModuleShop();
        }
        else if (cardType == Card.CardType.Weapon)
        {
            Debug.Log("Weapon card chosen: " + card.GetCardName);
            Debug.Log("Upgrade Handler: " + upgradeHandler);
            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            Debug.Log("Weapon card name: " + card.GetCardName);
            upgradeHandler.AddWeapon(card.GetCardName);
            sceneSwitcher.LoadModuleShop();
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
            RenderCard(SelectRandomCard(assignedCardType));
            hasRerolled = true;
            rerollText.text = "Reroll (x0)";
        }
        else
        {
            Debug.Log("You can only reroll once!");
        }
    }

    public void ResetReroll()
    {
        hasRerolled = false;
        rerollText.text = "Reroll (x1)";
    }
    Card SelectRandomCard(Card.CardType rollType)
    {
        List<Card> pool = GetCardPool(rollType);
        if (pool == null || pool.Count == 0)
        {
            Debug.LogWarning("No cards available for roll type: " + rollType);
            return null;
        }

        int randomIndex = Random.Range(0, pool.Count);
        card = pool[randomIndex];
        currentRollType = rollType;
        return card;
    }
    Card SelectRandomCard()
    {
        Card.CardType rollType = GetRandomRollType();
        return SelectRandomCard(rollType);
    }

    List<Card> GetCardPool(Card.CardType rollType)
    {
        switch (rollType)
        {
            case Card.CardType.Weapon: return weaponCards;
            case Card.CardType.Buff:   return buffCards;
            case Card.CardType.Module: return moduleCards;
            default:
                Debug.LogWarning("No pool defined for card type: " + rollType);
                return null;
        }
    }

    Card.CardType GetRandomRollType()
    {
        Card.CardType[] availableTypes = { Card.CardType.Weapon, Card.CardType.Buff, Card.CardType.Module };
        return availableTypes[Random.Range(0, availableTypes.Length)];
    }
}
