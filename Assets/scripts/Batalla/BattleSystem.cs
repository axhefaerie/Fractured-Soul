using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject attackButton;
    public GameObject healButton;

    public GameObject dañoAura;
    public GameObject dañoMonstruo;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Animator animatorMonstruo;
    public Animator animatorAura;

    public Animator ataqueAura;
    public Animator ataqueMonstruo;

    public int escenaCont;
    public int escenaDie;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;
    
    void Start()
    {
        attackButton.SetActive(false);
        healButton.SetActive(false);

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Te has chocado con un " + enemyUnit.unitName + " salvaje...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        dañoAura.SetActive(true);
        ataqueAura.SetBool("daño", true);
        attackButton.SetActive(false);
        healButton.SetActive(false);
        yield return new WaitForSeconds(0.6f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "El ataque ha sido efectivo...";

        dañoAura.SetActive(false);
        ataqueAura.SetBool("daño", false);

        attackButton.SetActive(false);
        healButton.SetActive(false);
        
        yield return new WaitForSeconds(3f);

        if (isDead)
        {
            state = BattleState.WON;
            dialogueText.text = "Has salido victoriosa...";
            animatorMonstruo.SetBool("muerte", true);
            yield return new WaitForSeconds(4f);
            Continue();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    
    IEnumerator PlayerHeal()
    {
        attackButton.SetActive(false);
        healButton.SetActive(false);

        playerUnit.Heal(16);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Ahora te encuentras un poco mejor...";

        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dañoMonstruo.SetActive(true);
        ataqueMonstruo.SetBool("daño", true);

        dialogueText.text = enemyUnit.unitName + " te ha atacado...";

        yield return new WaitForSeconds(0.4f);
        dañoMonstruo.SetActive(false);
        ataqueMonstruo.SetBool("daño", false);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            animatorAura.SetBool("muerte", true);
            dialogueText.text = "Te han vencido...";
            yield return new WaitForSeconds(4f);
            Die();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void Continue ()
    {
        PlayerPrefs.SetInt("HeGanado", 1);
        SceneManager.LoadScene(escenaCont);
    }

    public void Die ()
    {
        PlayerPrefs.SetInt("HeGanado", 0);
        SceneManager.LoadScene(escenaDie);
    }

    void PlayerTurn()
    {
        dialogueText.text = "Escoge que hacer...";
        attackButton.SetActive(true);
        healButton.SetActive(true);
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerHeal());
    }
}
