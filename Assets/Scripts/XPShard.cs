using UnityEngine;

public class XPShard : MonoBehaviour
{      
    float enemyXPValue;

    // There will only ever be one type of XP Shard, since XP Shard will take the XP value of the defeated enemy
    public void SetXPValue(float value) { enemyXPValue = value; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            XPManager xpManager = FindAnyObjectByType<XPManager>();
            xpManager.AddXP(5); // Give 10 XP to the player
            Debug.Log("XP shard collected by player");
            Destroy(gameObject);
        }
    }
}
