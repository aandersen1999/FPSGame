using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public Player character;

    private BoxCollider box;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //I'm gonna make this better I swear
        if(other.gameObject.GetComponent<Hitbox>() != null)
        {
            Hitbox hb = other.GetComponent<Hitbox>();

            character.TakeDamage(hb.Hit());
        }
        if(other.gameObject.GetComponent<Projectiles>() != null)
        {
            Projectiles pb = other.GetComponent<Projectiles>();
            character.TakeDamage(pb.damage);
            Destroy(pb.gameObject);
        }
    }
}
