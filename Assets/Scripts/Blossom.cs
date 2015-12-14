using UnityEngine;
using System.Collections;

public class Blossom : Pickup
{
  public GameObject petalSplosion;

  public override void Activate()
  {
    Vine.instance.CollectBlossom();
    Instantiate(petalSplosion, transform.position, transform.rotation);
    Destroy(gameObject);
  }
}