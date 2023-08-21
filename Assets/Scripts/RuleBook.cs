using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    // カード効果
    //  マジシャン(カード効果)がいれば数字対決
    //  密偵(次のターンにカードを先に出す)がいるなら追加効果
    //  道化(このターン引き分け)がいるなら引き分け
    //  暗殺者がいて、王子がいないなら数字効果反転
    //  王子と姫がいるならゲーム勝利判定
    //  ここまでくれば数字対決(大臣なら2倍)
    public Result GetResult(Battler player,Battler enemy)
    {
        if(player.SubmitCard.Base.Type == CardType.Magician || enemy.SubmitCard.Base.Type == CardType.Magician)
        {
            return NumberBattle(player, enemy, ministerEffect: false, reverseEffect:false);
        }

        if (player.SubmitCard.Base.Type == CardType.Spy && enemy.SubmitCard.Base.Type != CardType.Spy)
        {
            enemy.IsFirstSubmit = true;
        }
        else if (enemy.SubmitCard.Base.Type == CardType.Spy && player.SubmitCard.Base.Type != CardType.Spy)
        {
            player.IsFirstSubmit = true;
        }

        if(player.SubmitCard.Base.Type == CardType.Shogun)
        {
            player.IsAddNumberMode = true;
        }

        if (enemy.SubmitCard.Base.Type == CardType.Shogun)
        {
            enemy.IsAddNumberMode = true;
        }

        if (player.SubmitCard.Base.Type == CardType.Clown || enemy.SubmitCard.Base.Type == CardType.Clown)
        {
            return Result.TurnDraw;
        }

        if ((player.SubmitCard.Base.Type == CardType.Assassin && enemy.SubmitCard.Base.Type != CardType.Prince)
            || (enemy.SubmitCard.Base.Type == CardType.Assassin && player.SubmitCard.Base.Type != CardType.Prince))
        {
            return NumberBattle(player, enemy, ministerEffect: true, reverseEffect: true);
        }

        if (player.SubmitCard.Base.Type == CardType.Princess && enemy.SubmitCard.Base.Type == CardType.Prince)
        {
            return Result.GameWin;
        }

        if (player.SubmitCard.Base.Type == CardType.Prince && enemy.SubmitCard.Base.Type == CardType.Princess)
        {
            return Result.GameLose;
        }

        return NumberBattle(player, enemy, ministerEffect: true,reverseEffect:false);
    }

    Result NumberBattle(Battler player, Battler enemy,bool ministerEffect, bool reverseEffect)
    {
        var playerBase = player.SubmitCard.Base;
        var enemyBase = enemy.SubmitCard.Base;

        if (!ministerEffect)
        {
            if (playerBase.Number > enemyBase.Number)
            {
                if (reverseEffect) return Result.TurnLose;
                return Result.TurnWin;
            }
            if (playerBase.Number < enemyBase.Number)
            {
                if (reverseEffect) return Result.TurnWin;
                return Result.TurnLose;
            }
        }
        else
        {
            if (playerBase.Number > enemyBase.Number)
            {
                if (reverseEffect)
                {
                    if (enemyBase.Type == CardType.Minister)
                    {
                        return Result.TurnLose2;
                    }
                    return Result.TurnLose;
                }
                if (playerBase.Type == CardType.Minister)
                {
                    return Result.TurnWin2;
                }
                return Result.TurnWin;
            }
            else if (playerBase.Number < enemyBase.Number)
            {
                if (reverseEffect)
                {
                    if (playerBase.Type == CardType.Minister)
                    {
                        return Result.TurnWin2;
                    }
                    return Result.TurnWin;
                }
                if (enemyBase.Type == CardType.Minister)
                {
                    return Result.TurnLose2;
                }
                return Result.TurnLose;
            }
        }
        return Result.TurnDraw;

    }
}


public enum Result
{
    TurnWin,
    TurnLose,
    TurnDraw,
    TurnWin2,
    TurnLose2,
    GameWin,
    GameLose,
    GameDraw,
}