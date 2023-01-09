using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class GameFeedbackAttribute : Attribute
{
    public Color Color => color;
    public String MenuName => menuName;

    private Color color = Color.white;
    private string menuName;

    public GameFeedbackAttribute()
    {
        color = Color.white;
    }

    public GameFeedbackAttribute(string menu)
    {
        menuName = menu;
    }

    public GameFeedbackAttribute(string menu, int r, int g, int b)
    {
        menuName = menu;
        color = new Color(r / 255f, g / 255f, b / 255f);
    }

    public GameFeedbackAttribute(int r, int g, int b)
    {
        color = new Color(r / 255f, g / 255f, b / 255f);
    }

}