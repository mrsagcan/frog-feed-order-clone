using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private void Update()
    {
        GetPlayerInput();
    }

    //If player clicks a frog.
    private void GetPlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //If what we hit is a frog.
                Frog frog = hit.collider.GetComponentInChildren<Frog>();
                if (frog != null && frog.gameObject.activeInHierarchy)
                {
                    frog.OnClicked();
                }
            }
        }
    }
}
