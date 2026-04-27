using UnityEngine;

public class Teleporrfromvoid : MonoBehaviour
{
    [SerializeField] public playerspwn SAVE;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SAVE.Start();
    }
}
