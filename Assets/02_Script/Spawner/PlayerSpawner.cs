using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTrs;
    [SerializeField] private PassiveInitializer passiveInitializer;

    private void Awake()
    {
        CharacterStat[] characterStats = Resources.LoadAll<CharacterStat>("CharcterStats");
        string playerName = PlayerPrefs.GetString("PlayerName");

        foreach (CharacterStat stat in characterStats)
        {
            if (stat.chatacterName == playerName)
            {
                var player = Instantiate(stat.player, spawnTrs.position, Quaternion.identity).GetComponent<PlayerBase>();

                passiveInitializer.SetPassiveInitializer();

                player.SetDataBase(stat.characterColor, stat.speedStat, stat.jumpStat,
                    stat.attackStat, stat.skillDelayStat, stat.attackDelayStat,
                    stat.GetPassiveTypes());

                break;
            }
        }
    }

    private void Start()
    {
        
    }
}
