using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class Monster : MonoBehaviour
{
    //Allows us to assign sprite to the monster
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied) 
            return false;
        //If collision was with a gameobject that had bird, then save that, else its a NULL
        Birb bird = collision.gameObject.GetComponent<Birb>();

        if (bird != null) //won't be null unless we hit something that isn't a bird
            return true;

        //Normals are -1 if something is coming straight down in the collision.
        //1 if coming straight up. When using the Y part of the normal

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }

    IEnumerator Die()
    {
        
        _hasDied = true;
        _particleSystem.Play();
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        yield return new WaitForSeconds(1);
        // Disapears since its not active anymore
        gameObject.SetActive(false);
    }
}
