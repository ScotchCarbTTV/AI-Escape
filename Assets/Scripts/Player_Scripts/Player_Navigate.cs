using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine;

/*
 * Player character navigation script:
 * Will use finite state machine to switch between 'moving', 'idle' and other states
 * While in 'moving' state will try to get to the navigation target * 
 */

public class Player_Navigate : MonoBehaviour
{
    //reference to 'navigation target'
    [SerializeField] private Player_Navpoint navPoint;

    private GameObject interactionTarget;

    //reference to the attaached navmesh agent
    private NavMeshAgent agent;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        if (!gameObject.TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.Log("You need a navmeshagent attached to this bad boy");
            gameObject.SetActive(false);
        }
        StateMachine = new StateMachine();
    }

    void Start()
    {
        StateMachine.SetState(new PlayerNavIdle(this));
        //EventManager.playerCanMoveEvent += SwitchMoveStates;
        EventManager.updateNavPositionEvent += UpdateNavTargetPosition;
        EventManager.leftCtrlClickInteractionEvent += MoveToInteractable;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.OnUpdate();
    }

    private void UpdateNavTargetPosition(Vector3 navPos)
    {
        agent.SetDestination(navPos);
    }

    private void MoveToInteractable(GameObject interaction)
    {
        interactionTarget = interaction;
        Vector3 lolPos = interactionTarget.transform.position - transform.position;
        lolPos = lolPos.normalized * 2;
        navPoint.gameObject.transform.position = interactionTarget.transform.position - lolPos;
        StateMachine.SetState(new PlayerNavMoveToInteract(this));
    }

    private void SwitchStates(bool canMove)
    {
        if(canMove == true)
        {
            //switch to the 'can move' state if we aren't already in it
            StateMachine.SetState(new PlayerNavMoving(this));
        }
        else
        {
            //switch to the idle state
            StateMachine.SetState(new PlayerNavIdle(this));
        }
    }

    public abstract class PlayerNavState : IState
    {
        protected Player_Navigate instance;

        public PlayerNavState(Player_Navigate _instance)
        {
            instance = _instance;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void OnUpdate()
        {
            
        }
    }

    public class PlayerNavIdle : PlayerNavState
    {
        public PlayerNavIdle(Player_Navigate _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {
            instance.agent.isStopped = true;
            //Debug.Log("Player entering idle nav state");            
        }

        public override void OnUpdate()
        {
            if(Vector3.Distance(instance.agent.transform.position, instance.navPoint.transform.position) > instance.agent.stoppingDistance)
            {
                instance.StateMachine.SetState(new PlayerNavMoving(instance));
            }
        }
    }

    public class PlayerNavMoving : PlayerNavState
    {
        public PlayerNavMoving(Player_Navigate _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {
            instance.agent.isStopped = false;
            instance.agent.SetDestination(instance.navPoint.transform.position);
        }

        public override void OnUpdate()
        {
            if(Vector3.Distance(instance.agent.transform.position, instance.navPoint.transform.position) < instance.agent.stoppingDistance)
            {
                instance.StateMachine.SetState(new PlayerNavIdle(instance));
                //EventManager.Instance._PlayerCanMove(true);
            }
        }

    }

    public class PlayerNavMoveToInteract : PlayerNavState
    {
        public PlayerNavMoveToInteract(Player_Navigate _instance) : base(_instance)
        {
        }

        IInteraction _interaction;

        public override void OnEnter()
        {
            instance.agent.isStopped = false;
            if (instance.interactionTarget != null) 
            {
                instance.agent.SetDestination(instance.interactionTarget.transform.position);
            }
            else
            {
                instance.StateMachine.SetState(new PlayerNavIdle(instance));
            }
        }

        public override void OnUpdate()
        {
            if(instance.interactionTarget.TryGetComponent<IInteraction>(out _interaction))
            {
                if(_interaction.CheckAvail() == true)
                {
                    _interaction.Activate();
                    instance.StateMachine.SetState(new PlayerNavIdle(instance));
                }
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.updateNavPositionEvent -= UpdateNavTargetPosition;
        EventManager.leftCtrlClickInteractionEvent -= MoveToInteractable;
    }

}
