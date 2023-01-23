using UnityEngine;

/// <summary>
/// Represents the xp points the player has to collect to level up
/// </summary>
public class CollectableLife : MonoBehaviour
{

    
    void OnTriggerEnter2D(Collider2D col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);
        
        if (other != null)
        {
            other.Heal(10);
            GameObject.Destroy(gameObject);
        }
    }
    
    
    
}
