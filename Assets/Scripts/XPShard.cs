using UnityEngine;

public class XPShard : MonoBehaviour
{      
    int enemyXPValue;

    // There will only ever be one type of XP Shard, since XP Shard will take the XP value of the defeated enemy
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetXPValue(int value) { enemyXPValue = value; }
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            XPManager xpManager = FindAnyObjectByType<XPManager>();
            xpManager.AddXP(10); // Give 10 XP to the player
            Debug.Log("XP shard collected by player");
            Destroy(gameObject);
        }
    }
}
