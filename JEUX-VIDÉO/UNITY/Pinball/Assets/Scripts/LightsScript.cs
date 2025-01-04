using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsScript : MonoBehaviour
{

    [SerializeField] private MeshFilter target_mesh;
    [SerializeField] private Mesh[] target_mesh_states;
    [SerializeField] private SpriteRenderer sprite_mesh;
    [SerializeField] private Sprite[] text_sprite;

    GameManager gameManager;

    int MultiplierValue;

    bool Active;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.CompareTag("X2"))
        {
            MultiplierValue = 2;
        }
        else if (gameObject.CompareTag("X3"))
        {
            MultiplierValue = 3;
        }
        else if (gameObject.CompareTag("X4"))
        {
            MultiplierValue = 4;
        }
        else if (gameObject.CompareTag("X5"))
        {
            MultiplierValue = 5;
        }
        else
        {
            MultiplierValue = 1;
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Active = false;
    }

        

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if(!Active)
            {
                if (gameObject.CompareTag("Lights"))
                {
                    target_mesh.mesh = target_mesh_states[1];
                }
                else
                {
                    sprite_mesh.sprite = text_sprite[1];
                }
                gameManager.AddMultiplier(MultiplierValue);

                gameManager.AddActiveLights(gameObject);

                Active = true;
            }

            gameManager.Addpoints(250);

        }
    }

    public void ResetLights()
    {
        if (gameObject.CompareTag("Lights"))
        {
            target_mesh.mesh = target_mesh_states[0];
        }
        else
        {
            sprite_mesh.sprite = text_sprite[0];
        }

        Active = false;
    }
}
