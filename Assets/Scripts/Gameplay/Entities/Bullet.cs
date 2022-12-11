using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10;
    public int Team;
    public float Damage = 5;

    private Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position += _direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var other = HitWithParent.GetGameObject(col);
        Unit otherUnit = other.GetComponent<Unit>();
        
        if (otherUnit == null)
        {
            GameObject.Destroy(gameObject);
        }
        else if (otherUnit.Team != Team)
        {
            GameObject.Destroy(gameObject);

            otherUnit.Hit(Damage);
        }
    }
}
