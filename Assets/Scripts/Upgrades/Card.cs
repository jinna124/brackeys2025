using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] string cardName;
    [SerializeField] string cardSubtitle;
    [SerializeField][TextArea(10, 100)] string cardDescription;
    [SerializeField] Sprite cardImage;
    [SerializeField] Sprite buffPanelImage;
    [SerializeField] Sprite modulePanelImage;
    [SerializeField] Sprite weaponPanelImage;
    [SerializeField] Sprite upgradePanelImage;
    [SerializeField] Sprite gachaPanelImage;
    [SerializeField] Sprite buffCardFrame;
    [SerializeField] Sprite moduleCardFrame;
    [SerializeField] Sprite weaponCardFrame;
    [SerializeField] Sprite upgradeCardFrame;
    [SerializeField] Sprite gachaCardFrame;
    [SerializeField]
    public enum CardType
    {
        Buff,
        Module,
        Weapon,
        Upgrade,
        Gacha
    }

    [SerializeField] CardType cardType;
    [SerializeField] GameObject prefab;

    public string GetCardName
    {
        get
        {
            // Use serialized field if provided, otherwise get from component
            if (!string.IsNullOrEmpty(cardName)) return cardName;
            
            if (prefab == null) return "Unknown Card";
            
            switch (cardType)
            {
                case CardType.Buff:
                    var buff = prefab.GetComponent<Buff>();
                    return buff != null ? buff.GetName() : "Unknown Buff";
                case CardType.Module:
                    var module = prefab.GetComponent<Module>();
                    return module != null ? module.GetName() : "Unknown Module";
                default:
                    return "Unknown Card Type";
            }
        }
    }
    
    public string GetCardSubtitle { get { return cardType.ToString(); } }
    
    public string GetCardDescription
    {
        get
        {
            // Use serialized field if provided, otherwise get from component
            if (!string.IsNullOrEmpty(cardDescription)) return cardDescription;
            
            if (prefab == null) return "No description available.";
            
            switch (cardType)
            {
                case CardType.Buff:
                    var buff = prefab.GetComponent<Buff>();
                    return buff != null ? buff.GetDescription() : "No buff description.";
                case CardType.Module:
                    var module = prefab.GetComponent<Module>();
                    return module != null ? module.GetDescription() : "No module description.";
                default:
                    return "No description available for this card type.";
            }
        }
    }
    
    public Sprite GetCardImage { get { return cardImage; } }
    
    public Sprite GetPanelImage
    {
        get
        {
            if (cardType == CardType.Buff && buffPanelImage != null)
                return buffPanelImage;
            else if (cardType == CardType.Module && modulePanelImage != null)
                return modulePanelImage;
            else if (cardType == CardType.Weapon && weaponPanelImage != null)
                return weaponPanelImage;
            else if (cardType == CardType.Upgrade && upgradePanelImage != null)
                return upgradePanelImage;
            else if (cardType == CardType.Gacha && gachaPanelImage != null)
                return gachaPanelImage;
            else
                return cardImage; // Fallback to card image if no panel image is set
        }
    }
    
    public Sprite GetCardFrame
    {
        get
        {
            if (cardType == CardType.Buff && buffCardFrame != null)
                return buffCardFrame;
            else if (cardType == CardType.Module && moduleCardFrame != null)
                return moduleCardFrame;
            else if (cardType == CardType.Weapon && weaponCardFrame != null)
                return weaponCardFrame;
            else if (cardType == CardType.Upgrade && upgradeCardFrame != null)
                return upgradeCardFrame;
            else if (cardType == CardType.Gacha && gachaCardFrame != null)
                return gachaCardFrame;
            else
                return null; // No fallback frame
        }
    }
    
    public CardType GetCardType { get { return cardType; } }
    public GameObject GetPrefab { get { return prefab; } }

}
