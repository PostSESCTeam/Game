using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomsClicks : MonoBehaviour
{
    public GameObject ProfilePlace;
    public GameObject FormsPlace;
    public bool IsProfileOpened = false;
    public bool IsFormsOpened = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsProfileOpened)
        {
            ProfilePlace.SetActive(true);
            FormsPlace.SetActive(false);
        }
        else if (IsFormsOpened)
        {
            FormsPlace.SetActive(true);
            ProfilePlace.SetActive(false);
        }
    }

    public void OpenProfile()
    {
        IsProfileOpened = true;
        IsFormsOpened = false;
    }

    public void OpenForms()
    {
        IsProfileOpened = false;
        IsFormsOpened = true;
    }
}
