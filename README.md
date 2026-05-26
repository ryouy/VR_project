# VR Project

大学の VR 授業における最終課題の共同作業用 Unity プロジェクトです。

## このプロジェクトについて

このプロジェクトは、以下の授業資料を参考に、資料内の手順を最後まで進めた状態のファイルです。

- [授業資料（Google Slides）](https://docs.google.com/presentation/d/1hqpBQY4AmpkIB0ocIzcZ5_nT9L_BbbEGcCk4uLEjalg/edit?slide=id.g35b1630830e_0_169#slide=id.g35b1630830e_0_169)

これからは、このプロジェクトをベースとして最終課題の制作を進めていきます。

## 最初にすること

細かいセットアップや操作方法については、まず上記の授業資料を確認してください。

このプロジェクトを自分の PC で初めて使うときは、リポジトリをダウンロード（clone）します。GitHub Desktop を使う場合は、このリポジトリのページから `Code` > `Open with GitHub Desktop` を選択してください。

ターミナルを使う場合は、保存したい場所で次を実行します。

```bash
git clone https://github.com/ryouy/VR_project.git
```

clone 後、Unity Hub の `Add project from disk` からダウンロードした `VR_project` フォルダを選び、Unity で開いてください。初回起動時は必要なデータの生成に時間がかかる場合があります。

## 普段の作業の流れ

1. 作業を始める前に、他の人の変更を取り込みます。GitHub Desktop では `Fetch origin` / `Pull origin`、ターミナルでは以下を実行します。

```bash
git pull origin main
```

2. Unity で編集や動作確認を行います。

3. 作業が終わったら、何を変更したか分かるメッセージを付けて変更を共有します。GitHub Desktop では変更内容を確認して `Commit to main`、続いて `Push origin` を押します。

ターミナルを使う場合の例:

```bash
git add .
git commit -m "プレイヤーの移動処理を追加"
git push origin main
```

## 共同作業での注意

- Unity は同じシーンや prefab を同時に編集すると、変更が衝突しやすいです。作業を始める前に、誰がどの部分を触るか共有してから進めましょう。
- `Library` やビルド済みのアプリファイルは共有する必要がないため、このリポジトリには含めていません。
- `pull` や `push` でエラーが出た場合は、無理に進めずメンバーに共有してください。

## 開発環境

- Unity `2022.3.62f3`
