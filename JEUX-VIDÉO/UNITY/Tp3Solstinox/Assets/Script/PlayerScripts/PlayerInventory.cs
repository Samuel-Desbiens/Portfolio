using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    int GreenKey;
    int BlueKey;
    int PinkKey;
    int WhiteKey;

    const int NumberOfBall = 10;

     int BallInventory;

    Text GreenText;
    Text BlueText;
    Text PinkText;
    Text WhiteText;
    Text TokenText;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Canvas"));
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in GameObject.Find("Canvas").transform)
        {
            if(child.name.Contains("Green"))
            {
                GreenText = child.gameObject.GetComponent<Text>();
            }
            else if(child.name.Contains("Blue"))
            {
                BlueText = child.gameObject.GetComponent<Text>();
            }
            else if (child.name.Contains("Pink"))
            {
                PinkText = child.gameObject.GetComponent<Text>();
            }
            else if (child.name.Contains("White"))
            {
                WhiteText = child.gameObject.GetComponent<Text>();
            }
            else if (child.name.Contains("Token"))
            {
                TokenText = child.gameObject.GetComponent<Text>();
            }
        }

        GreenKey = 0;
        BlueKey = 0;
        PinkKey = 0;
        WhiteKey = 0;
        GreenText.text = GreenKey.ToString();
        BlueText.text = BlueKey.ToString();
        PinkText.text = PinkKey.ToString();
        WhiteText.text = WhiteKey.ToString();
    }

    public void GainKey(Type keyType)
    {
        if(keyType == Type.Green)
        {
            GreenKey++;
            GreenText.text = GreenKey.ToString();
        }
        else if (keyType == Type.Blue)
        {
            BlueKey++;
            BlueText.text = BlueKey.ToString();
        }
        else if (keyType == Type.Pink)
        {
            PinkKey++;
            PinkText.text = PinkKey.ToString();
        }
        else if (keyType == Type.White)
        {
            WhiteKey++;
            WhiteText.text = WhiteKey.ToString();
        }
    }

    public void GainBall()
    {
        BallInventory++;

        TokenText.text = BallInventory.ToString();
        if(BallInventory >= 10)
        {
            SceneManager.LoadScene("EndMenu");
        }
    }

    public bool OpenDoor(Type doorType)
    {
        if (doorType == Type.Green)
        {
            if (GreenKey > 0)
            {
                GreenKey--;
                GreenText.text = GreenKey.ToString();
                return true;
            }
        }
        else if (doorType == Type.Blue)
        {
            if (BlueKey > 0)
            {
                BlueKey--;
                BlueText.text = BlueKey.ToString();
                return true;
            }
        }
        else if (doorType == Type.Pink)
        {
            if (PinkKey > 0)
            {
                PinkKey--;
                PinkText.text = PinkKey.ToString();
                return true;
            }
        }
        else if(doorType == Type.White)
        {
            if (WhiteKey > 0)
            {
                WhiteKey--;
                WhiteText.text = WhiteKey.ToString();
                return true;
            }
        }

        return false;
    }
}
