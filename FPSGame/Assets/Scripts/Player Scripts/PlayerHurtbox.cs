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
        if(other.GetComponent<Hitbox>() != null)
        {
            Hitbox hb = other.GetComponent<Hitbox>();

            character.TakeDamage(hb.Hit());
        }
    }
}
