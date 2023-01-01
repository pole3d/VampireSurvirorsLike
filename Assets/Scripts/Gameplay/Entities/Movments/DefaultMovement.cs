using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class DefaultMovement : BaseMovement
{
    public override void Update(EnemyController enemy , Rigidbody2D rb)
    {
        Vector3 direction = enemy.GetFuturePlayerPosition() - enemy.transform.position;
        direction.z = 0;

        float moveStep = enemy.Data.MoveSpeed * Time.deltaTime;
        if (moveStep >= direction.magnitude)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            direction.Normalize();
            rb.velocity = direction * enemy.Data.MoveSpeed;
        }
    }



}

