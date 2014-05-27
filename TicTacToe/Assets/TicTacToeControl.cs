using UnityEngine;
using System.Collections;

public class TicTacToeControl : MonoBehaviour
{
	public GUISkin guiSkin;
	public Texture2D titleImage;

	public GameState gameState = GameState.Opening;
	public SquareState winner = SquareState.Clear;
	public SquareState[] board = new SquareState[9];
	public bool xTurn = true;

	//int smaller = Screen.width == GetBigger(Screen.width , titleImage.width) ? titleImage.width : Screen.width;
	//GUIStyle turnStyle = new GUIStyle(GUI.skin.GetStyle("label"));
	//turnStyle.fontSize *= (int)((Screen.width + Screen.height - (GetSmaller(Screen.width, Screen.height) * 2)) / 100);


	public void NewGame ()
	{
		xTurn = true;
		board = new SquareState[9];
	}

	
	public void OnGUI ()
	{
		if(guiSkin != null)
			GUI.skin = guiSkin;

		switch (gameState) {
		case GameState.Opening:
			DrawOpening();
			break;
		case GameState.MultiPlayer:
			DrawGameBoard();
			break;
		case GameState.SinglePlayer:
			DrawGameBoard();
			break;
		case GameState.GameOver:
			DrawGameOver();
			break;
		}
	}

	public int GetBigger(int x, int y) {
		if(x > y) {
			return x;
		}
		else {
			return y;
		}

	}

	public int GetSmaller(int x, int y) {
		if(x <= y) {
			return x;
		}
		else {
			return y;
		}
		
	}

	public void DrawOpening ()
	{

		Rect groupRect = new Rect((GetBigger(Screen.width , titleImage.width) / 2 - GetSmaller(Screen.width , titleImage.width) / 2), (Screen.height / 2) - ((titleImage.height + 75) / 2), GetSmaller(Screen.width , titleImage.width), titleImage.height + 75);
		GUI.BeginGroup(groupRect);
	
		int width = Screen.width == GetBigger(Screen.width , titleImage.width) ? GetSmaller(Screen.width , titleImage.width) : GetSmaller(Screen.width , titleImage.width) - (GetBigger(Screen.width , titleImage.width) - GetSmaller(Screen.width , titleImage.width));
		Rect titleRect = new Rect(0, 0, width, titleImage.height);		

		GUI.DrawTexture(titleRect, titleImage);
	    Rect multiRect = new Rect (titleRect.x, titleRect.y + titleRect.height - 7, titleRect.width, 40);
		Rect singleRect = new Rect (multiRect.x, multiRect.y + multiRect.height + 5, multiRect.width, 40);

		//GUIStyle turnStyle = new GUIStyle(GUI.skin.GetStyle("button"));
		//urnStyle.fontSize *= (int)((Screen.width + Screen.height - (GetSmaller(Screen.width, Screen.height))*2) / 100);
	
		if(GUI.Button(multiRect, "MultiPlayer")) {
			NewGame();
			gameState = GameState.MultiPlayer;
	
		}
		if(GUI.Button(singleRect, "SinglePlayer")) {
			NewGame();
			gameState = GameState.SinglePlayer;
			
		}

		GUI.EndGroup();
	}


	public void RandomShot() {
		int randomShot;
		
		while (IsClear()) {
			randomShot = Random.Range(0,9);		
			if (board [randomShot] == SquareState.Clear) {
				board [randomShot] = SquareState.OControl;
				return;
			}						
		}
	}
	
	public bool IsClear()
	{
		for (int i = 0; i < board.Length; i++) {
			if (board [i] == SquareState.Clear)
				return true;
		}
		return false;
	}



	public void DrawGameBoard ()
	{
		for(int y = 0; y < 3; y++) {
			for(int x = 0; x < 3; x++) {
				int boardIndex = (y * 3) + x;
				Rect square = new Rect (x * GetSmaller(Screen.width, Screen.height) / 3, y * GetSmaller(Screen.width, Screen.height) / 3, GetSmaller(Screen.width, Screen.height) / 3, GetSmaller(Screen.width, Screen.height) / 3);
				string owner = board[boardIndex] == SquareState.XControl ? "X" : board[boardIndex] == SquareState.OControl ? "O" : "";
		
				if(board[boardIndex] == SquareState.Clear) {
					if(GUI.Button(square, owner))
						SetControl (boardIndex);
				}
				else GUI.Label(square, owner, owner + "Square");
			}
		}

		Rect turnRect = new Rect(300, 0, 100, 100);
		turnRect.x = Screen.width == GetSmaller(Screen.width, Screen.height)? 0 : Screen.width;
		turnRect.y = Screen.width == GetSmaller(Screen.width, Screen.height) ? Screen.width : 0;
		turnRect.width = Screen.width == GetSmaller(Screen.width, Screen.height) ? Screen.width : Screen.width - Screen.height;
		turnRect.height = Screen.width == GetSmaller(Screen.width, Screen.height) ? Screen.height - Screen.width : Screen.height;
		
	   // GUIStyle turnStyle = new GUIStyle(GUI.skin.GetStyle("label"));
	   // turnStyle.fontSize *= (int)((Screen.width + Screen.height - (GetSmaller(Screen.width, Screen.height) * 2)) / 100);

		//string turnTitle = xTurn ? "X's Turn!" : "O's Turn!";

		if( gameState == GameState.SinglePlayer) {
			if(!xTurn) {
				RandomShot();
			    xTurn = true;
			}
			//GUI.Label(turnRect, turnTitle, turnStyle);
		}
		GUIStyle turnStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		turnStyle.fontSize *= (int)((Screen.width + Screen.height - (GetSmaller(Screen.width, Screen.height) * 2)) / 100);
		string turnTitle = xTurn ? "X's Turn!" : "O's Turn!";

		GUI.Label(turnRect, turnTitle, turnStyle);
		
	}
	
	public void DrawGameOver ()
	{
		Rect groupRect = new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 75, 200 , 150);
		GUI.BeginGroup(groupRect);

		Rect winnerRect = new Rect (0, 0, groupRect.width, groupRect.height / 2);
		string winnerTitle = winner == SquareState.XControl ? "X Wins!" : winner == SquareState.OControl ? "O Wins!" : "It's a Tie!";

		GUIStyle turnStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		turnStyle.fontSize *= (int)((Screen.width + Screen.height - (GetSmaller(Screen.width, Screen.height) * 2)) / 100);

		GUI.Label (winnerRect, winnerTitle, turnStyle);
	
		winnerRect.y += winnerRect.height;

		if(GUI.Button(winnerRect, "MainMenu"))
		   gameState = GameState.Opening;

		GUI.EndGroup();	
	}
	

	public void SetControl (int boardIndex)
	{
		if (boardIndex < 0 || boardIndex >= board.Length)
			return;
		
		board [boardIndex] = xTurn ? SquareState.XControl : SquareState.OControl;
		xTurn = !xTurn;
	}


	public void LateUpdate ()
	{
		if(gameState != GameState.MultiPlayer && gameState != GameState.SinglePlayer )
			return;

		for(int i = 0; i < 3; i++) {
			if(board[i] != SquareState.Clear && board[i] == board[i + 3] && board[i] == board[i + 6]) {
				SetWinner(board[i]);
				return;
			}
			else if(board[i * 3] != SquareState.Clear && board[i * 3] == board[(i * 3) + 1] && board[i * 3] == board[(i * 3) + 2]) {
		    	 SetWinner(board [i * 3]);
				 return;
			}
		}

		if(board[0] != SquareState.Clear && board[0] == board[4] && board[0] == board[8]) {
		   SetWinner(board[0]);
		   return;
		}
		else if(board[2] != SquareState.Clear && board[2] == board[4] && board[2] == board[6]) {
			SetWinner(board[2]);
			return;
		}

		for(int i = 0; i < board.Length; i++) {
			if (board [i] == SquareState.Clear)
				return;
		}
		SetWinner (SquareState.Clear);
	}



    public void SetWinner (SquareState toWin)
	{
		winner = toWin;
		gameState = GameState.GameOver;
	}
}
		

