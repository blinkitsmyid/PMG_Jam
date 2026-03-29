using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookerBody : MonoBehaviour
{
    public static EnemyLookerBody Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // столкновение с игроком → вызываем его смерть
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.Death();

           
        }
        if (collision.TryGetComponent(out Lamp lamp))
        {
            if (lamp.IsOn())
            {
                EnemyLooker loooker = GetComponentInParent<EnemyLooker>();
                if (loooker != null)
                {
                    loooker.Respawn();
                }
            }
        }
    }
}