using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    // private List<Consumable> powerups;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;


    public IntVariable playerJumpSpeed;
    public IntVariable playerMoveSpeed;

    void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            ResetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    powerupIcons[i].SetActive(true);
                }
            }
        }
    }

    public void ResetInventory() {
        powerupInventory.Clear();
        powerupInventory.Setup(powerupIcons.Count);
        ResetPowerup();
    }
       
    public void ResetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }
   

    // public void AddPowerup(int index, Powerup p)
    // {
    //     if (index < powerupIcons.Count)
    //     {
    //         powerupIcons[index].SetActive(true);
    //         powerupInventory.Items[index] = p;
    //     }
    // }

    //called back by event
    public void AddPowerup(Powerup p)
    {
        Debug.Log("powerup added");
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

      void AddPowerupUI(int index, Texture t)
    {
        // powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void RemovePowerup(int index)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].SetActive(false);
            powerupInventory.Items[index] = null;
        }
    }

    public void ConsumePowerup(KeyCode keyCode) 
    {
        int index = 0;
        if(keyCode == MarioPlayer.ORANGE_MUSH) {
            index = 0;
        }
        else if(keyCode == MarioPlayer.RED_MUSH) {
            index = 1;
        }

        Powerup powerup = powerupInventory.Items[index];
        if(powerup) {
            playerJumpSpeed.SetValue(playerJumpSpeed.Value + powerup.absoluteJumpBooster);
            playerMoveSpeed.SetValue(playerMoveSpeed.Value + powerup.absoluteSpeedBooster);

            RemovePowerup(index);
            StartCoroutine(PowerupExpire(powerup, index));
        }
    }

    IEnumerator PowerupExpire(Powerup powerup, int index) {
        yield return new WaitForSeconds(powerup.duration);        
        playerJumpSpeed.SetValue(playerJumpSpeed.Value - powerup.absoluteJumpBooster);
        playerMoveSpeed.SetValue(playerMoveSpeed.Value - powerup.absoluteSpeedBooster);
    }

    // public void Cast(int i, GameObject p)
    // {
    //     if (powerupInventory.Items[i] != null)
    //     {
    //         powerupInventory.Items[i].ConsumedBy(p); 
    //         RemovePowerup(i);
    //     }
    // }
}
