using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceWaveWaveBehaviour : MonoBehaviour
{
    private IceWaveBehaviour ParentScript;
    private SpriteRenderer Sprite;

    private bool WaveDone;

    private Rigidbody2D RB;
    private Collider2D IWWcollider;

    SoundManager soundManager;

    private Vector3 BaseBaseScale = new Vector3(15f, 25f, 0);
    private Vector3 UpgradedBaseScale = new Vector3(30, 45, 0);

    private float WaveScaleDownMultiplier = 0.95f;

    private void Awake()
    {
        ParentScript = transform.parent.GetComponent<IceWaveBehaviour>();
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        IWWcollider = gameObject.GetComponent<Collider2D>();
        soundManager = SoundManager.Instance;
    }
    private void OnEnable()
    {
        ResetSpell();
        Cast();
    }

    private void ResetSpell()
    {
        if (ParentScript.transform.childCount > 2)
        {
            transform.localScale = UpgradedBaseScale;
        }
        else
        {
            transform.localScale = BaseBaseScale;
        }
        IWWcollider.enabled = true;


        WaveDone = false;
    }

    private void Cast()
    {
        soundManager.PlayAudio(soundManager.waveClip, transform.position);
        if (ParentScript.GetDirection() < 0)
        {
            Sprite.flipX = true;
        }
        else
        {
            Sprite.flipX = false;
        }
    }

    private void Update()
    {
        if (!WaveDone)
        {
            transform.Translate(new Vector3(ParentScript.GetMovementSpeed() * ParentScript.GetDirection(), 0, 0) * Time.deltaTime);

            if (RB.velocity.y < -3)
            {
                WaveDone = true;

                IWWcollider.enabled = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(ParentScript.GetMovementSpeed() * ParentScript.GetDirection(), 0, 0) * Time.deltaTime);

            transform.localScale *= WaveScaleDownMultiplier;

            if (transform.localScale.x <= 0.1f || transform.localScale.y <= 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDmg(transform.parent.GetComponent<IceWaveBehaviour>().GetDamage());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y) * transform.parent.GetComponent<IceWaveBehaviour>().GetKnockback());
        }
        else if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<Health>().TakeDmg(transform.parent.GetComponent<IceWaveBehaviour>().GetDamage());
        }
        else if (collision.tag != "Player")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * (Sprite.flipX ? -1 : 1), 5, LayerMask.GetMask("Ground"));
            if (hit.collider)
            {
                WaveDone = true;
            }
        }
    }
}
