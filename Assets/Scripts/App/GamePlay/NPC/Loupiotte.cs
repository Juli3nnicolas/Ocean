using UnityEngine;
using System.Collections;

public class Loupiotte : MonoBehaviour, Core.StdInterfaces.Initiable {


	/* Loupitte use animations to move
	 * Meanning we dynamicaly set animation clips
	 */
	//The vector used by the animation it determines the position of loupiotte
	//Relative to the player
    public Vector3 positionToPlayer;
    Player player;
    Animation anim;
    public float state;
    float xReference;
    float zReference;
    float yReference;
    Vector3 disturbMove;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + positionToPlayer;
		//If an animation has end we play Insist animation 
        if (state == 1.0f)
        {
            anim.RemoveClip("Insist");
            SetLoupiotteInsist();
            anim.Play("Insist");
        }
	}

    public void Init()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animation>();
        SetLoupiotteAproach();
        state = 0.0f;
		//First we play approach animations
        anim.Play("Approche");
        SetLoupiotteInsist();
        SetLoupiotteRun();
        xReference = Random.Range(-1.0f, 1.0f);
        zReference = Random.Range(-1.0f, 1.0f);
		yReference = 3.0f;
        GetComponent<Light>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
    public void Terminate()
    {
        Destroy(this.gameObject);
    }
    void SetLoupiotteAproach()
    {
        AnimationClip clip = new AnimationClip();
        Keyframe[] xValues = new Keyframe[5];
        Keyframe[] yValues = new Keyframe[5];
        Keyframe[] zValues = new Keyframe[5];
        Keyframe[] stateValues = new Keyframe[5];
        for (int i = 0; i<5; i++)
        {
            xValues[i] = new Keyframe(i * 3.0f, i < 4 ? Random.Range(-20.0f / (2 * i + 1), 20.0f / (2 * i + 1)) : xReference);
            zValues[i] = new Keyframe(i * 3.0f, i < 4 ? Random.Range(-20.0f / (2 * i + 1), 20.0f / (2 * i + 1)) : zReference);
            stateValues[i] = new Keyframe(i * 3.0f, i < 4 ? 0.0f : 1.0f);
        }

        yValues[0] = new Keyframe(0.0f, 50.0f);
        yValues[1] = new Keyframe(3.0f, 35.0f);
        yValues[2] = new Keyframe(6.0f, 25.0f);
        yValues[3] = new Keyframe(9.0f, 15.0f);
        yValues[4] = new Keyframe(12.0f, yReference);

        AnimationCurve xCurve = new AnimationCurve(xValues);
        AnimationCurve yCurve = new AnimationCurve(yValues);
        AnimationCurve zCurve = new AnimationCurve(zValues);
        AnimationCurve stateCurve = new AnimationCurve(stateValues);
        clip.legacy = true;
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.x", xCurve);
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.y", yCurve);
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.z", zCurve);
        clip.SetCurve("", typeof(Loupiotte), "state", stateCurve);

        anim.AddClip(clip, "Approche");

    }
    void SetLoupiotteInsist()
    {
        AnimationClip clip = new AnimationClip();
        Keyframe[] xValues = new Keyframe[5];
        Keyframe[] yValues = new Keyframe[5];
        Keyframe[] zValues = new Keyframe[5];
        Keyframe[] stateValues = new Keyframe[5];
        for (int i = 0; i < 5; i++)
        {
            xValues[i] = new Keyframe(i * 2.0f, (i > 0 && i< 4) ? Random.Range(-1.0f, 1.0f) : xReference);
            zValues[i] = new Keyframe(i * 2.0f, (i > 0 && i< 4) ? Random.Range(-1.0f, 1.0f) : zReference);
            stateValues[i] = new Keyframe(i * 2.0f, i<4 ? 2.0f : 1.0f);
        }

        yValues[0] = new Keyframe(0.0f, yReference);
        yValues[1] = new Keyframe(2.0f, yReference - 0.5f);
        yValues[2] = new Keyframe(4.0f, yReference - 1.0f);
        yValues[3] = new Keyframe(6.0f, yReference + 2.0f);
        yValues[4] = new Keyframe(8.0f, yReference);

        AnimationCurve xCurve = new AnimationCurve(xValues);
        AnimationCurve yCurve = new AnimationCurve(yValues);
        AnimationCurve zCurve = new AnimationCurve(zValues);
        AnimationCurve stateCurve = new AnimationCurve(stateValues);
        clip.legacy = true;
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.x", xCurve);
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.y", yCurve);
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.z", zCurve);
        clip.SetCurve("", typeof(Loupiotte), "state", stateCurve);

        anim.AddClip(clip, "Insist");
    }
    void SetLoupiotteRun()
    {
        AnimationClip clip = new AnimationClip();
        Keyframe[] xValues = new Keyframe[5];
        Keyframe[] yValues = new Keyframe[5];
        Keyframe[] zValues = new Keyframe[5];
        Keyframe[] stateValues = new Keyframe[5];

        xValues[0] = new Keyframe(0.0f, transform.position.x);
        xValues[1] = new Keyframe(1.0f, transform.position.x + disturbMove.normalized.x * 4.0f);
        xValues[2] = new Keyframe(3.0f, xReference);

        zValues[0] = new Keyframe(0.0f, transform.position.z);
        zValues[1] = new Keyframe(1.0f, transform.position.z + disturbMove.normalized.z *4.0f);
        zValues[2] = new Keyframe(3.0f, zReference);

        stateValues[0] = new Keyframe(0.0f, 4.0f);
        stateValues[1] = new Keyframe(3.0f, 4.0f);
        stateValues[2] = new Keyframe(3.01f, 1.0f);

        yValues[0] = new Keyframe(0.0f, transform.position.y);
        yValues[1] = new Keyframe(1.0f, transform.position.y + disturbMove.normalized.y * 4.0f);
        yValues[2] = new Keyframe(3.0f, yReference);

        AnimationCurve xCurve = new AnimationCurve(xValues);
        AnimationCurve yCurve = new AnimationCurve(yValues);
        AnimationCurve zCurve = new AnimationCurve(zValues);
        AnimationCurve stateCurve = new AnimationCurve(stateValues);
        clip.legacy = true;
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.x", xCurve);
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.y", yCurve);
        clip.SetCurve("", typeof(Loupiotte), "positionToPlayer.z", zCurve);
        clip.SetCurve("", typeof(Loupiotte), "state", stateCurve);

        anim.AddClip(clip, "Run");
    }
	//If the player disturb Loupitte, we play set and play animation run away
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand" && state != 4.0f)
        {
            disturbMove = other.GetComponent<Hand>().GetMove() != Vector3.zero ? other.GetComponent<Hand>().GetMove() : (transform.position - other.GetComponentInParent<Player>().transform.position);
            if (disturbMove == Vector3.zero)
                disturbMove = new Vector3(1.0f, 1.0f, 1.0f);
            anim.RemoveClip("Run");
            SetLoupiotteRun();
            anim.Play("Run");
        }
    }


}
