using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Branch : MonoBehaviour
{

  private GameObject currentSegment;
  public GameObject cocoonTemplate;
  private GameObject cocoon;
  private Vector3 nextPosition;

  private LineRenderer lineRenderer;
  private List<Vector3> linePoints;

  public float rotationVariance = 200f;
  public float nextSegment = 0f;
  public float segmentTime = 0.4f;
  public int length = 200;

  // Use this for initialization
  void Start()
  {
    lineRenderer = GetComponent<LineRenderer>();
    linePoints = new List<Vector3>();
    nextPosition = transform.position;
    currentSegment = Instantiate(Vine.instance.segments[0], nextPosition, transform.rotation) as GameObject;

    currentSegment.transform.parent = transform;
  }

  // Update is called once per frame
  void Update()
  {
    if (Vine.instance.IsPlaying())
    {
      currentSegment.transform.localRotation = currentSegment.transform.localRotation * Quaternion.AngleAxis(
        (-0.5f + Mathf.PerlinNoise(Time.fixedTime, Time.fixedTime)) * rotationVariance,
        Vector3.forward
      );

      if (!Vine.instance.level.GetComponent<DynamicWall>().ContainsPoint(nextPosition) && Time.fixedTime > nextSegment && length > 0)
      {
        linePoints.Add(nextPosition);
        lineRenderer.SetVertexCount(linePoints.Count);
        lineRenderer.SetPositions(linePoints.ToArray());

        currentSegment.transform.position = nextPosition;

        nextPosition = currentSegment.GetComponent<PlayerSegment>().GetNextPosition();

        nextSegment = Time.fixedTime + segmentTime;

        length--;

        if (length == 100)
        {
          cocoon = Instantiate(cocoonTemplate, currentSegment.transform.position, Quaternion.identity) as GameObject;
        }
      }
    }
  }

  void OnDestroy()
  {
    Destroy(cocoon);
  }
}
