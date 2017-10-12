using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeManager : MonoBehaviour {

    private static CubeManager _instance;
    public static CubeManager Instance {
        get {
            return _instance = _instance ?? new CubeManager();
        }
    }

    [HideInInspector]
    public bool isfull;

    private Transform[] raw;

    private int minClr;
    private int maxClr;
    private int maxRaw;

    private void Awake() {
        raw = new Transform[21];
        for (int i = 0; i < 21; i++) {
            raw[i] = transform.Find(string.Format("Box{0:D2}", i));
        }

        isfull = false;

        minClr = 20;
        maxClr = 0;
        maxRaw = 0;

        _instance = this;    
    }

    public void ReparentAndCheck(Transform tran,int index) {
        tran.parent = raw[index];
        if (index > maxRaw) {
            maxRaw = index;
        }
        FullCheck(index);
    }

    private void FullCheck(int index) {
        if (raw[index].childCount == 10) {
            isfull = true;

            if (index < minClr) {
                minClr = index;
            }
            if (index > maxClr) {
                maxClr = index;
            }

            Transform[] childTran = raw[index].GetComponentsInChildren<Transform>();
            raw[index].DetachChildren();
            for (int i = 1; i < childTran.Length; i++) {
                Destroy(childTran[i].gameObject);
                SceneManage.Instance.ClearRaw(index);
            }
        }
    }

    public void MoveDown() {
        for (int i = maxClr; i >= minClr; i--) {
            if (raw[i].childCount == 0) {
                for (int k = i; k < maxRaw; k++) {
                    Transform[] trans = raw[k + 1].GetComponentsInChildren<Transform>();
                    for (int j = 1; j < trans.Length; j++) {
                        trans[j].position += Vector3.down;
                        trans[j].parent = raw[k];
                    }
                    SceneManage.Instance.MoveDown(k);
                }
                maxRaw--;
            }
        }
            
        ResetMinMaxClr();
    }

    public void ResetMinMaxClr() {
        isfull = false;
        minClr = 20;
        maxClr = 0;
    }
}
