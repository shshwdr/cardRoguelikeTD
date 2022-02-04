using UnityEngine;

public interface IClickable
{
    Transform transform { get; }
    GameObject gameObject { get; }
    public void hideInfo();


    public void showInfo();

}