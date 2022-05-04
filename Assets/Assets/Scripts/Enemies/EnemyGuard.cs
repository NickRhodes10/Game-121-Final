using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGuard : BaseEnemy
{
  public GuardStates currentState = GuardStates.Init;

  [SerializeField] Vector2 waitTime;
  [SerializeField] LayerMask nonTransparentLayers;
  [SerializeField] private float detectionRange;
  [SerializeField] private float attackRange;
  [SerializeField] private float escapeRange;

  public PatrolPath patrolPath;
  private int currentWaypointIndex = 0;
  private Transform currentWaypoint;
  private Transform player;

  private float searchTimer = 0;
  bool isWaiting = false;

  public enum GuardStates
  {
    Init,
    Idle,
    Patrol,
    Searching,
    Chasing,
    Attacking,
    Dead
  }

  private void Awake()
  {
    //agent = GetComponent<NavMeshAgent>();
    player = GameObject.FindGameObjectWithTag("Player").transform;
  }

  private void Update()
  {
    switch (currentState)
    {
      case GuardStates.Init:
        currentState = GuardStates.Patrol;
        currentWaypointIndex = 0;
        currentWaypoint = patrolPath.WaypointsArray[currentWaypointIndex];
        agent.SetDestination(currentWaypoint.position);
        break;
      case GuardStates.Idle:
        break;
      case GuardStates.Patrol:
        Patrol();
        break;
      case GuardStates.Searching:
        Searching();
        break;
      case GuardStates.Chasing:
        Chasing();
        break;
      case GuardStates.Attacking:
        Attacking();
        break;
      case GuardStates.Dead:
        break;
    }
  }

  private void Patrol()
  {
    if (player == null)
      return;

    if (currentWaypoint != null) // check to see if we have a waypoint
    {
      if (Vector3.Distance(transform.position, player.position) <= detectionRange)// if enemy is within detection range of the player
      {
        agent.SetDestination(player.position);
        isWaiting = false;
        currentState = GuardStates.Searching;
        agent.isStopped = false;
        //set walk speed
        return;
      }

      if (Vector3.Distance(transform.position, currentWaypoint.position) <= 2)//if enemy has arrived at destination
      {
        //Debug.Log("Enemy has arrived at waypoint.");
        if (!isWaiting)
        {
          isWaiting = true;
          agent.isStopped = true;
          float randomTime = Random.Range(waitTime.x, waitTime.y);
          StartCoroutine(PatrolDelay(randomTime));
          return;
        }
      }
    }


  }

  private void Searching()
  {
    //Debug.Log("Searching for player.");
    if (CanSeePlayer())
      currentState = GuardStates.Chasing;

    //check if enemy is at destination
    if (Vector3.Distance(transform.position, agent.destination) < 2)
    {
      if (!isWaiting)
      {
        searchTimer = 0;
        isWaiting = true;
        agent.isStopped = true;
        //float rng = Random.Range(3, 5);
        //SearchDelay(rng);
        return;
      }
      else
      {
        searchTimer += Time.deltaTime;
        if (searchTimer > 5)
        {
          currentState = GuardStates.Patrol;
          isWaiting = false;
          agent.isStopped = false;
        }
      }
    }
  }

  private void Chasing()
  {
    //Debug.Log("Chasing the player");
    //Set destination to player
    agent.SetDestination(player.position);
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

    if (distanceToPlayer <= attackRange)
    {
      //Attack player
      Debug.Log(transform.name + " is attacking the player.");
    }
    else if (distanceToPlayer > escapeRange)
    {
      //player escapes, patrol.
      //Debug.Log("The player has escaped from " + transform.name);
      agent.SetDestination(currentWaypoint.position);
      currentState = GuardStates.Patrol;
    }
  }

  private void Attacking()
  {
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    if (distanceToPlayer > attackRange)
    {
      //Debug.Log("Player is no longer in attack range of " + transform.name);
      currentState = GuardStates.Chasing;
    }
    else
    {
      //attack player, deal damage.
    }
  }

  private IEnumerator SearchDelay(float delayTime)
  {
    yield return new WaitForSeconds(delayTime);
    agent.SetDestination(currentWaypoint.position);
    currentState = GuardStates.Patrol;
    agent.isStopped = false;
    isWaiting = false;
  }

  private IEnumerator PatrolDelay(float delayTime)
  {
    yield return new WaitForSeconds(delayTime);

    GetNextWaypoint();
    agent.SetDestination(currentWaypoint.position);
    agent.isStopped = false;
    isWaiting = false;
  }

  private void GetNextWaypoint()
  {
    currentWaypointIndex++;
    if (currentWaypointIndex >= patrolPath.WaypointsArray.Length)
    {
      currentWaypointIndex = 0;
    }
    currentWaypoint = patrolPath.WaypointsArray[currentWaypointIndex];
  }

  private bool CanSeePlayer()
  {
    Vector3 forward = transform.TransformDirection(Vector3.forward);
    Vector3 relativeDir = player.position - transform.position;
    if (Vector3.Dot(forward, relativeDir) > .2f) //if the player is in front of the guard
    {
      //Debug.Log("The player is in front of us.");
      //raycast to see if we can draw line of sight to the player
      RaycastHit hit;
      if (Physics.Raycast(transform.position, relativeDir, out hit, 20, nonTransparentLayers)) //shoot a raycast to the player, ignoring any transparent layers
      {
        if (hit.transform.tag == "Player") //if what we hit is the player
        {
          //Debug.Log("we have line of sight to the player.");
          return true;
        }
        else
        {
          //Debug.Log("We do not have line of sight to the player.");
          return false;
        }
      }
      else
      {
        //Debug.Log("we do not have line of sight to the player.");
        return false;
      }
    }
    else
    {
      //Debug.Log("The player is not in front of us");
      return false;
    }
  }
}
