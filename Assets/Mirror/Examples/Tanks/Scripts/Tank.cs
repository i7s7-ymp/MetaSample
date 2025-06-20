using UnityEngine;
using UnityEngine.AI;

namespace Mirror.Examples.Tanks
{
    public class Tank : NetworkBehaviour
    {
        [Header("Components")]
        public NavMeshAgent agent;
        public Animator animator;
        public TextMesh healthBar;

        [Header("Movement")]
        public float rotationSpeed = 100;

        [Header("Firing")]
        public KeyCode shootKey = KeyCode.Space;
        public GameObject projectilePrefab;
        public Transform projectileMount;

        [Header("Stats")]
        [SyncVar] public int health = 4;

        // Update is called once per frame
        void Update()
        {
            // 体力バーの表示を常に更新します。
            // SyncVarのフック(hook)はクライアント側でしか呼び出されないため、サーバー側でも表示が更新されるようにUpdate内で処理しています。
            healthBar.text = new string('-', health);

            // このオブジェクトがローカルプレイヤー（自分自身が操作しているキャラクター）である場合のみ、以下の処理を実行します。
            // これにより、他のプレイヤーのキャラクターを誤って操作してしまうことを防ぎます。
            if (isLocalPlayer)
            {
                // 水平方向の入力（A, Dキーまたは←, →キー）を取得し、戦車を回転させます。
                float horizontal = Input.GetAxis("Horizontal");
                transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

                // 垂直方向の入力（W, Sキーまたは↑, ↓キー）を取得し、戦車を前進させます。
                // 後退はしないようにMathf.Maxで0以上の値に制限しています。
                float vertical = Input.GetAxis("Vertical");
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
                // NavMeshAgentの速度が0でなければ、Animatorの"Moving"パラメータをtrueにして移動アニメーションを再生します。
                animator.SetBool("Moving", agent.velocity != Vector3.zero);

                // 発射キー（デフォルトではスペースキー）が押されたら、サーバーに発射命令を送ります。
                if (Input.GetKeyDown(shootKey))
                {
                    // サーバー上でCmdFireメソッドを実行するように要求します。
                    CmdFire();
                }
            }
        }

        // [Command]属性: このメソッドはクライアントから呼び出されますが、実際の処理はサーバー上で実行されます。
        // 弾の発射など、ゲームの重要なロジックはサーバーで権威的に処理するのが一般的です。
        [Command]
        void CmdFire()
        {
            // サーバー上で弾のプレハブから新しいインスタンスを生成します。
            GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, transform.rotation);
            // NetworkServer.Spawn() を使って、生成した弾をネットワーク上のすべてのクライアントに同期（出現）させます。
            NetworkServer.Spawn(projectile);
            // 全てのクライアントに対して、発射時のエフェクト（アニメーション）を再生するように命令します。
            RpcOnFire();
        }

        // [ClientRpc]属性: このメソッドはサーバーから呼び出され、ネットワーク上のすべてのクライアントで実行されます。
        // "Rpc"はRemote Procedure Callの略です。
        [ClientRpc]
        void RpcOnFire()
        {
            // "Shoot"トリガーを起動して、射撃アニメーションを再生します。
            animator.SetTrigger("Shoot");
        }

        // [ServerCallback]属性: このメソッドはサーバー上でのみ呼び出されることを保証します。
        // クライアント側では物理演算のコールバックを無視することで、不正な処理を防ぎます。
        void OnTriggerEnter(Collider other)
        {
            // 衝突した相手がProjectileコンポーネントを持っているか（つまり、弾かどうか）をチェックします。
            if (other.GetComponent<Projectile>() != null)
            {
                // 体力を1減らします。
                // health変数は[SyncVar]属性を持っているため、サーバーで値が変更されると、その値は自動的にすべてのクライアントに同期されます。
                --health;
                // 体力が0になったら
                if (health == 0)
                    // NetworkServer.Destroy() を使って、この戦車オブジェクトをネットワーク上のすべてのクライアントから削除します。
                    NetworkServer.Destroy(gameObject);
            }
        }
    }
}
