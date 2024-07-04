using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] List<GameObject> options;
    [SerializeField] GameObject optionsFunctionality;
    [HideInInspector]public bool canMove = true;
    private int index;

    [SerializeField] moveType Movement;
    private enum moveType
    {
        horizontal,
        vertical
    }

    private void OnEnable()
    {
        index = 0;
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        ItemSelectorMovement(canMove);
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            optionsFunctionality.GetComponent<IOptions>().OptionSelected(index);
        }
    }

    private void ItemSelectorMovement(bool active)
    {
        if (active)
        {
            if (((int)Movement == 0))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (index > 0 && options[index - 1].activeSelf == true)
                    {
                        index--;
                    }
                    else
                    {
                        index = options.Count - 1;
                    }

                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (index < options.Count - 1 && options[index + 1].activeSelf == true)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }

                }
            }

            else if(Movement.ToString() == "vertical")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (index > 0 && options[index - 1].activeSelf == true)
                    {
                        index--;
                    }
                    else
                    {
                        index = options.Count - 1;
                    }

                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (index < options.Count - 1 && options[index + 1].activeSelf == true)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }

                }
            }
            

            this.gameObject.transform.position = options[index].transform.position;
        }
    }

    public int GetResult()
    {
        return index;
    }

    public void ResetIndex()
    {
        index = 0;
    }
    
    public void RemoveOption(int option)
    {
        options.Remove(options[option]);
    }

    public void SetOptionsList(List<GameObject> optionsList)
    {
        options = optionsList;
    }

    public int GetNumberOfOptions()
    {
        return options.Count;
    }
}
