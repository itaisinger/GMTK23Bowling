using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAnimManager : MonoBehaviour
{
      public List<GameObject> toDestroy = new List<GameObject>();
      private void Awake() 
      {
         DontDestroyOnLoad(gameObject);
      }
  public void CahngeLevel()
  {
    foreach(var obj in toDestroy)
    {
        Destroy(obj);
    }
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
  public void DesGameObj()
  {
    Destroy(gameObject);
  }
}
