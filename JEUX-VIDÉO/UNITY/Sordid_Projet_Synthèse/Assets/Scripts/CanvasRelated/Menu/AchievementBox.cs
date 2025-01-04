using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AchievementBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Color notCompletedColor = new Color(0.7f,0.7f,0.7f,0.7f);
    TextMeshProUGUI title;
    TextMeshProUGUI description;
    Image icon;

    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        title = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        description = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    //TODO : set sprite, title and description with achievement
    public void SetAchievement(Achievement achievement)
    {
        icon.sprite = achievement.icon;
        SetSpriteHue(achievement.done);
        title.text = achievement.name;
        description.text = achievement.desc;
    }

    void SetSpriteHue(bool completed)
    {
        icon.color = completed ? Color.white : notCompletedColor;
    }
}
