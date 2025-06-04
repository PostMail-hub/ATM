using UnityEngine;


/// UserData 클래스는 게임 내 사용자 정보를 저장하는 데이터 전용 클래스이다.
/// 이름, 보유 현금, 통장 잔액과 같은 값을 저장하며,
/// UI 표시나 저장/불러오기 등 다양한 시스템에서 참조된다.

/// MonoBehaviour를 상속하지 않기 때문에 new 키워드로 생성되며,
/// GameManager를 통해 전역적으로 관리된다.

[System.Serializable]
public class UserData
{
    public string Name;

    public int Cash;

    public int AccountBalance;

    public UserData(string name, int cash, int balance)
    {
        Name = name;
        Cash = cash;
        AccountBalance = balance;
    }

}
