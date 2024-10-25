using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

/*
ECS or MVC conception is applied on the small game
Firstly, entitis and their states are defined as the game model.
And then, give some components/controls which can modify the game model.
The end, system handler OnGUI called by game circle provides a game UI view.
*/
public class ChessGame : MonoBehaviour {

    // Entities and their states / Model
    private static int player;
    private static int count;
    private int winner;
    private int[,] chessBoard = new int[12, 12];

    // System Handlers
    void Start () {
        Init();
    }

    // View render entities / models
    // Here! you cannot modify model directly, use components/controls to do it
    void OnGUI() {
        GUI.Box(new Rect(210, 25, 925, 920), "");
        if (GUI.Button(new Rect(622, 905, 100, 30), "Restart")) Init();
        if (!GameOver()) {
            for (int i = 0; i < 12; i++) {
                for (int j = 0; j < 12; j++) {
                    if (chessBoard[i, j] == 0 && GUI.Button(new Rect(255 + j * 70, 50 + i * 70, 70, 70), "")) {
                        PutChess(i,j);
                    }
                    else if (chessBoard[i, j] == 1) GUI.Button(new Rect(255 + j * 70, 50 + i * 70, 70, 70), "O");
                    else if (chessBoard[i, j] == 2) GUI.Button(new Rect(255 + j * 70, 50 + i * 70, 70, 70), "X");
                }
            }
        }
        else {
            if (winner != 0)
                GUI.Box(new Rect(210, 25, 925, 920), "\n\n\n\n\nCongratulations!\n Player "+winner+" has won.");
            else
                GUI.Box(new Rect(210, 25, 925, 920), "\n\n\n\n\n\nThis is a draw!");
        }
    }

    // Components /controls
    void Init() {
        player = 1;
        winner = 0;
        count = 0;
        for(int i = 0; i < 12; i++)
            for(int j = 0; j < 12; j++)
                chessBoard[i, j] = 0;
    }

    void PutChess(int i,int j) {
        chessBoard[i, j] = player;
        player = 3 - player;
        count++;       
    }

    bool GameOver() {
        for(int i = 0; i < 12; i++) {       
            for(int j = 0; j < 12; j++){
                if(chessBoard[i, j] != 0 && j < 8){
                    if(chessBoard[i, j]==chessBoard[i, j+1] && chessBoard[i, j+1]==chessBoard[i, j+2] && chessBoard[i, j+2]==chessBoard[i, j+3] && chessBoard[i, j+3]==chessBoard[i, j+4]){
                        winner = chessBoard[i, j];
                        return true;
                    }
                }
                if(chessBoard[i, j] != 0 && i < 8){
                    if(chessBoard[i, j]==chessBoard[i+1, j] && chessBoard[i+1, j]==chessBoard[i+2, j] && chessBoard[i+2, j]==chessBoard[i+3, j] && chessBoard[i+3, j]==chessBoard[i+4, j]){
                        winner = chessBoard[i, j];
                        return true;
                    }
                }
                if(chessBoard[i, j] != 0 && i < 8 && j < 8){
                    if(chessBoard[i, j]==chessBoard[i+1, j+1] && chessBoard[i+1, j+1]==chessBoard[i+2, j+2] && chessBoard[i+2, j+2]==chessBoard[i+3, j+3] && chessBoard[i+3, j+3]==chessBoard[i+4, j+4]){
                        winner = chessBoard[i, j];
                        return true;
                    }
                }
                if(chessBoard[i, j] != 0 && i < 8 && j > 3){
                    if(chessBoard[i, j]==chessBoard[i+1, j-1] && chessBoard[i+1, j-1]==chessBoard[i+2, j-2] && chessBoard[i+2, j-2]==chessBoard[i+3, j-3] && chessBoard[i+3, j-3]==chessBoard[i+4, j-4]){
                        winner = chessBoard[i, j];
                        return true;
                    }
                }
            }
        }
        if (count < 144 && winner == 0) return false;

        return true;
    }
}