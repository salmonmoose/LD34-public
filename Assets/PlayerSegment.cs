using UnityEngine;
using System.Collections;

public class PlayerSegment : MonoBehaviour
{

  public Transform nextNode;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    Pickup pickup = other.GetComponent<Pickup>();

    if (pickup != null)
    {
      pickup.Activate();
    }
  }

  public Vector3 GetNextPosition()
  {
    return nextNode.position;
  }
}
