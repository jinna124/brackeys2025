using UnityEngine;
using UnityEngine.UIElements;

public class MrMuffin : Weapons
{
    [Header("General Settings")]
    [Space]
    [Header("____Rotation Settings____")]
    [SerializeField] GameObject mrMuffin_prefab;
    [SerializeField] float mrMuffinSpeed;
    [SerializeField] float mrMuffinRange;
    [Space]
    [Header("____Floating Settings____")]
    [SerializeField] bool floats = true;
    [SerializeField] float mrMuffinFloatSpeed;
    [SerializeField] float mrMuffinFloatAmplitude;
    [Space]
    [Header("____Smoothing Settings____")]
    [SerializeField] bool smoothenMovement = true;
    [SerializeField] float smoothing = 5f;
    
    private Vector2 offset;
    private GameObject mrMuffin;
    private float angle;
    private void Start()
    {
        // give the angle some randomness doesnt have to always be at 0
        angle = Random.Range(0f, 2 * Mathf.PI);
        offset = (Vector2)transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * mrMuffinRange;
        mrMuffin = Instantiate(mrMuffin_prefab, offset, Quaternion.identity);
    }
    private void Update()
    {
        if (mrMuffin != null)
            rotate();
    }

    void rotate()
    {
        // get the relative position first
        offset = (Vector2)transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * mrMuffinRange;
        // ------floating------- (additional sin wave movement to it to feel like it is floating)
        float floatCrest = 0;
        if(floats)
            floatCrest = Mathf.Sin(Time.time * mrMuffinFloatSpeed) * mrMuffinFloatAmplitude;
 
        Vector2 finalPosition = offset + new Vector2(0, floatCrest);

        if (smoothenMovement)
            mrMuffin.transform.position = Vector2.Lerp(mrMuffin.transform.position, finalPosition, smoothing * Time.fixedDeltaTime);
        else
            mrMuffin.transform.position = finalPosition;
        angle += mrMuffinSpeed * Time.deltaTime;
    }
}
