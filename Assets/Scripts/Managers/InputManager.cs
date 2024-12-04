using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : PersistentSingleton<InputManager>
{
    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Frog frog = hit.collider.GetComponentInChildren<Frog>();
                if (frog != null && frog.gameObject.activeInHierarchy)
                {
                    frog.OnClicked();
                }
            }
        }
    }
}
