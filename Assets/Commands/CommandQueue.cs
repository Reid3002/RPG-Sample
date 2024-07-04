using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour,IQueue<ICommand>
{
    private ICommand[] commands;
    private int index = 0;
    [SerializeField] private int size;


    private static CommandQueue instance;
    public static CommandQueue Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        StartQueue(size);
    }

    private void Update()
    {
        if (commands.Length != 0)
        {
            Dequeue().Run();
        }
    }

    public ICommand Dequeue()
    {
        if (!IsQueueEmpty())
        {            
            ICommand result = commands[0];

            for (int i = 0; i < index; i++)
            {
                commands[i] = commands[i + 1];
            }

            commands[index] = null;
            index--;

            return result;
        }

        Debug.Log("Tried to remove an item from an empty CommandQueue");
        return null;
    }

    public void Enqueue(ICommand item)
    {
        if (item != null)
        {
            commands[index] = item;
            index++;
        }
        else
        {
            Debug.Log("Tried to add a null item to the CommandQueue");
        }
    }

    public bool IsQueueEmpty()
    {
        if (commands[0] == null)
        {
            return true;
        }
        else { return false; }
    }

    public void StartQueue(int size)
    {
        commands = new ICommand[size];
        index = 0;
    }   




    //*******************************************************************
    /*


public class EnemyQueue : MonoBehaviour, IQueue<GameObject>
{
    private GameObject[] enemyQueue;
    private int index;
    [SerializeField] int initialSize = 50;

    void Awake()
    {
        StartQueue(initialSize);
    }

    void Update()
    {

    }

    public void StartQueue(int size)
    {
        enemyQueue = new GameObject[size];
        index = 0;
    }

    public void Enqueue(GameObject item)
    {
        if (item != null)
        {
            enemyQueue[index] = item;
            enemyQueue[index].SetActive(true);
            index++;
        }
        else
        {
            Debug.Log("Tried to add a null item to the Queue");
        }
    }

    public GameObject Dequeue()
    {
        if (!IsQueueEmpty())
        {
            enemyQueue[0].SetActive(false);
            GameObject result = enemyQueue[0];

            for (int i = 0; i < index; i++)
            {
                enemyQueue[i] = enemyQueue[i + 1];
            }

            enemyQueue[index] = null;
            index--;

            return result;
        }

        Debug.Log("Tried to remove an item from an empty Queue");
        return null;
    }

    public bool IsQueueEmpty()
    {
        if (enemyQueue[0] == null)
        {
            return true;
        }
        else { return false; }
    }
}*/

}
