using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour
{
    Type doorType;

    bool open;

    Vector3 UpPosition;

    const float UpPositionOffset = 3f;
    const float UpSpeed = 5f;

    [SerializeField] private SceneStateSave Save;

    // Start is called before the first frame update
    void Start()
    {
        open = Save.DoorState;

        UpPosition = new Vector3(transform.position.x, transform.position.y + UpPositionOffset, transform.position.z);

        if (transform.name.Contains("Green"))
        {
            doorType = Type.Green;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/Green", typeof(Material)) as Material;
        }
        else if (transform.name.Contains("Blue"))
        {
            doorType = Type.Blue;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/Blue", typeof(Material)) as Material;
        }
        else if (transform.name.Contains("Pink"))
        {
            doorType = Type.Pink;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/Pink", typeof(Material)) as Material;
        }
        else if (transform.name.Contains("White"))
        {
            doorType = Type.White;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/White", typeof(Material)) as Material;
        }
    }

    private void Update()
    {
        if(open && transform.position != UpPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, UpPosition, UpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!open)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<PlayerInventory>().OpenDoor(this.doorType))
                {
                    open = true;
                    Save.DoorState = true;
                }
            }
        }
    }
}
