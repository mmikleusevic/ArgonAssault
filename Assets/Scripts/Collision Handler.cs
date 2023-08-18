using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject _playerExplosionFX;
    [SerializeField] float _loadDelay = 1f;

    Effects _effects;
    GameObject _parentGameObject;

    readonly static string PLAYER = "Player";

    private void Start()
    {
        _parentGameObject = GameObject.FindWithTag(PLAYER);
        _effects = Effects.GetInstance();
    }

    private void OnTriggerEnter(Collider collider)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        _effects.ProcessFX(_playerExplosionFX, transform.position, _parentGameObject);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;

        Invoke("LoadLevelAgain", _loadDelay);
    }

    void LoadLevelAgain()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
