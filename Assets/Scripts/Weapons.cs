using UnityEngine;

// ONLY ACTS AS A MARKER
public class Weapons : MonoBehaviour
{
    [SerializeField] GameObject SacchirePerfume;
    private MrMuffin mrMuffins;
    private FryingPan fryingPan;
    private Oven oven;
    private RollingCane rollingCane;
    private void Start()
    {
        mrMuffins = GetComponent<MrMuffin>();
        fryingPan = GetComponent<FryingPan>();
        oven = GetComponent<Oven>();
        rollingCane = GetComponent<RollingCane>();
    }


    public void EnableMrMuffins() {  mrMuffins.enabled = true; }
    public void DisableMrMuffins() { mrMuffins.enabled = false; }

    public void EnableFryingPan() { fryingPan.enabled = true; }
    public void DisableFryingPan() { fryingPan.enabled  = false; }

    public void EnableOven() {  oven.enabled = true; }
    public void DisableOven() { oven.enabled = false; }

    public void EnableRollingCane() {  rollingCane.enabled = true; }
    public void DisableRollingCane() { rollingCane .enabled = false; }

    public void EnableSacchirePerfume() { SacchirePerfume.SetActive(true); 
    }
    public void DisableSacchirePerfume() { SacchirePerfume.SetActive(false); }

}
