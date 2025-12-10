using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    void Update(){

    }

    private Rigidbody rb;
    public float speed;
    private int count;
    private Vector3 _startPos;
    private Quaternion _startRot;
    public TMP_Text countText;
    public TMP_Text winText;
    [SerializeField] private GameObject resetButton;     // 把 Canvas 里的 ResetButton 拖进来
    private GameObject[] _allPickups;                    // 缓存所有 Pick Up（含将来会被隐藏的）


        void Start(){
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        // 初始隐藏 Reset 按钮（只在胜利时显示）
        if (resetButton) resetButton.SetActive(false);

        // 缓存所有 Pick Up（此时它们都是激活的，之后即使被隐藏也有引用）
        _allPickups = GameObject.FindGameObjectsWithTag("Pick Up");
    }

    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement*speed);
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    
    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count>=12){
            winText.text = "You Win!";
        }
    }

    void Awake()
    {
    _startPos = transform.position;
    _startRot = transform.rotation;
    }

    public void ResetGameSoft()
    {
        // 1) 分数 & UI
        count = 0;
        SetCountText();
        if (winText) winText.text = "";

        // 2) 小球回到初始位置，清速度
        transform.SetPositionAndRotation(_startPos, _startRot);
        if (rb != null)
        {
            // 修正：Rigidbody 用 velocity（不是 linearVelocity）
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 3) 重新激活所有收集物 —— 用“缓存”恢复（即使它们现在是隐藏的也能拿到）
        if (_allPickups != null)
        {
            foreach (var p in _allPickups)
            {
                if (p) p.SetActive(true);
            }
        }

        // 4) 重置完把重置按钮继续隐藏（直到再次胜利）
        if (resetButton) resetButton.SetActive(false);
    }
}
