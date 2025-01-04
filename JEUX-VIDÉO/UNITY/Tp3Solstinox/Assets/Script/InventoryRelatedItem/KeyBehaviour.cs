using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    Type keyType;

    [SerializeField] private SceneStateSave Save;

    private void Awake()
    {
        if (Save.KeyState)
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    { 
        if(transform.name.Contains("Green"))
        {
            keyType = Type.Green;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/Green", typeof(Material)) as Material;
        }
        else if (transform.name.Contains("Blue"))
        {
            keyType = Type.Blue;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/Blue", typeof(Material)) as Material;
        }
        else if (transform.name.Contains("Pink"))
        {
            keyType = Type.Pink;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/Pink", typeof(Material)) as Material;
        }
        else if (transform.name.Contains("White"))
        {
            keyType = Type.White;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/White", typeof(Material)) as Material;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInventory>().GainKey(this.keyType);

            Save.KeyState = true;

            gameObject.SetActive(false);
        }

       
    }
}
