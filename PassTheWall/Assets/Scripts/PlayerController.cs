using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal static PlayerController instance = null;
    internal static Direction direction = Direction.None;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] internal SkinnedMeshRenderer playerMeshRenderer = null;
    [SerializeField] internal TrackManager activeTrackManager = null;
    [SerializeField] internal Animator animator = null;

    internal bool isGameOver = false;
    internal bool isGameStarted = false;
    private MeshRenderer activeGate = null;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            if (direction == Direction.Left)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
            else if (direction == Direction.Right)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Gate")
        {
            other.gameObject.transform.parent.transform.parent.gameObject.GetComponent<TrackManager>().BurstGate(other.gameObject.transform.position, other.gameObject.GetComponent<MeshRenderer>().material);
            activeGate = other.gameObject.GetComponent<MeshRenderer>();
            activeGate.enabled = false;
            GameUIHandler.instance.gameScore += 10;
        }
        else if (other.gameObject.tag == "Track")
        {
            activeTrackManager = other.gameObject.GetComponent<TrackManager>();
            activeTrackManager.ColorChanger();
        }
        else if(other.gameObject.tag=="Border" || other.gameObject.tag == "Wall")
        {
            animator.SetInteger("Index", Random.Range(1,5));
            moveSpeed = 0;
            isGameOver = true;
            GameUIHandler.instance.GameOver();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Track")
        {
            activeGate.enabled = true;
        }
    }
}
