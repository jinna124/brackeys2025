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


    [Header("Player scripts")]
    [SerializeField] GameObject player;
    void Awake()
    {
        // chooseButton = GetComponentInChildren<Button>();
        // cardNameText = GetComponentInChildren<TextMeshProUGUI>();
        // cardSubtitleText = GetComponentInChildren<TextMeshProUGUI>();
        // cardDescriptionText = GetComponentInChildren<TextMeshProUGUI>();
        // cardImage = GetComponentInChildren<Image>();
    }


    public void Start()
    {
        upgradeHandler = UpgradeHandler.instance;
        sceneSwitcher = SceneSwitcher.instance;
        Debug.Log("Upgrade Handler: " + upgradeHandler);
        RenderCard(SelectRandomCard());
        ResetReroll();
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

    public void OnChooseButtonClicked()
    {
        if (cardType == Card.CardType.Buff)
        {
            // TODO: Apply buff
            Debug.Log("Buff card chosen: " + card.GetCardName);

            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            Debug.Log("Upgrade Hand!wdsfasdfasdfler: " + upgradeHandler);
            // --------------------------------------------------           // again jinna if u can make this the "addbuff()" fn in upgradehandler instead
            PlayerStats playerstats = player.GetComponent<PlayerStats>();
            switch(card.GetCardName)
            {
                case "MaxHP":
                    playerstats.setMaxHp(5); break;
                case "MoveSpeed":
                    playerstats.setMovementSpeed(2); break;
                default:
                    Debug.Log("buff not defined or still not implemented"); break;
            }
            // -----------THE PART I ADDED-----------------------
            sceneSwitcher.UnpauseGame();
        }
        else if (cardType == Card.CardType.Module)
        {
            // TODO: Add module to module list
            Debug.Log("Module card chosen: " + card.GetCardName);
            upgradeHandler.AddModule(card.GetPrefab);
            Debug.Log("Upgrade Hand!wdsfasdfasdfler: " + upgradeHandler);
            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            sceneSwitcher.UnpauseGame();
            // TODO: Switch to Manufacturing scene with a scene transition
        }
        else if (cardType == Card.CardType.Weapon)
        {
            // TODO: Handle weapon card
            Debug.Log("Weapon card chosen: " + card.GetCardName);
            Debug.Log("Upgrade Hand!wdsfasdfasdfler: " + upgradeHandler);
            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            //-----------------------------------------------------------       // HEY JINNA IF U CAN ADD TRANSFER THIS PART OF CODE TO THE "ADDWEAPON()" FN INSTEAD
            Weapons weaponsscript = player.GetComponent<Weapons>();         // fetches the weapon script from the player
            Debug.Log("Weapon card name: " + card.GetCardName);
            if (weaponsscript == null) Debug.LogError("No Weapons script found on Player!");

            switch (card.GetCardName)       // based on the cardname enable the respective script/gameobject
            {
                case "Cane":
                    weaponsscript.EnableRollingCane(); break;
                case "Frying Pan":
                    weaponsscript.EnableFryingPan(); break;
                case "Mr. Muffin":
                    Debug.Log("Card Type when chosen: " + cardType);
                    weaponsscript.EnableMrMuffins(); break;
                case "Oven (Bomb)":
                    weaponsscript.EnableOven(); break;
                case "Saccharine Perfume":
                    weaponsscript.EnableSacchirePerfume(); break;
                default: Debug.Log("Weapon not found"); break;
            }
            // ---------------THE PART I ADDED-------------------------------
            //upgradeHandler.AddWeapon(card.GetPrefab);     // commented this part since we dont instantiate prefabs anymore 
            sceneSwitcher.UnpauseGame();
        }
        else if (cardType == Card.CardType.Upgrade)
        {
            // TODO: Handle upgrade card
            Debug.Log("Upgrade Hand!wdsfasdfasdfler: " + upgradeHandler);
            Debug.Log("Card: " + card);
            Debug.Log("CardPrefab: " + card.GetPrefab);
            Debug.Log("Upgrade card chosen: " + card.GetCardName);
            Debug.Log("Create Upgrade Weapon Card handling");
            sceneSwitcher.UnpauseGame();
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

    Card SelectRandomCard()
    {
        int randomIndex = Random.Range(0, cardPool.Count);
        card = cardPool[randomIndex];
        return card;
    }
}
