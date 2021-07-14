
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpaceStation : MonoBehaviour
{
    [Header("Station Info")]
    public float radius;
    public int capacity;
    public int holdAmount;

    [Header("Mass Info")]
    public int massMultiple;

    private bool isCrashed;
    private bool isFull;

    [Header("Events")]
    public UnityEvent OnCollideWithBlackHoleEvent;
    public UnityEvent OnStationCrashedEvent;
    public UnityEvent OnStationFullEvent;

    [Header("Score Info")]
    public IntVariableSO stationScore;
    public IntVariableSO shipScore;

    [Space]
    public IntVariableSO currentScore;

    [Space]
    public Text capacityText;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        holdAmount = 0;
        isCrashed = false;
        isFull = false;

        if (!gameObject.GetComponent<SphereCollider>())
            gameObject.AddComponent<SphereCollider>();
        gameObject.GetComponent<SphereCollider>().radius = radius;

        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollieder in hitColliders)
        {
            if (hitCollieder.gameObject == gameObject)
                continue;
            if (hitCollieder.gameObject.tag == "Ship")
            {
                holdAmount += 1;
                UpdateCapacityUI(holdAmount);
                //
                PlayParticleEffect();
                //

                if (holdAmount >= capacity)
                {
                    isFull = true;
                }
                hitCollieder.SendMessage("OnParkInStation", null, SendMessageOptions.DontRequireReceiver);
                break;
            } else if (hitCollieder.gameObject.tag == "Meteor")
            {
                isCrashed = true;
                break;
            }
        }

        if (isCrashed)
        {
            if (currentScore)
                currentScore.ApplyChange(-(stationScore.Value + holdAmount * shipScore.Value));

            // effect
            GameObject effect = Instantiate(Resources.Load<GameObject>("Explode Particle"), transform.position, transform.rotation);
            effect.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            OnStationCrashedEvent.Invoke();
            Destroy(gameObject);
        }

        if (isFull)
        {
            if (currentScore)
                currentScore.ApplyChange(stationScore.Value);
            OnStationFullEvent.Invoke();
            Destroy(gameObject);
        }
    }


    public void OnCollideWithBlackHole(BlackHoleController blackHole)
    {
        blackHole.absorbShip(massMultiple + holdAmount);
        OnCollideWithBlackHoleEvent.Invoke();
        isCrashed = true;
    }

    public void PlayParticleEffect()
    {
        particle.Play();
    }

    public void UpdateCapacityUI(int amount)
    {
        capacityText.text = amount.ToString() + " / " + capacity.ToString();
    }

    private void DisappearStationEffects()
    {

    }

}
