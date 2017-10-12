using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileControl : MonoBehaviour 
{  
    
    [HideInInspector]
    public bool isMoving;

    private Transform xianxinzhe;
    private MoveCheck moveCheck;
    //private Transform realCube;

    private WaitForSeconds downSpeed;      // 默认下降速度间隔
    private WaitForSeconds changeSpeed;    // 加速移动时间间隔

    //left right down    up(Rotate)   
    private bool notDrop;
    private Vector3 direction;

    //记录按键时间
    private float time1;
    private float pressTime;


    void Start()
    {
        xianxinzhe = transform.Find("Xianxinzhe");
        moveCheck = xianxinzhe.GetComponent<MoveCheck>();
        //realCube = transform.Find("RealCube");

        downSpeed = new WaitForSeconds(1f);
        changeSpeed = new WaitForSeconds(0.2f);

        direction = Vector3.zero;     
        isMoving = true;

        notDrop = false;

        time1 = 0.8f;
        pressTime = 0;

        StartCoroutine(Drop());
        //StartCoroutine(Accelerate());
    }



    void Update()
    {
        if (isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                notDrop = true;
                direction = Vector3.left;
                MoveOnce();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                notDrop = true;
                direction = Vector3.right;
                MoveOnce();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                notDrop = true;
                direction = Vector3.down;
                MoveOnce();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                notDrop = true;
                direction = Vector3.up;
                RotateOnce();
            }
            else
            {
                notDrop = false;
            }
            

            moveCheck.CheckAndMove();
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            pressTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            StateReset();
        }
    }


    private void MoveOnce()
    {
        xianxinzhe.position += direction;
    }

    private void RotateOnce() {
        xianxinzhe.Rotate(0, 0, 90);
    }

    IEnumerator Drop()
    {
        while (isMoving)
        {
            if (notDrop)
            {
                yield return null;
            }
            else
            {
                xianxinzhe.position += Vector3.down;
                yield return downSpeed;
            }
        }
    }

    IEnumerator Accelerate() {
        while (isMoving) {
            if (pressTime > time1) {
                if (direction == Vector3.up) {
                    RotateOnce();
                }
                else {
                    MoveOnce();
                }
                yield return changeSpeed;
            }
            else
                yield return null;
        }
    }

    private void StateReset() {
        pressTime = 0;
        direction = Vector3.zero;
    }


}
