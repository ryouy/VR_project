# VRキャッチスティックゲーム 実装仕様書

## 概要

Unity と XR Interaction Toolkit / XR Hands を用いて、VR空間で遊べる反射神経ゲームを制作した。

プレイヤーは前方上部に配置された複数の棒の中から、ランダムに落下する棒を素早く掴む。キャッチできるとスコアが加算され、全ての棒が処理されると最終スコアが表示される。

## ゲームの流れ

```text
STARTボタンを押す
↓
ランダムな棒が落下する
↓
落下中の棒を手で掴む
↓
キャッチ成功ならスコア加算
↓
全ての棒が消える
↓
FINAL SCOREを表示
```

## 実装済み機能

### 1. ハンドトラッキング操作

XR Hands を利用し、コントローラーを使わずに手で直接操作できるようにした。

- XR Hands導入
- OpenXR設定
- XR Interaction Toolkit設定
- Hand Tracking有効化

### 2. スティック配置システム

複数の棒を上部の支柱から吊り下げる形で配置した。

```text
StickHolder
├── TopBar
├── Stick1
├── Stick2
├── Stick3
├── Stick4
└── Stick5
```

- TopBarを土台として配置
- 棒を等間隔に配置
- Rigidbodyを利用した物理挙動設定

### 3. ランダム落下システム

複数の棒の中から未使用の棒をランダムに1本選択し、一定時間ごとに落下させる。

- `StickManager.cs` で落下管理
- `Random.Range` によるランダム選択
- コルーチンによる1〜3秒のランダム待機
- 使用済みの棒は再度選ばれないように制御

### 4. スティックキャッチ判定

落下中の棒のみ掴めるようにした。

- `XR Grab Interactable` 使用
- `grab.enabled` による有効化制御
- 待機中は掴めない
- 落下中のみ掴める

### 5. スコアシステム

棒をキャッチした際にスコアを加算し、UIへ表示する。

- `GameManager.cs` でスコア管理
- `TextMeshProUGUI` 使用
- `AddScore()` でスコア加算
- `SCORE : 0` 形式で表示

### 6. FINAL SCORE表示

全ての棒が消えた際にゲーム終了とし、最終スコアを表示する。

- 全Stickの `activeSelf` を確認
- `CheckGameEnd()` で終了判定
- `ShowFinalScore()` で最終表示
- 表示前にSEを再生

```text
FINAL SCORE : 5
```

### 7. STARTボタン

STARTボタンを押すとゲームが開始される。

- World Space Canvas 使用
- Buttonコンポーネント使用
- OnClickに `StartGame()` を登録
- 二重開始を防止

### 8. RESETボタン

ゲーム状態を初期化し、再スタートできるようにした。

- スコア初期化
- 全Stickの初期位置復帰
- 落下停止
- 発光状態リセット
- UIを通常スコア表示へ戻す

### 9. 効果音

キャッチ成功時、UIボタン押下時、最終スコア表示時に効果音を再生した。

- `AudioSource` 使用
- キャッチSE
- ButtonSE
- FinalScoreSE

### 10. 発光演出

棒をキャッチした際、一瞬だけ発光する演出を追加した。

- URP Lit Material 使用
- Emission有効化
- `_EmissionColor` 制御
- 発光後に通常状態へ戻す

### 11. ミス判定

棒が床に当たった場合はミス扱いとし、一定時間後に棒を非表示にする。

- Floorタグとの衝突判定
- `OnCollisionEnter()` でミス検出
- `HideStick()` で非表示
- 非表示後に終了判定を実行

### 12. ゲーム筐体フィードバック演出

ゲームの状態やプレイヤーの結果が分かりやすくなるよう、ゲーム筐体の色と揺れによる演出を追加した。

- `GameMachine.cs` を追加
- 待機中はシアンで表示
- キャッチ成功時は黄色に変化して小さく揺れる
- ミス時は赤色に変化して大きく揺れる
- ゲーム終了時は虹色に変化する
- リセット時は待機状態へ戻る

### 13. 外観モデル追加

ゲーム空間の見た目を強化するため、建物やタワーの3Dモデルを追加した。

- `Building.fbx` 追加
- `tower.fbx` 追加
- 建物用マテリアル `red.mat` 追加
- `FinalProject.unity` へ外観オブジェクトを配置

### 14. WebGLビルドと公開

Unity WebGL向けにビルドし、ブラウザから遊べる形にした。

- `web/` フォルダにWebGLビルド一式を追加
- WebGL用の圧縮設定を有効化
- READMEにunityroomのプレイリンクを追加
- unityroomで公開

```text
https://unityroom.com/games/stick_catch
```

## シーン構成

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
├── GameMachine
├── Building
├── tower
└── AudioManager
```

## 使用スクリプト

### Stick.cs

- 棒の落下制御
- Grab判定
- キャッチ成功処理
- ミス判定
- 発光演出
- SE再生
- 棒の非表示制御
- 初期状態へのリセット

### StickManager.cs

- ゲーム開始／停止
- ランダム落下管理
- 使用済みStickの管理
- 全Stick終了判定

### GameManager.cs

- スコア管理
- UI更新
- FINAL SCORE表示
- リセット管理
- FinalScoreSE再生
- GameMachineの状態切り替え

### ButtonSE.cs

- UIボタンSE再生

### GameMachine.cs

- 待機状態の色管理
- キャッチ成功時の色変化と小さい揺れ
- ミス時の色変化と大きい揺れ
- ゲーム終了時の虹色演出
- リセット時の演出停止

## 使用技術

- Unity
- XR Interaction Toolkit
- XR Hands
- OpenXR
- TextMeshPro
- Rigidbody Physics
- AudioSource
- URP Lit Shader
- FBX Model
- Unity WebGL
- unityroom

## 到達目標

- VR空間でゲームを動作させる
- ハンドトラッキングで操作できる
- ランダム落下ゲームを成立させる
- UI操作を実装する
- スコア管理を実装する
- SEと演出を追加する
- リセットと最終スコア表示を実装する
- 外観モデルを追加してゲーム空間を作る
- WebGLビルドを作成して公開する
