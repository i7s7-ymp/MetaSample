| ファイル/ディレクトリ | 内容 | 役割 |
|---|---|---|
| **AssemblyInfo.cs** | C# Script | アセンブリに関する情報（バージョン、会社名など）を定義します。 |
| **Discovery/** | Directory | ネットワーク上のサーバーを自動的に検出するためのコンポーネントが含まれています。 |
| **Experimental/** | Directory | 実験的な機能や開発中のコンポーネントが含まれています。 |
| **GUIConsole.cs** | C# Script | ゲーム画面上にコンソールログを表示するためのシンプルなGUIコンポーネントです。 |
| **InterestManagement/** | Directory | ネットワークオブジェクトの同期範囲を管理（インタレストマネジメント）するためのコンポーネントが含まれています。 |
| **Mirror.Components.asmdef** | Assembly Definition | このディレクトリ内のスクリプトを `Mirror.Components` アセンブリとして定義します。 |
| **NetworkAnimator.cs** | C# Script | Animatorコンポーネントの状態をネットワーク経由で同期します。 |
| **NetworkLobbyManager.cs** | C# Script | プレイヤーがロビーで待機し、準備が整ったらゲームを開始する、というロビー機能を提供します。 |
| **NetworkLobbyPlayer.cs** | C# Script | `NetworkLobbyManager` と連携し、ロビー内の各プレイヤーを表します。 |
| **NetworkMatchChecker.cs** | C# Script | ネットワークごとのMatch IDをチェックし、意図しないクライアントの接続を防ぎます。 |
| **NetworkOwnerChecker.cs** | C# Script | オブジェクトの所有権（`isOwned`）に基づいて表示/非表示を切り替えるコンポーネントです。 |
| **NetworkPingDisplay.cs** | C# Script | サーバーとのPing値をUIに表示します。 |
| **NetworkProximityChecker.cs** | C# Script | プレイヤーからの距離に基づいて、オブジェクトのネットワーク可視性（表示/非表示）を制御します。 |
| **NetworkRoomManager.cs** | C# Script | `NetworkLobbyManager` に似ていますが、よりシンプルなルームベースのマッチメイキング機能を提供します。 |
| **NetworkRoomPlayer.cs** | C# Script | `NetworkRoomManager` と連携し、ルーム内の各プレイヤーを表します。 |
| **NetworkSceneChecker.cs** | C# Script | クライアントとサーバーが同じシーンにいることを確認し、異なる場合はクライアントを切断するなどの処理を行います。 |
| **NetworkTransform2k/** | Directory | `NetworkTransform` の代替となる、より高機能なTransform同期コンポーネントが含まれています。 |
