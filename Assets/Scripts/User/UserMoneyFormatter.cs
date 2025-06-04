using UnityEngine;
using TMPro;

/// UserMoneyFormatter는 GameManager에서 저장된 UserData를 기반으로,
/// 유저의 이름, 보유 현금, 통장 잔액 정보를 UI 텍스트에 반영해주는 클래스이다.

public class UserMoneyFormatter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI UserName; // 유저의 이름을 표시하는 텍스트

    [SerializeField]
    private TextMeshProUGUI AccountBalanceValue; // 유저의 통장 잔액을 표시하는 텍스트

    [SerializeField]
    private TextMeshProUGUI CashAmount; // 유저의 보유 현금을 표시하는 텍스트

    // Start보다 먼저 호출되는 Unity 생명주기 메서드
    // 인스펙터에서 연결되지 않았을 경우, 오브젝트 이름으로 TextMeshProUGUI 컴포넌트를 자동 참조한다.
    private void Awake()
    {
        if (UserName == null)
        {
            GameObject name = GameObject.Find("UserName");
            if (name != null)
            {
                UserName = name.GetComponent<TextMeshProUGUI>();
            }
        }

        if (CashAmount == null)
        {
            GameObject cash = GameObject.Find("CashAmount");
            if (cash != null)
            {
                CashAmount = cash.GetComponent<TextMeshProUGUI>();
            }
        }

        if (AccountBalanceValue == null)
        {
            GameObject ab = GameObject.Find("AccountBalanceValue");
            if (ab != null)
            {
                AccountBalanceValue = ab.GetComponent<TextMeshProUGUI>();
            }
        }
    }
    // 게임 시작 시 호출되며, 화면에 유저 정보를 반영하기 위해 Refresh()를 실행한다
    void Start()
    {
        Refresh();
    }

    // 유저 데이터를 GameManager에서 불러와 UI에 표시하는 메서드
    // 외부에서 이 메서드를 호출하면 최신 데이터를 UI에 반영할 수 있다
    public void Refresh()
    {
        var data = GameManager.Instance.userData;

        // data.Cash와 data.AccountBalance는 숫자(int)지만,
        // 사용자에게 보여줄 땐 천 단위마다 쉼표(,)가 있는 문자열로 바꿔야 보기 쉽다.
        // string.Format("{0:N0}", 값)을 사용하면 숫자를 "1,000"처럼 자동으로 작성해준다.
        // 추가 예시 : 1000000 → "1,000,000"
        string userName = data.Name;
        string cashText = string.Format("{0:N0}", data.Cash);
        string accountText = string.Format("{0:N0}", data.AccountBalance);

        // 변환된 값을 텍스트 UI에도 적용
        if (UserName != null)
            UserName.text = userName;

        if (CashAmount != null)
            CashAmount.text = cashText;

        if (AccountBalanceValue != null)
            AccountBalanceValue.text = accountText;
    }
}
