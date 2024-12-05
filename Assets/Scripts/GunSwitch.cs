using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [SerializeField] private GameObject gun1, gun2;
    void Start()
    {
        ChooseGun1();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("gun1"))
        {
            ChooseGun1();
        }
        else if (Input.GetButtonDown("gun2"))
        {
            ChooseGun2(); 
        }
    }

    void ChooseGun1()
    {
        gun1.SetActive(true);
        gun2.SetActive(false);

    }
    void ChooseGun2()
    {
        gun1.SetActive(false);
        gun2.SetActive(true);

    }
}
