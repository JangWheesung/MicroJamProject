using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CharacterStat))]
public class CharacterStatInspector : Editor
{
    private CharacterStat characterStat;

    private void OnEnable()
    {
        characterStat = (CharacterStat)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
