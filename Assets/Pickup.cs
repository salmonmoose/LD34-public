using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

  public GameObject petalSplosion;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Activate()
  {
    Vine.instance.CollectBlossom();
    Instantiate(petalSplosion, transform.position, transform.rotation);
    Destroy(gameObject);
  }
}
