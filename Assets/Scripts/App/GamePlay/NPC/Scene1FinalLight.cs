using UnityEngine;
using System.Collections;

public class Scene1FinalLight : MonoBehaviour, Core.StdInterfaces.Initiable
{

    public Vector3 positionToPlayer;
    Player player;
    Animation anim;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.FindChild("Eye Camera").transform.position + positionToPlayer;
    }

    public void Init()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void Terminate()
    {
        Destroy(this.gameObject);
    }

}
