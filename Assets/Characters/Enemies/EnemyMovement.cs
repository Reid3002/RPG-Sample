using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveInterval = 7f;
    [SerializeField] EnemyParty Enemies;
    public EnemyParty enemyParty => Enemies;
    private int moveDistance;
    private Vector3 moveDirection;
    public float movementSpeed = 5f;
    private Vector3 startingPos;

    private float timer = 0;
    private bool canMove;
    private bool hasDirection;

    // Update is called once per frame
    void Update()
    {
        if (timer < moveInterval)
        {
            timer += Time.deltaTime;            
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            Move();
        }
    }
    


    private void Move()
    {
        if (!hasDirection)
        {
            startingPos = transform.position;
            int randomDirection = Random.Range(1, 5);
            moveDistance = Random.Range(1, 6);
            
            switch (randomDirection)
            {
                case 1:
                    moveDirection = Vector3.up;
                    break;

                case 2:
                    moveDirection = Vector3.down;
                    break;

                case 3:
                    moveDirection = Vector3.left;
                    break;

                case 4:
                    moveDirection = Vector3.right;
                    break;

            }
            hasDirection = true;            
        }

        //if(transform.position != startingPos+ moveDirection /* moveDistance*/)
        //{
        //    transform.position += moveDirection * movementSpeed * Time.deltaTime;
        //}
        int b =0;
        for (int i = 0;  i < moveDistance; i++)
        {
            b++;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;
        }
        if(b >= moveDistance)
        {
            canMove = false;
            timer = 0f;
            hasDirection = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentEnemy = this.gameObject;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentEnemyParty = Enemies;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().StartBattle();
        }
    }
}
