using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClassSelection : MonoBehaviour
{
    public Client Client;
    private List<Button> _choiceButtons;
    private void Awake()
    {
        _choiceButtons = new List<Button>();

        for (int i = 0; i < 4; i++)
        {
            int temp = i; //Delegates use the index as pointer. I had to realloc a new index in order to change i's address.
            _choiceButtons.Add(transform.GetChild(i).GetComponent<Button>());
            _choiceButtons[i].onClick.AddListener(delegate { Client.CreatePlayerClass(temp); AssignSound(temp); });
        }
    }

    public void AssignSound(int index)
    {
        switch(index)
        {
            case 0:
                SoundManager.Instance.SoundType = new SoundType.Rifle();
                break;
            case 1:
                SoundManager.Instance.SoundType = new SoundType.Shotgun();
                break;
            case 2:
                SoundManager.Instance.SoundType = new SoundType.Handgun();
                break;
            case 3:
                SoundManager.Instance.SoundType = new SoundType.Lasergun();
                break;
            default:
                SoundManager.Instance.SoundType = new SoundType.Rifle();
                break;
        }
    }
}

