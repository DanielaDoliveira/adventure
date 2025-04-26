using UnityEngine;

[System.Serializable]
public class Item 
{
   public string id;
   public bool isShield;
   public bool isPotion;
   public Sprite image;
   public int attackPower;
   public int recoveryPower;
   public Transform prefab;
}
