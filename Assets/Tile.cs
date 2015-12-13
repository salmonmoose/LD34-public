using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
  bool active;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public bool ContainsPoint(Vector3 point)
  {
    if (!active)
    {
      return false;
    }
    BoxCollider2D box = GetComponent<BoxCollider2D>();

    point = box.transform.InverseTransformPoint(point);

    Vector2 point2 = new Vector2(point.x, point.y) - box.offset;

    float halfX = (box.size.x * 0.5f);
    float halfY = (box.size.y * 0.5f);

    if (point.x < halfX && point.x > -halfX &&
      point.y < halfY && point.y > -halfY)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  //Toggle if this tile should be doing anything
  public void SetActive(bool active)
  {
    this.active = active;
    SpriteRenderer sr = GetComponent<SpriteRenderer>();

    if (sr != null)
    {
      sr.enabled = active;
    }
  }
}
