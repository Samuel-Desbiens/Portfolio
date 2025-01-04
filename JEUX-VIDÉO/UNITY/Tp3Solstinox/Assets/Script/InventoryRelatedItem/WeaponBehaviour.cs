using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    const float speed = 10f;

    Vector3 Direction;

    SoundManager smfx;


    private void Start()
    {
        smfx = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

    public void Thrown(Vector3 tRotation)
    {
        Direction = tRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        {
            if(!collision.gameObject.CompareTag("Enemy"))
            {
                smfx.PlayWallHit();
            }

            gameObject.SetActive(false);
        } 
    }
}
