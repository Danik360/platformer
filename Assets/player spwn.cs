using UnityEngine;

public class playerspwn : MonoBehaviour
{
    public GameObject Nextlvl;
    public GameObject Spawn;
    public GameObject player;
    public void Start()
    {
        player.transform.position = Spawn.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = Spawn.transform.position;
        }
    }

    public void ToTheNext()
    {
        player.transform.position = Nextlvl.transform.position;
    }
}
