using UnityEngine;
using System.Collections;

public class DynamicWall : MonoBehaviour
{
  public GameObject backgroundTile;
  public GameObject foregroundTile;

  private GameObject pickup;

  public int tileViewX = 10;
  public int tileViewY = 10;

  public float threshold = 0.5f;

  public float nextPickup = 0f;
  public float pickupRate = 1.0f;

  private int xPos;
  private int yPos;

  private float seed;

  private Vector3 currentPosition;

  public GameObject[] tiles;

  void Awake()
  {
    nextPickup = Time.fixedTime + pickupRate;

    tiles = new GameObject[tileViewX * tileViewY];

    //Initialize all tiles here
    for (int y = 0; y < tileViewY; y++)
    {
      for (int x = 0; x < tileViewX; x++)
      {
        int index = x + (y * tileViewX);

        tiles[index] = Instantiate(foregroundTile, Vector3.zero, Quaternion.identity) as GameObject;
        tiles[index].transform.parent = transform;
      }
    }
  }

  // Use this for initialization
  void Start()
  {
    
  }

  public void Reset()
  {
    seed = Random.Range(0, 100);
    nextPickup = Time.fixedTime + pickupRate;
    Destroy(pickup);
  }

  public bool ContainsPoint(Vector3 point)
  {
    for (int y = 0; y < tileViewY; y++)
    {
      for (int x = 0; x < tileViewX; x++)
      {
        int index = x + (y * tileViewX);

        if (tiles[index].GetComponent<Tile>().ContainsPoint(point))
        {
          return true;
        };
      }
    }

    return false;
  }

  // Update is called once per frame
  void Update()
  {
    int xPosNew = (int)Mathf.Floor(Camera.main.transform.position.x) - (tileViewX / 2);
    int yPosNew = (int)Mathf.Floor(Camera.main.transform.position.y) - (tileViewY / 2);

    if (Vine.instance.IsPlaying() && pickup == null)
    {
      if (Time.fixedTime > nextPickup)
      {
        SpawnPickup();
      }
    }

    if(xPosNew != xPos || yPosNew != yPos)
    {
      xPos = xPosNew;
      yPos = yPosNew;

      for (int y = 0; y < tileViewY; y++)
      {
        for (int x = 0; x < tileViewX; x++)
        {
          int index = x + (y * tileViewX);

          tiles[index].transform.position = new Vector3(xPos + x, yPos + y, 0);

          if (Mathf.PerlinNoise((xPos + x) / 10f, (yPos + y) / 10f) > threshold)
          {
            tiles[index].GetComponent<Tile>().SetActive(true);
          }
          else
          {
            tiles[index].GetComponent<Tile>().SetActive(false);
          }
        }
      }
    }
  }

  Vector3 GenerateSpawnLocation()
  {
    Vector2 Offset = Random.insideUnitCircle * 5.0f;

    return new Vector3(
      Camera.main.transform.position.x + Offset.x,
      Camera.main.transform.position.y + Offset.y,
      -20
    );
  }

  void SpawnPickup()
  {
    int index = Random.Range(0, Vine.instance.pickups.Length);

    Vector3 location = GenerateSpawnLocation();

    while (ContainsPoint(location))
    {
      location = GenerateSpawnLocation();
    }

    pickup = Instantiate(
      Vine.instance.pickups[index],
      location,
      Quaternion.identity
    ) as GameObject;

    nextPickup = Time.fixedTime + pickupRate;
  }
}
