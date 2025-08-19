using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public enum LogicMode
{
    AND,
    OR,
    NAND,
    NOR
}

public class LogicalSwitch : UdonSharpBehaviour
{
    [Header("Global Switch")]
    [Tooltip("チェックが外れている場合、対象オブジェクトは常にOFFになります。")]
    public bool globalSwitch = true;

    [Header("Target Objects")]
    [Tooltip("ここに入れたオブジェクトは、論理判定結果に応じてON/OFFが切り替わります。")]
    public GameObject[] targets;

    [Header("Logic Settings")]
    [Tooltip("論理演算のモードを選択します。")]
    public LogicMode logicMode;

    [Header("Input Objects")]
    [Tooltip("論理判定に使うオブジェクト。インスペクタから好きな数追加できます。")]
    public GameObject[] inputs;

    void Update()
    {
        // グローバルスイッチがOFFの場合、入力状態に関わらず全対象をOFF
        if (!globalSwitch)
        {
            SetTargets(false);
            return;
        }

        bool result = EvaluateLogic();
        SetTargets(result);
    }

    // 入力オブジェクトの状態に基づいて論理演算を実施
    private bool EvaluateLogic()
    {
        // 入力がない場合は false とする
        if (inputs == null || inputs.Length == 0)
        {
            return false;
        }

        switch (logicMode)
        {
            case LogicMode.AND:
                // AND：全てのオブジェクトが active なら true
                foreach (GameObject input in inputs)
                {
                    if (input == null || !input.activeSelf)
                    {
                        return false;
                    }
                }
                return true;

            case LogicMode.OR:
                // OR：いずれかが active なら true
                foreach (GameObject input in inputs)
                {
                    if (input != null && input.activeSelf)
                    {
                        return true;
                    }
                }
                return false;

            case LogicMode.NAND:
                // NAND：AND の否定。全てが active の場合のみ false、そうでなければ true
                foreach (GameObject input in inputs)
                {
                    if (input == null || !input.activeSelf)
                    {
                        return true;
                    }
                }
                return false;

            case LogicMode.NOR:
                // NOR：OR の否定。いずれかが active なら false、全てが inactive なら true
                foreach (GameObject input in inputs)
                {
                    if (input != null && input.activeSelf)
                    {
                        return false;
                    }
                }
                return true;

            default:
                return false;
        }
    }

    // 対象オブジェクトの active 状態を一括設定
    private void SetTargets(bool state)
    {
        if (targets == null) return;

        foreach (GameObject go in targets)
        {
            if (go != null)
            {
                go.SetActive(state);
            }
        }
    }
}
