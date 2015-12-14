using UnityEngine;
using System.Collections;

public class Butterfly : MonoBehaviour
{

  public float movementSpeed = 20f;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position += transform.up * Time.deltaTime * movementSpeed;

    if (Vector3.Distance(transform.position, Vine.instance.player.GetComponent<Player>().nextPosition) > 100)
    {
      Destroy(gameObject);
    }
  }
}
