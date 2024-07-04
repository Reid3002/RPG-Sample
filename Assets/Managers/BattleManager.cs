using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject VictoryScreen;
    [SerializeField] GameObject LoseScreen;
    [SerializeField] GameObject TutorialScreen;
    private bool battleTutorial = false;
    [SerializeField] Text xpGainedText;
    [SerializeField] BattlerFactoryData FactoryData;
    [SerializeField] private Transform[] partyMembersPos;
    [SerializeField] private Transform[] enemyPos;
    private int enemies = 0;
    private bool won = false;
    private bool lose = false;
    [SerializeField] private GameObject[] characterInfo;
    [SerializeField] private GameObject[] enemyInfo;
    private GameObject gameManager;
    public Battler activeCharacter;
    private int totalXP = 0;
    private bool startAiActions = false;
    public float totalTime = 1f; 
    private float currentTime = 0f;
    private bool isTimerRunning = true;

    private void Awake()
    {
        VictoryScreen.SetActive(false);
        LoseScreen.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        BattlerFactory Battlerfactory = new BattlerFactory(FactoryData);
        GameObject createdBattler;
        int zPosition = 10;

        for (int i = 0; i < partyMembersPos.Length; i++)
        {            
            if (i < gameManager.GetComponent<PartyManager>().CheckParty().Count)
            {
                if (gameManager.GetComponent<PartyManager>().GetCharacter(i) != null)
                {
                    createdBattler = Battlerfactory.Create(gameManager.GetComponent<PartyManager>().GetCharacter(i).characterClass.id);
                    createdBattler.transform.SetParent(partyMembersPos[i]);
                    createdBattler.transform.localPosition = Vector3.zero;
                    characterInfo[i].GetComponent<BattleInfo>().Initialize(gameManager.GetComponent<PartyManager>().GetCharacter(i), createdBattler.GetComponent<Battler>(), i);
                    createdBattler.GetComponent<Battler>().Initialize(gameManager.GetComponent<PartyManager>().GetCharacter(i), i, 0);
                    createdBattler.GetComponent<Battler>().ForceStatusUpdate();
                    createdBattler.GetComponent<Battler>().OnDeath += KillBattler;
                    createdBattler.GetComponent<SpriteRenderer>().sortingOrder = zPosition;
                    characterInfo[i].SetActive(true);
                    zPosition--;
                }
                else { Debug.Log("party member " + i + " does not exist"); }

            }
            else
            {
                characterInfo[i].SetActive(false);

            }
        }

        //Enemies---------------------------------
        float z = 0f;
        for (int i = 0; i < enemyPos.Length; i++)
        {            
            if (i < gameManager.GetComponent<GameManager>().currentEnemy.GetComponent<EnemyMovement>().enemyParty.enemies.Length)
            {
                if (gameManager.GetComponent<GameManager>().currentEnemy.GetComponent<EnemyMovement>().enemyParty.enemies[i] != null)
                {
                    createdBattler = Battlerfactory.Create(gameManager.GetComponent<GameManager>().currentEnemyParty.enemies[i].characterClass.id);
                    createdBattler.transform.SetParent(enemyPos[i]);
                    createdBattler.transform.localPosition = new Vector3(0,0,z);
                    z += 1f;
                    createdBattler.transform.localRotation = new Quaternion(0,0, 0, 0);
                    enemyInfo[i].GetComponent<BattleInfo>().Initialize(gameManager.GetComponent<GameManager>().currentEnemyParty.enemies[i], createdBattler.GetComponent<Battler>(), i);
                    createdBattler.GetComponent<Battler>().Initialize(gameManager.GetComponent<GameManager>().currentEnemyParty.enemies[i], i, 1);
                    createdBattler.GetComponent<Battler>().ForceStatusUpdate();
                    createdBattler.GetComponent<Battler>().OnDeath += KillBattler;
                    createdBattler.GetComponent<SpriteRenderer>().sortingOrder = zPosition;
                    enemyInfo[i].GetComponent<EnemyAI>().battler = createdBattler.GetComponent<Battler>();
                    enemyInfo[i].GetComponent<EnemyAI>().character = createdBattler.GetComponent<Battler>().character;
                    enemyInfo[i].GetComponent<EnemyAI>().infoPanel = enemyInfo[i].GetComponent<BattleInfo>();
                    enemyInfo[i].SetActive(true);
                    enemies++;
                    zPosition--;
                }
                else { Debug.Log("Enemy " + i + " does not exist"); }

            }
            else
            {
                enemyInfo[i].SetActive(false);

            }        


        }
    }

    private void Start()
    {       
        characterInfo[0].GetComponent<BattleInfo>().GetDropdown().Select();
        if (GameManager.instance.battleTutorial == false)
        {
            TutorialScreen.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && won == true)
        {
            gameManager.GetComponent<GameManager>().EndBattle();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && lose == true)
        {
            gameManager.GetComponent<GameManager>().LoseGame();
        }

        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
            
            if (currentTime >= totalTime)
            {                            
                isTimerRunning = false;
                LoadAiActions();
                Debug.Log("Timer ended");
            }
        }


    }

    private void LoadAiActions()
    {        
        List<Battler> aiAllies = new List<Battler>();
        List<Battler> aiEnemies = new List<Battler>();

        foreach (GameObject ally in enemyInfo)
        {
            if (ally.activeSelf == true)
            {
                aiAllies.Add(ally.GetComponent<BattleInfo>().GetBattler());
            }          

        }
        foreach (GameObject enemy in characterInfo)
        {
            if(enemy.activeSelf == true)
            {
                aiEnemies.Add(enemy.GetComponent<BattleInfo>().GetBattler());
            }        

        }
        foreach (GameObject enemy in enemyInfo)
        {
            if (enemy.activeSelf == true)
            {
                enemy.GetComponent<EnemyAI>().StartAI(aiAllies, aiEnemies);
            }
            
        }
    }
   

// Battler Deaths and Lose condition
    private void KillBattler(int ID, int side)
    {
        if(side == 0)
        {
            if (characterInfo[ID].GetComponent<BattleInfo>().GetBattler().isPlayer)
            {
                LoseScreen.SetActive(true);
                lose = true;
            }
            characterInfo[ID].SetActive(false);
            try { Destroy(characterInfo[ID].GetComponent<BattleInfo>().GetBattler().gameObject);}
            catch { }
        }
        else if (side == 1)
        {
            enemyInfo[ID].SetActive(false);
            totalXP += enemyInfo[ID].GetComponent<BattleInfo>().GetBattler().xpRewardedOnKill;
            try { Destroy(enemyInfo[ID].GetComponent<BattleInfo>().GetBattler().gameObject); }
            catch { }
            enemies--;
            if (enemies == 0)
            {
                WinBattle(totalXP);
            }
        }
    }

    private void WinBattle(int xpWon)
    {        
        xpGainedText.text = xpWon.ToString();
        VictoryScreen.SetActive(true);
        won = true;
        gameManager.GetComponent<PartyManager>().DistributeXP(xpWon);
        print("You win");
    }
}
