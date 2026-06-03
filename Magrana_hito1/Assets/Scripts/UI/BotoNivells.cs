using UnityEngine;
using UnityEngine.UI;

public class BotoNivells : MonoBehaviour
{
    public int indexNivell;
    public Button boto;

    void Start()
    {
        int levelDesbloc = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (indexNivell <= levelDesbloc)
        {
            boto.interactable = true;
        }
        else
        {
            boto.interactable = false;
        }
    }
}