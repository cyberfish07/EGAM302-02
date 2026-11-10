using UnityEngine;

public class LightZombie : MonoBehaviour
{
    enum State
    {
        Patrol,
        Alerted,
        GoToSwitch,
        SwitchOff,
        ChasePlayer
    }
    public Transform[] patrolPoints;
    public float alertTime = 2f;
    public float pressTime = 1f;

    ZombieMotor motor;
    ZombiePerception perception;
    State state;
    LightSwitch targetSwitch;
    float stateTimer;
    int patrolIndex;

    void Awake()
    {
        motor = GetComponent<ZombieMotor>();
        perception = GetComponent<ZombiePerception>();
    }

    void Start()
    {
        StartPatrol();
    }

    void Update()
    {
        if (state != State.ChasePlayer && perception.Lit())
        {
            ChangeState(State.ChasePlayer);
        }
        
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Alerted:
                Alerted();
                break;
            case State.GoToSwitch:
                GoToSwitch();
                break;
            case State.SwitchOff:
                SwitchOff();
                break;
            case State.ChasePlayer:
                ChasePlayer();
                break;
        }
    }

    void ChangeState(State next)
    {
        state = next;
        stateTimer = 0f;
    }
     void StartPatrol()
    {
        motor.MoveTo(patrolPoints[patrolIndex].position);
        ChangeState(State.Patrol);
    }

    void Patrol()
    {
        LightSwitch seen = perception.SeenLight();
        if (seen != null)
        {
            targetSwitch = seen;
            motor.Stop();
            motor.FaceTowards(targetSwitch.targetLight.transform.position);
            ChangeState(State.Alerted);
            return;
        }

        if (motor.HasArrived())
        {
            patrolIndex = patrolIndex + 1;
            if (patrolIndex >= patrolPoints.Length) patrolIndex = 0;

            motor.MoveTo(patrolPoints[patrolIndex].position);
        }
    }

    void Alerted()
    {
        stateTimer += Time.deltaTime;
        
        if (targetSwitch != null && !targetSwitch.isOn)
        {
            StartPatrol();
            return;
        }
        
        if (stateTimer >= alertTime)
        {
            motor.MoveTo(targetSwitch.standPoint.position);
            ChangeState(State.GoToSwitch);
        }
    }

     void GoToSwitch()
    {
        if (!targetSwitch.isOn)
        {
            StartPatrol();
            return;
        }

        if (motor.HasArrived())
        {
            motor.Stop();
            motor.FaceTowards(targetSwitch.transform.position);
            ChangeState(State.SwitchOff);
        }
    }
    void SwitchOff()
    {
        stateTimer += Time.deltaTime;
        if (stateTimer < pressTime) return;

        if (targetSwitch.isOn)
        {
            targetSwitch.Interact();
        }
        StartPatrol();
    }

    void ChasePlayer()
    {
        if (!perception.Lit())
        {
            StartPatrol();
            return;
        }

        motor.MoveTo(perception.flashlight.transform.position);
    }

}
