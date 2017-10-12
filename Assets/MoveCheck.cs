using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCheck : MonoBehaviour {
    private TileControl tileControl;
    private Transform realCube;

    private List<Transform> listTrans;
    private List<Transform> listRealTrans;

    private bool canMove;

    private void Awake()
    {
        tileControl = GetComponentInParent<TileControl>();
        realCube = this.transform.parent.Find("RealCube");

        listTrans = new List<Transform>();
        Transform[] trans = GetComponentsInChildren<Transform>();
        for (int i = 1; i < trans.Length; i++)
        {
            listTrans.Add(trans[i]);
        }

        listRealTrans = new List<Transform>();
        Transform[] realTrans = realCube.GetComponentsInChildren<Transform>();
        for(int i = 1; i < realTrans.Length; i++)
        {
            listRealTrans.Add(realTrans[i]);
        }

        canMove = true;
        
    }


    public void CheckAndMove()
    {
        foreach (var tran in listTrans)
        {
            int i = Mathf.RoundToInt(tran.position.x);
            int j = Mathf.RoundToInt(tran.position.y);

            i = i < 0 ? 0 : i;
            j = j < 0 ? 0 : j;
            i = i > 20 ? 20 : i;
            j = j > 11 ? 11 : j;

            if (SceneManage.Instance.cube_0_1[j, i] == 1)
            {
                canMove = false;
                break;
            }
        }

        if (canMove)
        {
            DoMove();
        }
        else
        {
            CancleMove();
        }
    }

    private void DoMove()
    {
        realCube.position = transform.position;
        realCube.rotation = transform.rotation;
    }

    private void CancleMove()
    {

        if (this.transform.position.y == realCube.position.y)
        {
            transform.position = realCube.position;
            transform.rotation = realCube.rotation;

            canMove = true;
        }
        else
        {
            tileControl.isMoving = false;

            foreach (var tran in listRealTrans)
            {
                int i = Mathf.RoundToInt(tran.position.x);
                int j = Mathf.RoundToInt(tran.position.y);
                SceneManage.Instance.cube_0_1[j, i] = 1;

                CubeManager.Instance.ReparentAndCheck(tran, j);
            }

            if (CubeManager.Instance.isfull) {
                CubeManager.Instance.MoveDown();
            }
            

            Destroy(this.transform.parent.gameObject);

            SceneManage.Instance.Create();
        }
    }
}
