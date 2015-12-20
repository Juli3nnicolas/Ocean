using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaximizeBound : MonoBehaviour {
    public List<GameObject> meshToModify = new List<GameObject>();
    public Vector3 myMaxBoundsSize = new Vector3(50,50,50);

    // Use this for initialization
    void Start () {
        for (int i = 0; i < meshToModify.Count; i++)
        {
            // Get the SkinnedMeshRenderer component
            SkinnedMeshRenderer skinnedMeshRenderer = meshToModify[i].GetComponent<SkinnedMeshRenderer>();

            // Create and set your new bounds
            Bounds newBounds = new Bounds(Vector3.zero, myMaxBoundsSize);
            skinnedMeshRenderer.localBounds = newBounds;
        }



    }
}
