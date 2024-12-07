using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Object/Skill Details")]
public class SkillDetails : ScriptableObject
{
    public int id;
    public Sprite image;
    public string title;
    public string description;
}
