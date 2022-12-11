using UnityEngine;

public class CollectibleXp : MonoBehaviour
{
    public int Value { get; private set; }

    public void Initialize(int value)
    {
        Value = value;
    }
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);
        
        if (other != null)
        {
            other.GetXP(Value);
            GameObject.Destroy(gameObject);
        }
    }
    
    
    
}
