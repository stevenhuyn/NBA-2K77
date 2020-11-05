using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    // Start is called before the first frame update
    void Awake() {
        // Ensures this is a singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }

        // Ensures that this keeps playing between scenes
        DontDestroyOnLoad(this.gameObject);
    }
}
