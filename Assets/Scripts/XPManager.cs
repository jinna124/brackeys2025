using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class XPManager : MonoBehaviour
{
    // Singleton pattern (probably bad practice, but eh)
    [SerializeField] XPShard XPShardPrefab;
    int XP = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetXP() { return XP; }
    public void AddXP(int value) { XP += value; }
    public void ResetXP() { XP = 0; }
    public XPShard GetXPShardPrefab() { return XPShardPrefab; }
}
