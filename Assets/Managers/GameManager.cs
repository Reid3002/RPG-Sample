using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] SceneAsset worldScene;
    private GameObject worldSceneAssets;
    [SerializeField] SceneAsset battleScene;
    public GameObject currentEnemy;
    public EnemyParty currentEnemyParty;
    public bool battleTutorial = false;


    // Singleton***********************************

    private static GameManager Instance;

    public static GameManager instance  { get{ return Instance;} }




    //*************************************

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        worldSceneAssets = GameObject.FindGameObjectWithTag("WorldScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(worldScene.name));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(battleScene.name, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
        worldSceneAssets.SetActive(false);       

    }    

    public void EndBattle()
    {        
        if (currentEnemy != null)
        {
            Destroy(currentEnemy);
        }
        else { Debug.LogError("No enemy was assigned as currentEnemy.\n No enemy will be destroyed."); }       
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        worldSceneAssets.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName(worldScene.name))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(worldScene.name));
        }
        else if (scene == SceneManager.GetSceneByName(battleScene.name))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(battleScene.name));
            
        }        
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(worldScene.name, LoadSceneMode.Single);
    }

}
