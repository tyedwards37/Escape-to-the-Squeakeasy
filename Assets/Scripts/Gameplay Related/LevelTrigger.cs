using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        START,
        NEXT,
        PREVIOUS,
        END
    }

    public TriggerType triggerType;
    public int indexToGoTo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hamster"))
        {
            if(!Game.Instance.hasMadeFirstMove) return; // TODO: Remove the need for this when there is buggyness with transferring between levels

            Game.Instance.GetHamster().StopHamsterVelocity();

            switch(triggerType)
            {
                case TriggerType.START:
                    Debug.Log("No levels to go back to.");
                    break;
                    
                case TriggerType.NEXT:
                    Game.Instance.LoadNextLevel(indexToGoTo);
                    break;
                
                case TriggerType.PREVIOUS:
                    Game.Instance.LoadPreviousLevel(indexToGoTo);
                    break;

                case TriggerType.END:
                    Game.Instance.LoadWinningScene();
                    AudioInterface.Instance.PlayWinSound();
                    break;
            }
        }
    }
}
