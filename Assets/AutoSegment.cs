using UnityEngine;
using System.Collections;

public class AutoSegment : MonoBehaviour
{
  public float nextSegment = 0f;
  public float segmentTime = 0.1f;

  public float rotationVariance = 50f;

  private Transform nextNode;

  private bool spawned = false;

  void Awake()
  {
    nextSegment = Time.fixedTime + segmentTime;
    nextNode = GetComponent<PlayerSegment>().nextNode;

    GetComponent<BoxCollider2D>().enabled = false;
  }

  // Use this for initialization
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {
    if (Vine.instance.IsPlaying() && !spawned && transform.lossyScale.x > 0.1f)
    {
      if (!Vine.instance.level.GetComponent<DynamicWall>().ContainsPoint(nextNode.position) && Time.fixedTime > nextSegment && !spawned)
      {
        int index = Random.Range(0, Vine.instance.segments.Length);

        GameObject segment = Instantiate(
          Vine.instance.segments[index],
          nextNode.position,
          transform.rotation * Quaternion.AngleAxis(
            (-0.5f + Mathf.PerlinNoise(Time.fixedTime, Time.fixedTime)) * rotationVariance,
            Vector3.forward
          )
        ) as GameObject;

        segment.AddComponent<AutoSegment>();

        segment.transform.parent = transform;

        segment.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);

        spawned = true;
        enabled = false;
      }
    }
  }
}
