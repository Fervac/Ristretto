using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public List<GameObject> bricks;
    public GameObject victory;
    public GameObject countdownPanel;

    public Material red;
    public Material yellow;
    public Material green;
    public Material blue;

    #region SINGLETON PATTERN
    public static Manager _instance;
    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Manager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Manager");
                    _instance = container.AddComponent<Manager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    public void SwitchShowWindow(GameObject window)
    {
        window.SetActive(!window.activeInHierarchy);
    }

    private void Awake()
    {
        Screen.SetResolution(720, 1280, true);
        Time.timeScale = 1;
    }

    public void CheckWin()
    {
        if (bricks.Count == 0)
        {
            SwitchShowWindow(victory);
            Time.timeScale = 0;
        }
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
