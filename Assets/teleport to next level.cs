using UnityEngine;

public class teleporttonextlevel : MonoBehaviour
{
    [SerializeField] public playerspwn SpSys;
    public GameObject NextSpawn;
    public GameObject ThisLvl;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SpSys.ToTheNext();
    }
}
