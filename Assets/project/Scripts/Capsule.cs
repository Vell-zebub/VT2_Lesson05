using UnityEngine;

public class Capsule : MonoBehaviour
{
    [Header("Raycast設定")]
    [SerializeField] private float _raycastDistance = 2.0f; // 光線の長さ
    [SerializeField] private LayerMask _layerMask;    // 衝突レイヤー


    void Start()
    {
        
    }


    void Update()
    {
        bool isHit = Physics.Raycast(
            origin:transform.position,      // 光線の始点
            direction:Vector3.down,         // 光線の方向
            hitInfo: out RaycastHit hit,    // 衝突したObjectの情報
            maxDistance:_raycastDistance,   // 光線の長さ
            layerMask:_layerMask            // 衝突レイヤー
        );

        // 光線を可視化
        Debug.DrawRay(
            transform.position,             // 光線の始点
            Vector3.down * _raycastDistance,// 光線の方向
            Color.red                       // 光線の色
        );

        if(isHit)
        {
            Debug.Log($"衝突したObject:" +
            $"￥nName = {hit.collider.name}" +
            $"￥nPoint = {hit.point}" +
            $"￥nNormal = {hit.normal}" +
            $"￥nDistance = {hit.distance}"
            );
        }

        // 光線を可視化
        Debug.DrawRay(
            hit.point,                      // 光線の始点
            hit.normal,                     // 光線の方向
            Color.yellow                    // 光線の色
        );

        // 衝突したObjectの表面に移動
        Vector3 velocity = transform.forward; // Capsuleの進行方向

        // ”進行方向”と”法線”の外積 = 進行方向に右向きのベクトル
        Vector3 right = Vector3.Cross(velocity, hit.normal);

        // ”法線”と”右向きのベクトル”の外積 = スロープのベクトル
        // Vector3 slope = Vector3.Cross(hit.normal, right);
        Vector3 slope = Vector3.ProjectOnPlane(velocity, hit.normal);

        Debug.DrawRay(
            hit.point,                      // 光線の始点
            slope.normalized,               // 光線の方向
            Color.green                     // 光線の色
        );

        transform.position += slope * 30.0f * Time.deltaTime;
    }
}
