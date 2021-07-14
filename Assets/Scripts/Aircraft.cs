using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Aircraft : MonoBehaviour
{
    [Header("Ship Info")]
    public float radius;

    public float speed; // speed rate
    public float origenalSpeed;
    public Vector3 direction;    // speed direction

    [Space]
    public Vector3 acceleration;
    public FloatReference damping;

    [Space]
    public Transform top;
    public Transform down;
    public Transform left;
    public Transform right;
    private Vector4 edge; // top, down, left, right


    [Header("Events")]
    public UnityEvent OnCollideWithBlackHoleEvent;
    public UnityEvent OnShipCrashedEvent;
    public UnityEvent OnMeteorCrashedEvent;
    public UnityEvent OnParkInStationEvent;

    [Header("Score Info")]
    public IntVariableSO shipScore;
    public IntVariableSO meteorScore;

    [Space]
    public IntVariableSO currentScore;

    public string type;

    private bool isCrashed;
    private bool isCatched;
    private string crashTarget;

    public int meteorMassMultiple;


    // Start is called before the first frame update
    void Start()
    {
        acceleration = Vector3.zero;
        speed = origenalSpeed;
        isCrashed = false;

        if (!gameObject.GetComponent<SphereCollider>())
            gameObject.AddComponent<SphereCollider>();
        gameObject.GetComponent<SphereCollider>().radius = radius;

        if (top && down && left && right)
        {
            edge = new Vector4(top.position.z, down.position.z, left.position.x, right.position.x);
        }
        else
        {
            edge = new Vector4(5.0f, -5.0f, -5.0f, 5.0f);
        }


        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // apply gravity
        float timeClip = Time.deltaTime;
        float halfTime = timeClip / 2.0f;

        // calculate velocity
        Vector3 velocity = direction.normalized * speed;

        // half time velocity
        Vector3 midVelocity = velocity + acceleration * halfTime;

        // end time velocity
        Vector3 endVelocity = velocity + acceleration * timeClip;

        this.transform.position += midVelocity * timeClip;
        this.transform.forward = Vector3.RotateTowards(this.transform.forward, direction, 10f, 0f);
        direction = endVelocity.normalized;
        speed = endVelocity.magnitude;



        // apply damping
        if (speed != origenalSpeed)
        {
            float minus = speed - origenalSpeed;
            if (Mathf.Abs(minus) <= damping)
            {
                speed = origenalSpeed;
            }
            else if (minus < 0)
            {
                speed += damping;
            }
            else if (minus > 0)
            {
                speed -= damping;
            }
        }



        // edge test
        Vector3 pos = this.transform.position;
        TrailRenderer tr = this.GetComponentInChildren<TrailRenderer>();
        if (tr.enabled == false) { tr.enabled = true; }

        edge = GameObject.Find("MapManager").GetComponent<MapManger>().getEdge();
        if (pos.x >= edge.w)
        {
            tr.Clear();
            tr.enabled = false;
            this.transform.position = new Vector3(edge.z, pos.y, pos.z);
        }
        if (pos.x <= edge.z)
        {
            tr.Clear();
            tr.enabled = false;
            this.transform.position = new Vector3(edge.w, pos.y, pos.z);
        }
        if (pos.z >= edge.x)
        {
            tr.Clear();
            tr.enabled = false;
            this.transform.position = new Vector3(pos.x, pos.y, edge.y);
        }
        if (pos.z <= edge.y)
        {
            tr.Clear();
            tr.enabled = false;
            this.transform.position = new Vector3(pos.x, pos.y, edge.x);
        }

        // Physics Test
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == gameObject)
                continue;
            if (hitCollider.gameObject.tag == "Ship"
             || hitCollider.gameObject.tag == "Meteor")
            {
                isCrashed = true;
                crashTarget = hitCollider.gameObject.tag;
                hitCollider.SendMessage("ApplyCrash", true, SendMessageOptions.DontRequireReceiver);
                hitCollider.SendMessage("ApplyCrashTarget", gameObject.tag, SendMessageOptions.DontRequireReceiver);
                break;
            }
        }

        if (isCrashed)
        {
            if (crashTarget == "Ship")
            {
                if (currentScore)
                    currentScore.ApplyChange(-shipScore.Value);
                OnShipCrashedEvent.Invoke();
            }
            else if (crashTarget == "Meteor")
            {
                if (currentScore)
                    currentScore.ApplyChange(-meteorScore.Value);
                OnMeteorCrashedEvent.Invoke();
            }
            PlayParticleEffect();
            Destroy(gameObject);
        }

        if (isCatched)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyAcceleration(Vector3 extraGravity)
    {
        //Debug.Log("Apply Gravity");
        this.acceleration = extraGravity;
    }

    public void OnCollideWithBlackHole(BlackHoleController blackHole)
    {
        //Debug.Log("On Crashed");
        if (type == "Ship")
        {
            if (currentScore)
                currentScore.ApplyChange(-shipScore.Value);
            blackHole.absorbShip(1);
        }else if (type == "Meteor")
        {
            if (currentScore)
                currentScore.ApplyChange(meteorScore.Value);
            blackHole.absorbShip(meteorMassMultiple);
        }
        OnCollideWithBlackHoleEvent.Invoke();
        isCrashed = true;
    }

    public void OnParkInStation()
    {
        if (currentScore)
            currentScore.ApplyChange(shipScore.Value);
        OnParkInStationEvent.Invoke();
        this.isCatched = true;
    }

    public void ApplyCrash()
    {
        this.isCrashed = true;
    }

    public void ApplyCrashTarget(string targetType)
    {
        this.crashTarget = targetType;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void PlayParticleEffect()
    {
        GameObject effect = Instantiate(Resources.Load<GameObject>("Explode Particle"), transform.position, transform.rotation);
        effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }


}


