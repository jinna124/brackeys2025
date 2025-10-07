using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource impulseSource;

    public void Shake()
    {
       impulseSource.GenerateImpulse(1);
    }
    
    public void Shake(int power)
    {
        impulseSource.GenerateImpulse(power);
    }
}
