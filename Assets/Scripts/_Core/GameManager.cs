using UnityEngine;

/// GameManager는 게임 전반의 데이터를 관리하는 싱글톤 클래스
 
/// 게임 시작 시 UserData를 초기화하여 유저 정보를 저장한다.
/// Instance를 통해 어디서든 전역 접근이 가능하다.

public class GameManager : MonoBehaviour
{
    // GameManager의 싱글톤 인스턴스
    // 다른 스크립트에서 GameManager.Instance로 접근할 수 있다.
    public static GameManager Instance;

    // 유저 정보를 저장하는 데이터 객체
    // 이름, 현금, 통장 잔액 등의 정보를 포함합니다.
    public UserData userData;


    // 게임이 시작될 때 호출되며,
    // 싱글톤 인스턴스를 초기화하고 유저 데이터를 생성한다.
    private void Awake()
    {
        Instance = this;
        userData = new UserData("이희민", 100000, 50000);
    }
}