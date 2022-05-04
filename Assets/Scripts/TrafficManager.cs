using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public GameObject[] Cars;
    public int[] CoordinatesX;
    public int[] CoordinatesY;
    public int currentPos;
    //public int currentY;
    public bool move;
    public bool triggerMove;
    public bool isGameOver;
    private int index;
    //public int curPos;
    public int curDir;
    public string movement;
    public float destinationX;
    public float destinationZ;
    //private Rigidbody rb;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        move = false;
        triggerMove = false;
        currentPos = 0;
        movement = "1";
        CoordinatesX = new int[8]; // cutomize size to suit n-sided grid
        CoordinatesY = new int[8]; // cutomize size to suit n-sided grid
        CoordinatesX[0] = 3;
        CoordinatesY[0] = 3;
        curDir = 0;
        SetNextDestination(CoordinatesX[currentPos], CoordinatesY[currentPos]);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerMove && !isGameOver)
        {
            if (move)
            {
                Cars[index].transform.position = Vector3.MoveTowards(Cars[index].transform.position, target, 1f * Time.deltaTime);
                if (curDir != movement.Length - 1 && Cars[index].transform.position == target)
                {
                    curDir++;
                    SetNextDestination(CoordinatesX[currentPos], CoordinatesY[currentPos]);
                }
                else if (curDir == movement.Length - 1 && Cars[index].transform.position == target)
                {
                    move = false;
                    if (index != 16)  // cutomize size to suit n-sided grid
                        Cars[index].SetActive(false);
                }
            }

            if (!move)
            {
                currentPos++;
                curDir = 0;
                triggerMove = false;
                PickNextCarToMove();
                SetNextDestination(CoordinatesX[currentPos], CoordinatesY[currentPos]);
            }
        }
    }

    public void SetNextDestination(int x, int y)
    {
        index = x * 4 + y;
        //curDir = 0;
        if (movement[curDir] == '1')
        {
            destinationX = Cars[index].transform.position.x;
            destinationZ = Cars[index].transform.position.z - 1;
        }
        else
        {
            destinationZ = Cars[index].transform.position.z;
            destinationX = Cars[index].transform.position.x + 1;
        }
        target = new Vector3(destinationX, Cars[index].transform.position.y, destinationZ);
        Debug.Log(target);
        //rb = Cars[index].GetComponent<Rigidbody>();
        move = true;
    }

    public void MoveCar()
    {
        triggerMove = true;
    }

    public void PickNextCarToMove()
    {
        if (CoordinatesX[currentPos - 1] - 1 < 0 && CoordinatesY[currentPos - 1] - 1 < 0)
        {
            //isGameOver = true;
            movement = "0" + movement;
            CoordinatesX[currentPos] = 3;  // cutomize size to suit n-sided grid
            CoordinatesY[currentPos] = 4;  // cutomize size to suit n-sided grid
            //return;
        }
        else if(index == 16)  // cutomize size to suit n-sided grid
        {
            isGameOver = true;
            //return;
        }
        else if (CoordinatesX[currentPos - 1] - 1 < 0)
        {
            movement = "0" + movement;
            CoordinatesX[currentPos] = CoordinatesX[currentPos - 1];
            CoordinatesY[currentPos] = CoordinatesY[currentPos - 1] - 1;
        }else if (CoordinatesY[currentPos - 1] - 1 < 0)
        {
            movement = "1" + movement;
            CoordinatesY[currentPos] = CoordinatesY[currentPos - 1];
            CoordinatesX[currentPos] = CoordinatesX[currentPos - 1] - 1;
        }
        else
        {
            int cur = Random.Range(0, 2);
            if(cur == 0)
            {
                movement = "0" + movement;
                CoordinatesX[currentPos] = CoordinatesX[currentPos - 1];
                CoordinatesY[currentPos] = CoordinatesY[currentPos - 1] - 1;
            }
            else
            {
                movement = "1" + movement;
                CoordinatesY[currentPos] = CoordinatesY[currentPos - 1];
                CoordinatesX[currentPos] = CoordinatesX[currentPos - 1] - 1;
            }
        }
    }
}
