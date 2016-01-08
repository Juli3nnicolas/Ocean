using UnityEngine;
using System.Collections;

public class Scene2 : MonoBehaviour {

    public GameObject Viewport;
    public GameObject Player;

	// Use this for initialization
	void Start ()
    {
        // We use the default fractal color set in the Mandelbrot shader
        m_propBlock = new MaterialPropertyBlock();
        m_propBlock.AddVector("_ColorRatio", new Vector4(1.0f,1.0f,1.0f,1.0f));
        Viewport.GetComponent<Renderer>().SetPropertyBlock(m_propBlock);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Get Player's delta hand position
        Vector3 rightHandDelta = Player.GetComponentsInChildren<Hand>()[0].GetComponent<deltaPosition>().getDeltaPos();
        Vector3 leftHandDelta = Player.GetComponentsInChildren<Hand>()[1].GetComponent<deltaPosition>().getDeltaPos();

        // Change the fractal's color with regard to the player's delta hand position
        if (rightHandDelta != Vector3.zero )
            increaseFractalColor(rightHandDelta);

        if (leftHandDelta != Vector3.zero )
            decreaseFractalColor( leftHandDelta );
    }

    private void increaseFractalColor(Vector3 deltaPos)
    {
        // Set red component: Push to increase the red component
        if (deltaPos.z > 0)
        {
            m_colorRatio.x = Mathf.Clamp01( m_colorRatio.x + M_COLOR_QTY);
            updateFractalColor( m_colorRatio );
        }

        // Set green component: raise your hand to add green
        if (deltaPos.y > 0)
        {
            m_colorRatio.y = Mathf.Clamp01( m_colorRatio.y + M_COLOR_QTY);
            updateFractalColor(m_colorRatio);
        }

        // Set blue component: swipe on the right to add some blue
        if (deltaPos.x > 0)
        {
            m_colorRatio.z = Mathf.Clamp01( m_colorRatio.z + M_COLOR_QTY);
            updateFractalColor(m_colorRatio);
        }

    }

    private void decreaseFractalColor(Vector3 deltaPos)
    {
        const float MIN_COLOR = 0.4f;
        const float MAX_COLOR = 1.0f;

        // Set red component: Push to increase the red component
        if (deltaPos.z < 0)
        {
            m_colorRatio.x = Mathf.Clamp(m_colorRatio.x - M_COLOR_QTY, MIN_COLOR, MAX_COLOR);
            updateFractalColor(m_colorRatio);
        }

        // Set green component: raise your hand to add green
        if (deltaPos.y < 0)
        {
            m_colorRatio.y = Mathf.Clamp(m_colorRatio.y - M_COLOR_QTY, MIN_COLOR, MAX_COLOR);
            updateFractalColor(m_colorRatio);
        }

        // Set blue component: swipe on the right to add some blue
        if (deltaPos.x < 0)
        {
            m_colorRatio.z = Mathf.Clamp(m_colorRatio.z - M_COLOR_QTY, MIN_COLOR, MAX_COLOR);
            updateFractalColor(m_colorRatio);
        }
    }

    private void updateFractalColor(Vector3 colorRatio)
    {
        m_propBlock.SetVector("_ColorRatio", new Vector4(colorRatio.x, colorRatio.y, colorRatio.z, 1.0f));
        Viewport.GetComponent<Renderer>().SetPropertyBlock(m_propBlock);
    }

    private MaterialPropertyBlock m_propBlock;
    private Vector3 m_colorRatio = new Vector3(252.0f/255.0f, 120.0f/255.0f, 220.0f/255.0f);
    private float M_COLOR_QTY = 0.01f;
}
