using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
  private Vector3 target;
  public float smoothTime;
  private Vector3 velocity;
  public Transform background;
  
  // Use this for initialization
  void Start()
  {
    transform.position = new Vector3(0, 0, -100);
  }

  // Update is called once per frame
  void Update()
  {
    if (Vine.instance.IsPlaying())
    {
      transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    background.position = new Vector3(
      transform.position.x,
      transform.position.y,
      0
    );

  }

  public void SetTarget(Vector3 target)
  {
    this.target = target;
  }
}
