using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    public List<GameObject> powerupIcons;
    private List<Consumable> powerups;

    void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Consumable>();
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
            powerups.Add(null);
        }
    }

    public void AddPowerup(int index, Consumable i)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].SetActive(true);
            powerups[index] = i;
        }
    }

    public void RemovePowerup(int index)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].SetActive(false);
            powerups[index] = null;
        }
    }

    public void Cast(int i, GameObject p)
    {
        if (powerups[i] != null)
        {
            powerups[i].ConsumedBy(p); 
            RemovePowerup(i);
        }
    }
}
