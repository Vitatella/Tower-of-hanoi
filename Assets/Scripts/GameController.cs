using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Disc[] allDiscs;
    [SerializeField] float diskHeight, choicedDiscHeight;
    [SerializeField] Tower[] towers;
    private Disc choicedDisc;
    void Start()
    {
        choicedDiscHeight = (allDiscs.Length + 1) * diskHeight;
        for (int i = 0; i < allDiscs.Length; i++)
        {
            allDiscs[i].transform.position = new Vector3(towers[0].GetPosition().x, towers[0].GetPosition().y + towers[0].discs.Count * diskHeight, towers[0].GetPosition().z);
            towers[0].discs.Add(allDiscs[i]);
        }
    }
    public void OnClick(Tower tower)
    {
        if (choicedDisc == null)
        {
            Debug.Log(tower.discs.Count);
            choicedDisc = tower.discs[tower.discs.Count - 1];
            choicedDisc.transform.position = new Vector3(tower.GetPosition().x, tower.GetPosition().y + choicedDiscHeight, tower.GetPosition().z);
            tower.discs.Remove(choicedDisc);
        }
        else
        {
            if (tower.discs.Count == 0 || choicedDisc.scale < tower.discs[tower.discs.Count - 1].scale)
            {
                choicedDisc.transform.position = new Vector3(tower.GetPosition().x, tower.GetPosition().y + tower.discs.Count * diskHeight, tower.GetPosition().z);
                tower.discs.Add(choicedDisc);
                choicedDisc = null;
            }
        }
    }
}
