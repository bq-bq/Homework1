using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess_Board: MonoBehaviour
{
    public enum State { Win, Begin, End};
    public static int[][] ChessBoard = new int[3][];
    public const int player1 = 1;
    public const int player2 = 2;
    public static int flag = 1;
    public static int turn = 1;
    public State state;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i ++)
        {
            ChessBoard[i] = new int[3] { 0, 0, 0 };
        }
        flag = 1;
        state = State.Begin;
        count = 0;
    }

    void Reset()
    {
       for(int i = 0; i < 3; i ++)
        {
            for(int j = 0; j < 3; j ++)
            {
                ChessBoard[i][j] = 0;
            }
        }
        flag = 1;
        count = 0;
        state = State.Begin;
    }

    // Update is called once per frame
    void Update()
    {
        If_Win();
     //   Debug.Log("update");
    }
    string SetButton(int F)
    {
        if (F == 1) return "X";
        if (F == 2) return "O";
        return "";
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle
        {
            border = new RectOffset(10, 10, 10, 10),
            fontSize = 50,
            fontStyle = FontStyle.Italic
        };
        style.normal.textColor = new Color(0, 204 / 255f, 204 / 255f);
        GUI.Label(new Rect(250, 10, 50, 20), "井字棋", style);
        const int ButtonWidth = 50;
        const int ButtonHeight = 50;
        const int ButtonBeginX = 240;
        const int ButtonBeginY = 75;
        for(int i = 0;i < 3; i++)
        {
            for(int j = 0; j < 3;j ++)
            {

               if( GUI.Button(new Rect(ButtonBeginX + ButtonWidth * i, ButtonBeginY + ButtonHeight * j, ButtonWidth, ButtonHeight), SetButton(ChessBoard[i][j])))
                {
                    //  Debug.Log("Click\n");
                    // Debug.Log("This is the " + i + "," + j + "chess");
                    If_Win();
                    if (ChessBoard[i][j] == 0 && state == State.Begin )
                    {
                        ChessBoard[i][j] = flag;
                        flag = flag == 1 ? 2: 1;
                        count++;
                        If_Win();
                        Debug.Log(count);
                    }
                }
            }
        }

        if (count == 9 && state != State.Win){
            style.normal.textColor = new Color(0, 1, 0);
            GUI.Label(new Rect(190, 230, 100, 100), "Draw!", style);
            state = State.End;
        }
        if (GUI.Button(new Rect(100, 100, 50, 50), "Reset"))
        {
            Reset();
        }
        if (flag == 1 && state == State.Win)
        {
            style.normal.textColor = new Color(1, 0 , 0);
            GUI.Label(new Rect(190, 230, 0, 0), "Player 1 Victory!", style);
        }
        else if (flag == 2 && state == State.Win)
        {
            style.normal.textColor = new Color(0, 0, 1);
            GUI.Label(new Rect(190, 230, 0, 0), "Player 2 Victory!", style);
        }
    }

    void If_Win()
    {
        if (state == State.End || state == State.Win) return;
        for (int i = 0; i < 3; i++)
        {
            if(ChessBoard[i][0] != 0 && ChessBoard[i][0] == ChessBoard[i][1] && ChessBoard[i][0] == ChessBoard[i][2])
            {
                flag = ChessBoard[i][0];
                state = State.Win;
                return;
            }
            if (ChessBoard[0][i] != 0 && ChessBoard[0][i] == ChessBoard[1][i] && ChessBoard[0][i] == ChessBoard[2][i])
            {
                flag = ChessBoard[0][i];
                state = State.Win;
                return;
            }
        }
        if(ChessBoard[0][0] != 0 && ChessBoard[0][0] == ChessBoard[1][1] && ChessBoard[2][2] == ChessBoard[0][0])
        {
            state = State.Win;
            flag = ChessBoard[0][0];
            return;
        }
        if (ChessBoard[2][0] != 0 && ChessBoard[2][0] == ChessBoard[1][1] && ChessBoard[2][0] == ChessBoard[0][2])
        {
            state = State.Win;
            flag = ChessBoard[2][0];
            return;
        }
    }
}
