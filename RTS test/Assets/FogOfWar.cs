using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public GameObject m_fogOfWarPlane;
    public Transform m_player;
    public LayerMask m_fogLayer;
    public float m_radius = 5f;
    private float m_radiusSqr { get { return m_radius*m_radius; } }
    [SerializeField]
    private Mesh m_mesh;
    private Vector3[] m_verticies;
    private Color[] m_colors;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        for (int j = 0; j < UnitSelections.Instance.unitList.Count; j++)
        {
            Ray r = new Ray(UnitSelections.Instance.unitList[j].transform.position, Vector3.up * 30f);
            RaycastHit hit;
            Debug.DrawRay(UnitSelections.Instance.unitList[j].transform.position, Vector3.up * 30f);
            if (Physics.Raycast(r, out hit, Mathf.Infinity, m_fogLayer))
            {
                for (int i = 0; i < m_verticies.Length; i++)
                {
                    Vector3 v = m_verticies[i];
                    float dist = Vector3.SqrMagnitude(v - hit.point);
                    //float dist = Vector3.Distance(v, hit.point);
                    if (dist < m_radiusSqr)
                    {
                        float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
                        m_colors[i].a = alpha;
                    }
                }
                UpdateColor();
            }
        }
        //Ray r = new Ray(transform.position, m_player.position - transform.position);
        //RaycastHit hit;
        //Debug.DrawRay(transform.position, m_player.position - transform.position, Color.green);
        //if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        //{
        //    for (int i = 0; i < m_verticies.Length; i++)
        //    {
        //        Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_verticies[i]);
        //        //float dist = Vector3.SqrMagnitude(hit.point-v);
        //        float dist = Vector3.Distance(v, hit.point);
        //        Debug.Log(dist);
        //        if (dist < m_radiusSqr)
        //        {
        //            float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
        //            m_colors[i].a = alpha;
        //        }
        //    }
        //    UpdateColor();
        //}
    }

    void Initialize()
    {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_verticies = m_mesh.vertices;
        m_colors = new Color[m_verticies.Length];

        for (int i = 0; i < m_mesh.vertices.Length; i++)
        {
            m_verticies[i] = m_fogOfWarPlane.transform.TransformPoint(m_mesh.vertices[i]);
        }

        for (int i =0; i<m_colors.Length; i++)
        {
            m_colors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_colors;
    }
}
