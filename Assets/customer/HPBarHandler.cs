using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarHandler : MonoBehaviour
{
    public Image HealthBarImage;

    public int maxHP;

    List<Image> images = new List<Image>();

    public void SetMaxHp(int value)
    {
        maxHP = value;
    }
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(float value)
    {
        //value = value / (float)maxHP;
        HealthBarImage.fillAmount = value;
        if (value < 1 && value>0)
        {

            setChildrenActive(true);
        }
        else
        {

            setChildrenActive(false);
        }
        if (HealthBarImage.fillAmount < 0.2f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (HealthBarImage.fillAmount < 0.4f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        setChildrenActive(false);
    }

    void setChildrenActive(bool active)
    {

        foreach (var image in images)
        {
            image.gameObject.SetActive(active);
        }
    }

    private void Awake()
    {
        foreach (var image in GetComponentsInChildren<Image>())
        {
            images.Add(image);
        }
    }
}