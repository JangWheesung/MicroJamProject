using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTrs;

    private void Awake()
    {
        CharacterStat[] characterStats = Resources.LoadAll<CharacterStat>("CharcterStats");
        string playerName = PlayerPrefs.GetString("PlayerName");

        foreach (CharacterStat stat in characterStats)
        {
            if (stat.chatacterName == playerName)
            {
                var player = Instantiate(stat.player, spawnTrs.position, Quaternion.identity).GetComponent<PlayerBase>();
                player.SetDataBase(stat.characterColor, stat.speedStat, stat.jumpStat, stat.skillDelayStat, stat.attackDelayStat);

                break;
            }
        }
    }
}
