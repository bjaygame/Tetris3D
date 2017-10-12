using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour 
{
    private static SceneManage _instance;
    public static SceneManage Instance
    {
        get
        {
            return _instance = _instance ?? new SceneManage();
        }
        
    }

    [HideInInspector]
    public int[,] cube_0_1 = new int[21, 12];

    private GameObject[] goPrefabs;
    private int index;

    private void Awake()
    {
        _instance = this;

        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (i == 0 || j == 0 || j == 11)
                {
                    cube_0_1[i, j] = 1;
                }
                else
                {
                    cube_0_1[i, j] = 0;
                }
            }
        }

        goPrefabs = new GameObject[7];
        goPrefabs[0] = Resources.Load<GameObject>("Prefabs/Tiao");
        goPrefabs[1] = Resources.Load<GameObject>("Prefabs/Tian");
        goPrefabs[2] = Resources.Load<GameObject>("Prefabs/L01");
        goPrefabs[3] = Resources.Load<GameObject>("Prefabs/L02");
        goPrefabs[4] = Resources.Load<GameObject>("Prefabs/Z01");
        goPrefabs[5] = Resources.Load<GameObject>("Prefabs/Z02");
        goPrefabs[6] = Resources.Load<GameObject>("Prefabs/T");
    }


    private void Start() {
        Create();
    }


    public void Create() {
        index = Random.Range(0, 7);
        GameObject go = Instantiate(goPrefabs[index]);
        go.transform.parent = this.transform;
    }

    public void ClearRaw(int index) {
        for (int j = 1; j < 11; j++) {
            cube_0_1[index, j] = 0;
        }
    }

    public void MoveDown(int index) {
        for (int j = 1; j < 11; j++) {
            cube_0_1[index, j] = cube_0_1[index + 1, j];
            cube_0_1[index + 1, j] = 0;
        }
    }
}
