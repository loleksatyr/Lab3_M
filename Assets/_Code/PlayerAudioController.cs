using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioController : PlayerSubsystem {
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip land;
    [SerializeField] AudioClip sword;
    public AudioClip[] FootstepList;
 
    
    public override void HandleEvent(PlayerEventType eventType) {
        switch (eventType) {
            case PlayerEventType.Jump:
                //TODO play jump sound
                audioSource.PlayOneShot(jump);
                break;
            case PlayerEventType.Landing:
                //TODO play landing sound
                audioSource.PlayOneShot(land);
                break;
            case PlayerEventType.Death:
                break;
            case PlayerEventType.Attack:
                audioSource.PlayOneShot(sword);
                break;
            case PlayerEventType.Footstep:
                //TODO play random footstep sound
                audioSource.PlayOneShot(FootstepList[Random.Range(0,5)]);
                break;
        }
    }
}
