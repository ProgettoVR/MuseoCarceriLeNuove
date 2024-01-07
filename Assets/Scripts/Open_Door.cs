using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI; //inserito per le immagini

public class Open_Door : MonoBehaviour

{
    private Animator _animator;

    public Texture2D[] immagini;
    private RawImage rawImage;
    private bool _initialized = false;

    //public GameObject pianoImmagini;

    private bool _open = false;

    private int _leftClickCount = 0;

    public GameObject _target;

    //public SetVisible setVisible;

    void Start()
    {
        _animator = GetComponent<Animator>();

        // Ottieni il componente RawImage
        rawImage = GetComponent<RawImage>();
        rawImage.enabled = false;
        // Inizializza l'immagine con la prima immagine
        //rawImage.texture = immagini[0];
    }


    void Update()
    {

        /*if (!_initialized && Input.GetMouseButtonDown(0))
        {
            // Inizializza l'immagine solo al primo clic
            _initialized = true;
            rawImage.enabled = true;
            AggiornaImmagineCorrente();
        }
        */
        if (!_initialized)
        {
            // L'oggetto è inizializzato solo quando entra nel trigger.
            return;
        }
        // Aggiorna l'immagine corrente come prima.
        AggiornaImmagineCorrente();

        if (Input.GetMouseButtonDown(0))
        {

            _leftClickCount++;
            
            rawImage = GetComponent<RawImage>();
            rawImage.enabled = true;
            //rawImage.texture = immagini[_leftClickCount];
            rawImage.texture = immagini[0];
            //inserito per le immagini

            //da qui come prima 
            if (_leftClickCount >= 3)
            {
                rawImage.enabled = false;
                Open();
                Debug.Log("animazione partita");
                //_target.SetActive(true);
                Debug.Log("animazione attiva");
                _leftClickCount = 0;

            }
            // Mostra l'immagine corrispondente al numero di clic

            // MostraImmagineCorrente();
            AggiornaImmagineCorrente();

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Close();
            //StartCoroutine(AttendiCinqueSecondi());

            //Debug.Log("animazione disattivata");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Porta"))
        {
            // Quando entri nel trigger della porta, attiva il comportamento.
            //_target.SetActive(true);

            _target.SetActive(true);
            _initialized = true;
            rawImage.enabled = true;
            AggiornaImmagineCorrente(); // Aggiungi questa linea per aggiornare l'immagine all'entrata nel trigger.
        }

    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        if (other.CompareTag("Porta"))
        {
            _initialized = false;
            rawImage.enabled = false;
            _target.SetActive(false);
            StopAnimationAndReset();
            // Assicurati di chiudere l'animazione o eseguire altre azioni necessarie qui.
        }
    }
    /*void OnTriggerStay(Collider other)
    {
        // Quando rimani nel trigger, puoi reinizializzare qui se necessario.
        // Ad esempio, se vuoi che tutto si reinizializzi quando rientri nel trigger.
        if (other.CompareTag("Porta"))
        {
            _initialized = true;
            rawImage.enabled = true;
        }
    }*/


    public void Open () {

        if (_animator == null)
            return;
        //setVisible.AttivaOggettoConAnimazione();

        _open = true;

        _animator.SetBool("open", _open);

        }

    public void Close ()
    {
        if (_animator == null)
            return;
       // Debug.Log("Start di AltroScript");
       // setVisible.AttivaOggettoConAnimazione();
        _open = false;

        _animator.SetBool("open", _open);
    }
    /*private IEnumerator AttendiCinqueSecondi()
    {
        // Attendere 5 secondi
        yield return new WaitForSeconds(7);

        // Dopo l'attesa di 5 secondi, esegui questa azione
        _target.SetActive(false);
        rawImage.enabled = false;
    }
    */
    /*void MostraImmagineCorrente()
     {
         // Controlla che l'array di immagini non sia vuoto
         if (immagini.Length == 0)
         {
             Debug.LogError("L'array di immagini è vuoto. Assegna almeno una immagine nell'editor di Unity.");
             return;
         }

         // Mostra l'immagine corrispondente al contatore di clic
         rawImage.texture = immagini[_leftClickCount];

         // Nascondi l'immagine precedente (se esiste)
         if (_leftClickCount > 0)
         {
             rawImage.gameObject.SetActive(false);
             rawImage.gameObject.SetActive(true);
         }
     }*/
    public void StopAnimationAndReset()
    {
        if (_animator == null)
            return;

        // Il parametro "open" è quello che determina l'animazione.
        _open = false;
        _animator.SetBool("open", _open);
        _animator.Rebind(); // Reimposta l'animazione allo stato iniziale
        _animator.Update(0f); // Forza l'aggiornamento dell'animazione allo stato iniziale
        _leftClickCount = 0; // Reimposta il contatore
    }

    void AggiornaImmagineCorrente()
    {
        {
            // Controlla che l'array di immagini non sia vuoto
            if (immagini.Length == 0)
            {
                Debug.LogError("L'array di immagini è vuoto. Assegna almeno una immagine nell'editor di Unity.");
                return;
            }

            // Calcola l'indice dell'immagine corrispondente al numero di clic
            int indiceImmagine = _leftClickCount % immagini.Length;

            // Assegna la texture al RawImage solo se è stato inizializzato
            if (_initialized && rawImage.enabled)
            {
                rawImage.texture = immagini[indiceImmagine];
            }
        }
    }
}


