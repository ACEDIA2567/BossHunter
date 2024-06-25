# BossHunter

## 게임 빌드 파일: https://drive.google.com/file/d/1bG3_89DD3B-sOFA8ewbe__WbxodNnSMT/view?usp=sharing

## 게임 설명
### 장르: 2D 횡스크롤 소울라이크
### 엔진 및 언어: Unity / C#
### 게임 스토리
**모험을 떠나는 플레이어가 어느 날 몬스터와 만나게 되는데...**

### 조작법:
> **이동 : WASD**  
> **공격 : 마우스 좌클릭**  
> **방어 : 마우스 우클릭**  
> **점프 : Space**  
> **구르기 : Left Shift**

## 구현 내용
## 플레이어
- 플레이어 스탯(HP, SP)
- 공격, 방어 콜라이더 적용
- 특정 애니메이션(death, roll) 오류 수정
- 특정 애니메이션(death, hurt) 트리거 적용

## 적
- 보스 스탯(HP, 방어력, 이동속도)
- 일반 공격 패턴 3종, 특수 공격 패턴 2종
- 애니메이션 이벤트를 통해 공격 패턴의 공격 판정 발생 시간 조절
- 플레이어 추적 기능 구현

## 시스템

### - 씬  
<details>
<summary>펼치기</summary>
  
**인트로씬**  
![image](https://github.com/ACEDIA2567/BossHunter/assets/101154683/95142ed4-20a7-4f26-a5cf-33f83892b70d)  
**인게임씬**  
![image](https://github.com/ACEDIA2567/BossHunter/assets/101154683/b2b19fb4-2786-4518-b168-bac4f73e9fee)

</details>

### - UI
<details>
<summary>펼치기</summary>
  
**옵션 창**  
![image](https://github.com/ACEDIA2567/BossHunter/assets/101154683/7a8efc00-d295-4c3f-a065-bfecbbff4182)


**게임 클리어**  
![image](https://github.com/ACEDIA2567/BossHunter/assets/101154683/87f437e1-67d8-4c6c-bd14-301ebb566f42)

**게임 오버**  
![image](https://github.com/ACEDIA2567/BossHunter/assets/101154683/2241e2af-382e-470f-9c4f-98da5cebc6bb)

</details>

