using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTrs;
    [SerializeField] private PassiveInitializer passiveInitializer;
    [SerializeField] private TMP_Text skillCooltimeText;

    private PlayerBase player;
    private CharacterStat playerStat;

    private float currentTime = 0f;

    private void Awake()
    {
        CharacterStat[] characterStats = Resources.LoadAll<CharacterStat>("CharcterStats");
        string playerName = PlayerPrefs.GetString("PlayerName");

        foreach (CharacterStat stat in characterStats)
        {
            if (stat.chatacterName == playerName)
            {
                var player = Instantiate(stat.player, spawnTrs.position, Quaternion.identity).GetComponent<PlayerBase>();
                
                this.player = player;
                playerStat = stat;

                passiveInitializer.SetPassiveInitializer();

                player.SetDataBase(stat.characterColor, stat.speedStat, stat.jumpStat,
                    stat.attackStat, stat.skillDelayStat, stat.attackDelayStat,
                    stat.GetPassiveTypes());

                break;
            }
        }
    }

    private void Update()
    {
        SkillTime();
    }

    public void SkillTime()
    {
        if (player.pSkill)
        {
            currentTime = 0f;

            skillCooltimeText.color = playerStat.characterColor;
            skillCooltimeText.text = playerStat.skillName;
            return;
        }

        currentTime += Time.deltaTime;

        skillCooltimeText.color = Color.white;
        skillCooltimeText.text = $"Ω∫≈≥ µÙ∑π¿Ã ({(playerStat.skillDelayStat - currentTime).ToString("F1")}s)";
    }
}
