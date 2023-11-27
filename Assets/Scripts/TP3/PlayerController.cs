using UnityEditor;
using UnityEngine;

/// <summary>
/// Ecoute les entrées utilisateurs et déplace le personnage du joueur
/// en fonction.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Composant Unity permettant de déplacer facilement un objet dans
    /// un niveau.
    /// </summary>
    CharacterController _characterController;

    /// <summary>
    /// Vitesse de déplacement (en m/s) du personnage.
    /// </summary>
    [SerializeField]
    float _moveSpeed;


    public bool isPlayerAlive = true;

    [Header("Noise Things")]
    public Vector3 noiseLocation;
    public float timeSinceLastNoise = 0;
    public bool noiseWasMade = false;


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isPlayerAlive) {

            Vector3 inputDirection = new Vector3()
            {
                x = Input.GetAxis("Horizontal"),
                y = 0,
                z = Input.GetAxis("Vertical")
            };

            Vector3 velocity = inputDirection * _moveSpeed * Time.deltaTime;

            _characterController.Move(velocity);

            timeSinceLastNoise += Time.deltaTime;

            if (Input.GetButton("Fire1")) {

                if(timeSinceLastNoise > 20) {

                    OnNoiseMade();
                    timeSinceLastNoise= 0;

                }
            }
        }
    }

    
    public void OnNoiseMade() {
        // Debug.Log("Very Noisy");
        
        noiseLocation = this.transform.position; 
        noiseWasMade = true;
    }
}
