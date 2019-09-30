using UnityEngine;

public class SlideCurve : MonoBehaviour
{
    private static SlideCurve m_instance;

    public AnimationCurve m_curve;

    public static SlideCurve Get()
    {
        if (m_instance == null)
        {
            // var go = new GameObject("SlideCurve");
            // m_instance = go.AddComponent<SlideCurve>();

            m_instance = GameObject.Find("SlideCurve").GetComponent<SlideCurve>();
        }

        return m_instance;
    }
}
