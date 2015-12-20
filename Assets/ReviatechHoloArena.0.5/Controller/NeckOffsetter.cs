using UnityEngine;
using System.Collections;

public class NeckOffsetter : MonoBehaviour {
    public Transform NeckPos;
    public Vector3 UpVectorWorld = Vector3.up;
    public Transform HeadOrientationTransform;
    public Vector3 HeadOrientationDir;
    public Transform HeadBaseToMove;
    private float HALFPI=0;
    private float mag;

    public Transform dirRefForNeck;
    public Vector3 vecForNeckRotate;
    // Use this for initialization
    void Start () {
        mag = (HeadBaseToMove.position - NeckPos.position).magnitude;
        HeadOrientationDir.Normalize();

        HALFPI = Mathf.PI / 2;
    }
	
	// Update is called once per frame
	void Update () {

        // WORKS ONLY IN -90 / +90 quadrant

        Vector3 toUseVec = HeadOrientationTransform.TransformDirection(Vector3.right);
        float a = Mathf.Asin( Vector3.Dot(toUseVec, Vector3.Cross(UpVectorWorld, HeadOrientationTransform.TransformDirection(HeadOrientationDir))));
        // Debug.DrawRay(Vector3.zero, toUseVec);
        // Debug.DrawRay(Vector3.zero, UpVectorWorld);
        //  Debug.DrawRay(Vector3.zero, HeadOrientationTransform.TransformDirection(HeadOrientationDir));

        //float angle = Vector3.Angle(UpVectorWorld, HeadOrientationTransform.TransformDirection(HeadOrientationDir));
        //Debug.Log("!>" + Vector3.Angle(UpVectorWorld, HeadOrientationTransform.TransformDirection(HeadOrientationDir)));
        //Debug.Log(" >" + a * Mathf.Rad2Deg);
        a = Mathf.Min(HALFPI, a);
        a = Mathf.Max(-HALFPI, a);



        NeckPos.localRotation = Quaternion.Euler(new Vector3(0, dirRefForNeck.rotation.eulerAngles.y, 0));

        HeadBaseToMove.position = NeckPos.position + Quaternion.AngleAxis(a*Mathf.Rad2Deg, NeckPos.TransformDirection( vecForNeckRotate)) * new Vector3(0, mag, 0);
    }
}
