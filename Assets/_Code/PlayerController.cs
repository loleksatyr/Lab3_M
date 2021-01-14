using UnityEngine;
using UnityEngine.Audio;

#pragma warning disable CS0649

public class PlayerController : MonoBehaviour {
    [SerializeField] Bandit banditController;
    [SerializeField] PlayerSubsystem[] subsystems;
    [SerializeField] Transform targetSensor;
    public static bool inForest = true;
    bool initialised;
    [SerializeField] AudioMixerSnapshot audioMixerSnapshot;
    [SerializeField] AudioMixerSnapshot audioMixerSnapshot1;
    [SerializeField] AudioMixerSnapshot audioMixerSnapshot2;
    [SerializeField] AudioMixerSnapshot audioMixerSnapshot3;
    PlayerInput input;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource forest;
    public bool IsDead => banditController.IsDead;


    void Awake() {
        Initialise();
    }

    void Initialise() {
        if (initialised)
            return;
        foreach (var subsystem in subsystems)
            subsystem.Initialise(this);
        banditController.JumpedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Jump);
        banditController.LandedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Landing);
        banditController.AttackedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Attack);
        banditController.DiedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Death);
        banditController.FootstepEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Footstep);
        banditController.AttackHitEvent += CheckForTargetsAndHit;
        input = new PlayerInput();
        banditController.Setup(input);
        initialised = true;
    }

    void CheckForTargetsAndHit() {
        var layer = LayerMask.NameToLayer("Enemy");
        var hit = Physics2D.Raycast(targetSensor.position, banditController.GetFacingDirection(), .1f, 1 << layer);
        if (hit) {
            var enemy = hit.transform.GetComponent<EnemyController>();
            enemy.DealDamage(25);
        }
    }

    void NotifySubsystemsAboutNewEvent(PlayerEventType eventType) {
        foreach (var playerSubsystem in subsystems)
            playerSubsystem.HandleEvent(eventType);
    }

    public void DealDamage(float damage) {
        banditController.TakeDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "forest") {
           inForest =! inForest;
        }
    }
    public void Update()
    {
        if (inForest)
        {
            if (DayNightController.timeOfDay == 0)
            {

                audioMixerSnapshot.TransitionTo(0.5f);

            }
            else {
                audioMixerSnapshot2.TransitionTo(1f);
                Debug.Log("change");

            }
            
        }
        else {
            if (DayNightController.timeOfDay == 0)
            {

                audioMixerSnapshot1.TransitionTo(0.5f);

            }
            else
            {
                audioMixerSnapshot3.TransitionTo(1f);

            }

        }
    }
}

public abstract class PlayerSubsystem : MonoBehaviour {
    protected PlayerController player;

    public void Initialise(PlayerController player) {
        this.player = player;
    }

    public abstract void HandleEvent(PlayerEventType eventType);
}

public enum PlayerEventType {
    Jump,
    Landing,
    Death,
    Attack,
    Footstep
}