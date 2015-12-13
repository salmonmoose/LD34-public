using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
  public GameObject vineHeadTemplate;
  public GameObject particleEffectTemplate;
  private GameObject vineHead;
  private GameObject branchHolder;


  private AudioSource audioSource;
  private ParticleSystem particleSystem;
  private LineRenderer lineRenderer;
  private List<Vector3> linePoints;

  public GameObject branchTemplate;

  public float nextSegment = 0f;
  public float segmentTime = 0.1f;
  public float defaultSegmentTime = 0.1f;

  public float rotationSpeed = 10f;

  private Vector3 nextPosition = Vector3.zero;

  private Vector3 startPosition;

  // Use this for initialization
  void Start()
  {
    lineRenderer = GetComponent<LineRenderer>();
    linePoints = new List<Vector3>();
  }

  public float Distance()
  {
    return Vector3.Distance(startPosition, vineHead.transform.position);
  }

  public void Reset()
  {
    if (vineHead != null)
    {
      Destroy(vineHead);
      Destroy(branchHolder);
    }

    segmentTime = defaultSegmentTime;

    vineHead = Instantiate(vineHeadTemplate, nextPosition, Quaternion.identity) as GameObject;
    vineHead.transform.parent = transform;
    vineHead.gameObject.AddComponent<Rigidbody2D>();

    GameObject particleHead = Instantiate(particleEffectTemplate, vineHead.transform.position, vineHead.transform.rotation) as GameObject;
    particleHead.transform.parent = vineHead.transform;

    particleSystem = particleHead.GetComponent<ParticleSystem>();
    audioSource = particleHead.GetComponent<AudioSource>();

    branchHolder = new GameObject();
    branchHolder.transform.parent = transform;


    linePoints = new List<Vector3>();

    nextPosition = Vector3.zero;
    startPosition = nextPosition;

    vineHead.transform.position = nextPosition;
    vineHead.transform.rotation = Quaternion.identity;
  }

  // Update is called once per frame
  void Update()
  {
    if (Vine.instance.IsPlaying())
    {
      Camera.main.GetComponent<Rigidbody2D>().AddTorque(-Input.GetAxis("Horizontal") * rotationSpeed);

      audioSource.volume = 0.5f;

      audioSource.mute = false;
      audioSource.panStereo = Input.GetAxis("Horizontal");
      
      //float rotScreen = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

      //Camera.main.transform.rotation = Camera.main.transform.rotation * Quaternion.AngleAxis(rotScreen, Vector3.back);

      if (Time.fixedTime > nextSegment)
      {
        UpdatePlant();
      }
    }
    else
    {
      if (audioSource != null)
      {
        audioSource.mute = true;
        particleSystem.Pause();
      }
    }
  }

  public void Branch()
  {
    GameObject branch = Instantiate(branchTemplate, nextPosition, Camera.main.transform.rotation) as GameObject;

    branch.transform.parent = branchHolder.transform;

    if(segmentTime > 0)
    {
      segmentTime = segmentTime - 0.005f;
    }
  }

  void UpdatePlant()
  {
    if (vineHead != null && Vine.instance.level.GetComponent<DynamicWall>().ContainsPoint(nextPosition))
    {
      Vine.instance.SetState(GameState.GAMEOVER);
    }
    else
    {
      linePoints.Add(nextPosition);
      lineRenderer.SetVertexCount(linePoints.Count);
      lineRenderer.SetPositions(linePoints.ToArray());

      lineRenderer.material.mainTextureScale = new Vector2(linePoints.Count * 0.1f, 1);

      vineHead.transform.position = nextPosition;
      vineHead.transform.rotation = Camera.main.transform.rotation;
      nextPosition = vineHead.GetComponent<PlayerSegment>().GetNextPosition();

      Camera.main.GetComponent<FollowCamera>().SetTarget(nextPosition + (Vector3.back * 100));

      nextSegment = Time.fixedTime + segmentTime;

    }
  }
}
