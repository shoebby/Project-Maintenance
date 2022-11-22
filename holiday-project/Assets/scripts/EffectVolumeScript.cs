using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVolumeScript : MonoBehaviour
{
    public static PlayerScript player;

    public enum Effect { gas, caustic, electric }

    public Effect thisEffect;

    private float elapsedTime;

    public int damage;

    private void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        elapsedTime = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            elapsedTime += Time.deltaTime;

            switch (thisEffect)
            {
                case Effect.gas:
                    if (elapsedTime >= 1f && !player.gasImmune)
                    {
                        elapsedTime %= 1f;
                        player.DeductHealth(damage);
                    }
                    break;
                case Effect.caustic:
                    if (elapsedTime >= 1f && !player.causticImmune)
                    {
                        elapsedTime %= 1f;
                        player.DeductHealth(damage);
                    }
                    break;
                case Effect.electric:
                    if (elapsedTime >= 1f && !player.electricityImmune)
                    {
                        elapsedTime %= 1f;
                        player.DeductHealth(damage);
                    }
                    break;
            }
        }
    }
}
