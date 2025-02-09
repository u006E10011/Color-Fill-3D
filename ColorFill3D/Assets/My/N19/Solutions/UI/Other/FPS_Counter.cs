using UnityEngine;

namespace N19
{
    public class FPS_Counter : MonoBehaviour
    {
        private static FPS_Counter _instance;
        public FPS_Counter Instance { get; private set; }

        [SerializeField] private float updateInterval = 0.5f;

        [SerializeField, Space(10)] private Vector2 _position;
        [SerializeField] private Vector2 _size;

        [SerializeField] private Color Color;

        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;
        private float fps;

        private GUIStyle textStyle = new();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);

                return;
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            timeleft = updateInterval;

            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color;
        }

        private void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                fps = (accum / frames);
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(_position.x, _position.y, _size.x, _size.y), fps.ToString("F2") + " FPS", textStyle);
        }
    }
}
