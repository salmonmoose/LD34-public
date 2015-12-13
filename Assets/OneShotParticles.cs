using UnityEngine;
using System.Collections;

public class OneShotParticles : MonoBehaviour
{
  ParticleSystem particleSystem;

  // Use this for initialization
  void Start()
  {
    particleSystem = GetComponent<ParticleSystem>();
  }

  // Update is called once per frame
  void Update()
  {
    if (particleSystem && !particleSystem.IsAlive())
    {
      Destroy(gameObject);
    }
  }
}
