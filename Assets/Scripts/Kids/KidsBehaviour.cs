using System.Collections;
using UnityEngine;

public class KidsBehaviour : MonoBehaviour, InteractableBase
{
    public GameObject InteractUI;
    private GameObject EggObject;
    private GameObject TrickOrTreatText;
    private GameObject[] EggParticles;
    private Coroutine EggRoutine;
    private AudioSource KnockSound;
    private bool KidsAtDoor = false;

    private void Start()
    {
        EggObject = transform.Find("Eggs").gameObject;
        TrickOrTreatText = transform.Find("Trick or treat").gameObject;
        EggParticles = new GameObject[EggObject.transform.childCount];
        KnockSound = transform.Find("KnockEmitter").GetComponent<AudioSource>();

        for (int i = 0; i < EggObject.transform.childCount; i++)
        {
            EggParticles[i] = EggObject.transform.GetChild(i).gameObject;
            EggParticles[i].GetComponent<ParticleSystem>().Stop();
        }
        EggObject.SetActive(false);
        TrickOrTreatText.SetActive(false);
        
        HideInteractionUI();
    }

    public virtual void Interact()
    {
        if (!KidsAtDoor) return;

        KidsAtDoor = false;
        EggObject.SetActive(false);
        TrickOrTreatText.SetActive(false);
        HideInteractionUI();

        if (EggRoutine != null) StopCoroutine(EggRoutine);
        
        StartCoroutine(KidsCycle());
    }

    private void ThrowEggs()
    {
        foreach (GameObject Particle in EggParticles)
        {
            Particle.GetComponent<ParticleSystem>().Clear();
            Particle.GetComponent<ParticleSystem>().Play();
            Particle.GetComponent<AudioSource>().Play();
        }
        StartCoroutine(KidsCycle());
    }

    public IEnumerator KidsCycle()
    {
        KidsAtDoor = false;
        float waitTime = Random.Range(15f, 40f);
        yield return new WaitForSeconds(waitTime);

        KidsAtDoor = true;
        TrickOrTreatText.SetActive(true);
        KnockSound.Play();

        EggRoutine = StartCoroutine(EggTimer());
    }

    private IEnumerator EggTimer()
    {
        EggObject.SetActive(true);
        yield return new WaitForSeconds(6.5f);  
        if (KidsAtDoor) ThrowEggs();
    }

    public virtual void ShowInteractionUI()
    {
        if (InteractUI == null || InteractUI.activeSelf || !KidsAtDoor) return;
        InteractUI.SetActive(true);
    }

    public virtual void HideInteractionUI()
    {
        if (InteractUI == null || !InteractUI.activeSelf) return;
        InteractUI.SetActive(false);
    }
}
