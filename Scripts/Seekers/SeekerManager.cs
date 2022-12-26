
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SeekerManager : MonoBehaviour
{ 
    private Transform _playerPos;
    private SeekerBaseState CurrentBaseState { get; set; }
    private NavMeshAgent _agent;
    private  Dictionary<Type, SeekerBaseState> _availableStates= new ();
    public event Action SeekerHit;
    private TextMeshPro _alertIndicator;
    private readonly WaitForSeconds _waitTime =new (7f);
    private void Awake()
    {
        _alertIndicator = GetComponentInChildren<TextMeshPro>();
        _playerPos = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        InitializeStates();
    }

    private void InitializeStates()
    {
        var template = new Dictionary<Type, SeekerBaseState>
        {
            {typeof(SeekerSearchState),new SeekerSearchState(this)},
            {typeof(SeekerDeadState),new SeekerDeadState(this)},
            { typeof(SeekerAlertState),new SeekerAlertState(this) }
        };
        SetStates(template);
    }

    private void SetStates(Dictionary<Type,SeekerBaseState> states)
    {
        _availableStates = states;
        if (CurrentBaseState != null) return;
        var firstState = _availableStates[typeof(SeekerSearchState)];
        ChangeState(firstState.GetType());
    }

    private void Update() => CurrentBaseState?.Tick();

    private void ChangeState(Type desiredState)
    {
        CurrentBaseState?.ExitState();
        CurrentBaseState = _availableStates[desiredState];
        CurrentBaseState?.EnterState();
    }

    public void StartPatrol() => StartCoroutine(PatrolCoroutine());

    public void StopPatrol() => StopCoroutine(PatrolCoroutine());

    private IEnumerator PatrolCoroutine()
    {
        while (true)
        {
            var randomDirection = Random.insideUnitSphere * 25;
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out var hit, 25,NavMesh.AllAreas);
            var finalPosition = hit.position;
            _agent.SetDestination(finalPosition);
            yield return _waitTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<Ball>(out _)) return;
        DetectBallHit();
        SeekerHit?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AlertArea>(out _))
        {
            AlertHit();
        }
        
    }
    public void KillSeeker() => transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBounce).OnComplete(() => gameObject.SetActive(false));
    private void DetectBallHit() => ChangeState(typeof(SeekerDeadState));
    private void AlertHit() => ChangeState(typeof(SeekerAlertState));
    private void OnToSearchState()=> ChangeState(typeof(SeekerSearchState));
    public void AlertCoroutine() => StartCoroutine(Alert());
    
    private IEnumerator Alert()
    {
        _agent.ResetPath();
        _alertIndicator.enabled = true;
        var playerPosition = _playerPos.position;
        _agent.SetDestination(playerPosition + Vector3.one*(Random.Range(1, 3)));
        yield return new WaitForSeconds(5f);
        _alertIndicator.enabled = false;
        OnToSearchState();
        
    }
    
}