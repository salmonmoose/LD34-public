using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameState
{
  None,
  MENU,
  PLAYING,
  GAMEOVER
}

public class Vine : MonoBehaviour
{
  public static Vine instance { get; private set; }

  public GameObject player;
  public GameObject level;
  public GameObject gameOverPanel;
  public GameObject mainMenuPanel;
  public GameObject gamePlayPanel;

  private int collectedBlossom = 0;
  private int collectedCocoon = 0;

  public Text distanceDisplay;
  public Text blossomDisplay;

  public GameObject[] segments;
  public GameObject[] pickups;

  public GameState state = GameState.None;
  
  void Awake()
  {
    instance = this;
  }

  // Use this for initialization
  void Start()
  {
    SetState(GameState.MENU);
  }

  public bool IsPlaying()
  {
    return state == GameState.PLAYING;
  }

  public void StartGame()
  {
    ResetGame();

    SetState(GameState.PLAYING);
  }

  public void SetState(GameState newState)
  {
    if (newState != state)
    {
      state = newState;
    }
  }

  public void CollectBlossom()
  {
    player.GetComponent<Player>().Branch();
    collectedBlossom++;
  }

  public void CollectCocoon()
  {
    player.GetComponent<Player>().Slow();
    collectedCocoon++;
  }

  public void ResetGame()
  {
    player.GetComponent<Player>().Reset();
    level.GetComponent<DynamicWall>().Reset();
    collectedBlossom = 0;
    collectedCocoon = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if(state == GameState.MENU)
    {
      mainMenuPanel.SetActive(true);
      Camera.main.GetComponent<FollowCamera>().SetTarget(Vector3.zero);
    }
    else
    {
      mainMenuPanel.SetActive(false);
    }

    if(state == GameState.GAMEOVER)
    {
      gameOverPanel.SetActive(true);
    }
    else
    {
      gameOverPanel.SetActive(false);
    }

    if(state == GameState.PLAYING)
    {
      gamePlayPanel.SetActive(true);
      distanceDisplay.text = string.Format("Distance: {0}", player.GetComponent<Player>().Distance());
      blossomDisplay.text = string.Format("{0}", collectedBlossom);
    }
    else
    {
      gamePlayPanel.SetActive(false);
    }
  }
}
