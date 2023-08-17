using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _loadDelay = 1f;
    [SerializeField] ParticleSystem _playerExplosion;
    private void OnTriggerEnter(Collider collider)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        _playerExplosion.Play();

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
