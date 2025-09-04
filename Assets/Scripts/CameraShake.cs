using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource impulseSource;

    public void Shake()
    {
       impulseSource.GenerateImpulse(1);
    }
}
