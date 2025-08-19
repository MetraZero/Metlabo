# Logical Switch

複数の GameObject をまとめて ON/OFF するシンプルなスイッチ。

## 使い方（最短）
1. サンプルを導入（**Import Samples → Logical Switch Sample**）
2. シーンに Prefab を置く（または空Objectを作る）
3. `Udon Behaviour` の **Program Source** が割り当て済みであればそのまま動作
   - 未割り当ての場合は `Create → UdonSharp → Program Asset` を作成し、
     `Source Script` に `LogicalSwitch.cs` を指定 → UdonBehaviour の Program Source にセット

## インスペクター項目
- **targets: GameObject[]**  
  ON/OFF を切り替える対象
- **startOn: bool**  
  起動時に ON にするか

## 公開メソッド
- **Toggle()**  
  現在の状態を反転
- **SetActive(bool value)**  
  （必要なら公開）明示的に ON/OFF を設定

## サンプル
`Samples~/LogicalSwitch` に Prefab とシーンあり。

## 依存
- `com.vrchat.worlds >= 3.7.0`
- `com.vrchat.udonsharp >= 1.0.0`
