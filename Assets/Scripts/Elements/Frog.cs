using UnityEngine;

public class Frog : Element
{
    [SerializeField] private Direction pose;
    [SerializeField] private Tongue tongue;

    private void Start()
    {;
        tongue.SetColor(color);
    }

    //If frog is clicked.
    public void OnClicked()
    {
        if(!tongue.isReleasing && !tongue.isRetracting)
        {
            //Set the tongue's direction and release it.
            tongue.SetDirection(pose);
            tongue.Release();
            Actions.OnFrogClicked();
        }
    }
}
