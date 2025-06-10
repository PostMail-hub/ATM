# 🏧 Unity ATM 기기 시뮬레이션 프로젝트

Unity로 제작된 간단한 ATM 시뮬레이터입니다. 로그인 및 회원가입 기능부터 입출금 기능, 데이터 저장 기능까지 포함된 프로젝트입니다.

***

## ✨ 주요 기능

- ✅ **회원가입 시스템**
  - 비밀번호 입력 시 Unity 기본 UI 시스템의 별표(*) 표시 적용
  - 이름은 **5글자 이하의 한글**만 허용 (조건문 유효성 검사)
  - 회원가입 시 기본 자금 지급:
    - 은행 계좌: **50,000원**
    - 현금 소지금: **100,000원**

- 🔐 **로그인 시스템**
  - 등록된 계정 정보와 일치할 경우 로그인 성공
  - 아이디/비밀번호 불일치 시 오류 팝업 표시

- 💰 **입금 기능**
  - 입력한 금액만큼 **현금에서 차감**되고 **계좌에 증가**
  - 입금 시 현금 잔액 부족하면 에러 메시지 출력

- 💸 **출금 기능**
  - 입력한 금액만큼 **계좌에서 차감**되고 **현금에 추가**
  - 출금 시 계좌 잔액 부족하면 에러 메시지 출력

- 💾 **데이터 저장**
  - `JSON` 파일로 저장
  - 게임을 종료하고 다시 실행해도 사용자 데이터 유지

***

## 🎮 조작 방법

- **회원가입** ID , 비밀번호 입력 -> 버튼 클릭 → 이름 작성 → 버튼 클릭 -> 계정 생성
- **로그인** → 등록한 정보 입력
- **입금 / 출금** → 금액 입력 → 버튼 클릭 or 금액이 지정된 버튼 클릭
- **잔액 확인** → 좌측 표시창 확인

***

## 🖼️ 시연 이미지

> 아래는 프로젝트 실행 화면입니다.

| 로그인 화면 | 회원가입 팝업 | ATM UI |
|-------------|---------------|--------|
| <img src="https://github.com/user-attachments/assets/ab1690e0-482e-40d5-b4cd-57da19e2f670" width="200"/> | <img src="https://github.com/user-attachments/assets/6776c73b-f42d-4273-aa62-eb15cd26a538" width="200"/> | <img src="https://github.com/user-attachments/assets/7c01f45d-be45-4b1c-bd2d-8194f59070cd" width="200"/> |

***

## 🎥 플레이 영상

> 📺 프로젝트 실제 작동 모습입니다.

[![ATM 기능 구현 프로젝트 플레이 영상](http://img.youtube.com/vi/2PJjeMIYB6g/0.jpg)](https://youtu.be/2PJjeMIYB6g)

***

## 🛠️ 사용 기술

- Unity 2022.3.17f1
- C#
- JSON 데이터 저장 (`JsonUtility`)
- 싱글톤 패턴 (`GameManager`)

***

## 📁 프로젝트 구조

```plaintext
Assets/
├── Scripts/
│   ├── _Core/
│   │   ├── GameManager.cs
│   │   └── LoginManager.cs
│   ├── Bank/
│   │   └── PopupBank.cs
│   ├── UI/
│   │   └── BankUIController.cs
│   └── User/
│       ├── UserData.cs
│       └── UserMoneyFormatter.cs
```

***

## 💡 개발 포인트

- Unity의 `PlayerPrefs` 대신 **JSON 파일 저장 방식**으로 구현
- `InputField`의 텍스트 유효성 검사로 사용자 입력 제어

## 🐞 문제 해결 사례

- TextMeshPro가 null로 뜨던 문제 → `TextMeshProUGUI` 누락된 참조 수정
- 입력값에 한글 검증 필요 → `Regex`로 한글 5자 이하 필터링 구현
