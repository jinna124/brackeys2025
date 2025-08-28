using UnityEngine;

[CreateAssetMenu(fileName = "New Module", menuName = "Module")]
public class Module : ScriptableObject
{
    [SerializeField]
    enum ModuleType
    {
        Nonnas_Oven,
        Cupcake_Stand,
        Industrial_Mixer,
        Sugar_Reactor,
        MegaBakery_Tower
    }
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetCPS()
    {
        // Calculate cookies per second for this module
        return 1f; // Placeholder value
    }
}
