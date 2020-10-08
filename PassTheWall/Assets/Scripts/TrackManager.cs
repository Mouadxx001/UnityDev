using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private Transform track_End;
    [SerializeField] private Transform track_Start;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameObject wallHolder = null;
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private ParticleSystem burstEffect;
    [SerializeField] internal TrackManager nextTrackManagerRef = null;

    private Vector3 track_EndPos = Vector3.zero;
    private Vector3 trackPos = Vector3.zero;
    private List<MeshRenderer> wallMeshRenderers = new List<MeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        track_EndPos = track_End.position;
        trackPos = transform.position;

        for (int i = 0; i < wallHolder.transform.childCount; i++)
        {
            wallMeshRenderers.Add(wallHolder.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }
        if (PlayerController.instance.activeTrackManager == this)
        {
            ColorChanger();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance.isGameOver && PlayerController.instance.isGameStarted)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);
            trackPos = transform.position;
            if (trackPos.z <= track_EndPos.z)
            {
                transform.position = new Vector3(0, 0, track_Start.position.z);
            }
        }
    }

    internal void ColorChanger()
    {
        List<Material> temp = new List<Material>();
        int index = -1;

        foreach (Material m in materials)
        {
            temp.Add(m);
        }

        foreach (MeshRenderer r in wallMeshRenderers)
        {
            index = Random.Range(0, temp.Count);
            r.material = temp[index];
            r.gameObject.tag = "Wall";
            temp.RemoveAt(index);
        }
        GameObject gate = wallMeshRenderers[Random.Range(0, wallMeshRenderers.Count)].gameObject;
        gate.tag = "Gate";
        PlayerController.instance.playerMeshRenderer.material = gate.GetComponent<MeshRenderer>().material;
    }

    internal void BurstGate(Vector3 pos, Material mat)
    {
        burstEffect.transform.position = pos;
        burstEffect.Play();
        burstEffect.GetComponent<ParticleSystemRenderer>().material = mat;
    }
}
