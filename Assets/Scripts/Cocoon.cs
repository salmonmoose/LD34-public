using UnityEngine;
using System.Collections;

public class Cocoon : Pickup
{
  public GameObject butterfly;
  public GameObject cocoonSplosion;

  public float growTime = 5.0f;
  private float startTime;

  private Animator animator;

  // Use this for initialization
  void Start()
  {
    startTime = Time.fixedTime;
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.fixedTime < startTime + growTime)
    {
      float delta = (Time.fixedTime - startTime) / growTime;

      transform.localScale = new Vector3(delta, delta, delta);
    }
    else
    {
      transform.localScale = Vector3.one;

      if (Random.Range(0, 1000) > 990)
      {
        animator.SetTrigger("Wriggle");
      }
    }
  }

  public override void Activate()
  {
    if (Time.fixedTime > startTime + growTime)
    {
      Vine.instance.CollectCocoon();
      Instantiate(butterfly, transform.position, Camera.main.transform.rotation);
      Instantiate(cocoonSplosion, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }
  }
}
