using UnityEngine;
using System.Collections;

public class Scene2 : MonoBehaviour {

    public GameObject Viewport;

	// Use this for initialization
	void Start ()
    {
        // We use the default fractal color set in the Mandelbrot shader
        m_propBlock = new MaterialPropertyBlock();
        m_propBlock.AddVector("_ColorRatio", new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
        Viewport.GetComponent<Renderer>().SetPropertyBlock(m_propBlock);
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_propBlock.SetVector("_ColorRatio", new Vector4(1.0f, 1.0f, 0.5f, 1.0f));
        Viewport.GetComponent<Renderer>().SetPropertyBlock(m_propBlock);
    }

    private MaterialPropertyBlock m_propBlock;
}
