using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerPosition {
    public List<Vector3> position;

}

public class PlayerPositionDataController : MonoBehaviour
{
    [HideInInspector]
    public PlayerPosition playerPosClass;

    List<Vector3> allPlayersPos;
    public bool saveDataOnComputer;

    public bool drawGizmos;
    public float gizmosSize = 1f;

    string sceneName;
    
    private Dictionary<Vector3, int> visitCounts = new Dictionary<Vector3, int>();


    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);

        allPlayersPos = new List<Vector3>();
        playerPosClass.position = new List<Vector3>();
        StartCoroutine(GetPlayerPosition());

    }

    IEnumerator GetPlayerPosition() {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            playerPosClass.position.Add(transform.position);
            allPlayersPos.Add(transform.position);
        }
    }

    private void OnDisable()
    {
        playerPosClass.position.Add(new Vector3(0,-100000,0));
        SaveData();

    }

    public void SaveData() {
        if (saveDataOnComputer) {
            //get and save data on computer
            string path = Application.persistentDataPath + "/playerPosition.json";
            File.WriteAllText(path, JsonUtility.ToJson(allPlayersPos));
        }
    }
    

    private void OnDrawGizmos()
    {
        if (drawGizmos && allPlayersPos != null)
        {
            foreach (Vector3 pos in allPlayersPos)
            {
                if (!visitCounts.ContainsKey(pos))
                {
                    visitCounts[pos] = 1;
                }
                else
                {
                    visitCounts[pos]++;
                }
                int visits = visitCounts[pos];

                Color color;
                if (visits < 10)
                {
                    color = new Color(0, 1, 0, 0.2f);
                }
                else if (visits < 20)
                {
                    color = new Color(1, 1, 0, 0.2f);
                }
                else
                {
                    color = new Color(1, 0, 0, 0.2f);
                }

                Gizmos.color = color;
                Gizmos.DrawCube(pos, Vector3.one * gizmosSize);
            }
        }
    }
}