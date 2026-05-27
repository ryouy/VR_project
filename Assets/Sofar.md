# VRキャッチスティックゲーム — 実装仕様書

## 概要

本プロジェクトでは、UnityおよびXR Interaction Toolkitを用いて、VR空間内で遊べる反射神経ゲームを制作した。

プレイヤーは前方上部に吊り下げられた複数の棒の中から、ランダムに落下する棒を素早く掴み、スコアを獲得する。

ハンドトラッキングおよびVR UI操作を利用し、没入感のあるゲーム体験を目指した。

---

# 実装済み機能

---

# 1. ハンドトラッキング操作

## 内容

XR Hands を利用し、コントローラーを使用せずに手で直接ゲームを操作できるようにした。

## 実装内容

- XR Hands導入
- OpenXR設定
- XR Interaction Toolkit設定
- Hand Tracking有効化

---

# 2. スティック配置システム

## 内容

複数の棒を上部の支柱から吊り下げる形で配置した。

## 構成

```text
StickHolder
├── TopBar
├── Stick1
├── Stick2
├── Stick3
├── Stick4
└── Stick5
```

## 実装内容

- TopBarを土台として配置
- 棒を等間隔に配置
- Rigidbodyを利用した物理挙動設定

---

# 3. ランダム落下システム

## 内容

複数の棒の中からランダムに1本を選択し、一定時間ごとに落下させる。

## 実装内容

- StickManager.cs 作成
- InvokeRepeating による定期実行
- Random.Range によるランダム選択

## 動作

```text
一定時間経過
↓
ランダムな棒を選択
↓
重力ON
↓
落下開始
```

---

# 4. スティックキャッチ判定

## 内容

落下中の棒のみ掴めるように制御した。

## 実装内容

- XR Grab Interactable 使用
- grab.enabled による有効化制御
- 落下中のみ掴み可能

## 動作

```text
待機中
→ 掴めない

落下中
→ 掴める
```

---

# 5. スコアシステム

## 内容

棒をキャッチした際にスコアを加算し、UIへ表示する。

## 実装内容

- GameManager.cs 作成
- TextMeshProUGUI 使用
- AddScore() 実装

## UI表示

```text
SCORE : 0
```

---

# 6. FINAL SCORE表示

## 内容

全ての棒が落下終了した際に、最終スコアを表示する。

## 実装内容

- 全Stick非表示判定
- ゲーム終了処理
- FINAL SCORE 表示

## 表示内容

```text
FINAL SCORE : 5
```

---

# 7. STARTボタン

## 内容

STARTボタンを押した後にゲームを開始するようにした。

## 実装内容

- World Space Canvas 使用
- Buttonコンポーネント使用
- OnClick() に StartGame() 登録

## 動作

```text
START押下
↓
ランダム落下開始
```

---

# 8. RESETボタン

## 内容

ゲーム状態を初期化し、再スタート可能にした。

## 実装内容

- スコア初期化
- 全Stick初期位置復帰
- 落下停止

## 動作

```text
RESET押下
↓
ゲーム停止
↓
スコア初期化
↓
全Stick復帰
```

---

# 9. 効果音（SE）

## 内容

キャッチ成功時およびUIボタン押下時に効果音を再生した。

## 実装内容

- AudioSource 使用
- キャッチSE
- ButtonSE
- FinalScoreSE

## 使用箇所

- キャッチ成功
- STARTボタン
- RESETボタン
- FINAL SCORE表示

---

# 10. 発光演出

## 内容

棒をキャッチした際、一瞬だけ発光する演出を追加した。

## 実装内容

- URP Lit Material 使用
- Emission有効化
- _EmissionColor 制御

## 動作

```text
キャッチ
↓
発光
↓
SE再生
↓
棒消滅
```

---

# 11. ゲーム終了判定

## 内容

全ての棒が消滅した際にゲーム終了となるよう実装した。

## 実装内容

- activeSelf 判定
- CheckGameEnd() 実装

## 動作

```text
全Stick消滅
↓
ゲーム終了
↓
FINAL SCORE表示
```

---

# シーン構成

```text
Scene
├── XR Interaction Hands Setup
├── XR Device Simulator
├── Directional Light
├── Floor
├── StickHolder
│   ├── TopBar
│   ├── Stick1
│   ├── Stick2
│   ├── Stick3
│   ├── Stick4
│   └── Stick5
├── StickManager
├── Canvas
├── GameManager
└── AudioManager
```

---

# 使用スクリプト

## Stick.cs

- 棒落下制御
- Grab判定
- 発光演出
- SE再生
- 棒非表示制御

---

## StickManager.cs

- ランダム落下管理
- ゲーム開始／停止
- 終了判定

---

## GameManager.cs

- スコア管理
- UI更新
- FINAL SCORE表示
- リセット管理

---

## ButtonSE.cs

- UIボタンSE再生

---

# 使用技術

- Unity
- XR Interaction Toolkit
- XR Hands
- OpenXR
- TextMeshPro
- Rigidbody Physics
- AudioSource
- URP Lit Shader

---

# 到達目標

- VR空間でゲームを動作させる
- ハンドトラッキングで操作できる
- ランダム落下ゲームを成立させる
- UI操作を実装する
- スコア管理を実装する
- SEと演出を追加する
- VRゲームとして完成させる