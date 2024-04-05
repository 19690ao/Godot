using Godot;
using System;

public partial class TicTacToe : GridContainer
{
	private char currentPlayer = 'X';
	private char[] board = new char[9];
	private Button[] buttons = new Button[9];

	public override void _Ready()
	{
		for (int i = 0; i < 9; i++)
		{
			board[i] = ' ';
			buttons[i] = (Button)GetNode($"Button{i+1}");
			buttons[i].ClipText = true;
			int buttonIndex = i;
			buttons[i].Pressed += () => OnButtonPressed(buttonIndex);
		}
	}

	private void OnButtonPressed(int index)
	{
		GD.Print($"Pressed #{index + 1}");
		if (board[index] != ' ')
		{
			GD.Print("This spot is already taken.");
			return;
		}
		
		board[index] = currentPlayer;
		Button button = buttons[index];
		// button.Text = currentPlayer;
		GD.Print(this);
		
		if (CheckWin(currentPlayer))
		{
			GD.Print(currentPlayer + " wins!");
			ResetBoard();
		}
		else if (Array.IndexOf(board, ' ') == -1)
		{
			GD.Print("It's a draw!");
			ResetBoard();
		}
		else
		{
			currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
		}
	}

	private bool CheckWin(char player)
	{
		int[][] winConditions = new int[][]
		{
			new int[] { 0, 1, 2 },
			new int[] { 3, 4, 5 },
			new int[] { 6, 7, 8 },
			new int[] { 0, 3, 6 },
			new int[] { 1, 4, 7 },
			new int[] { 2, 5, 8 },
			new int[] { 0, 4, 8 },
			new int[] { 2, 4, 6 }
		};

		foreach (int[] condition in winConditions)
		{
			if (board[condition[0]] == player && board[condition[1]] == player && board[condition[2]] == player)
			{
				return true;
			}
		}

		return false;
	}

	private void ResetBoard()
	{
		for (int i = 0; i < 9; i++)
		{
			board[i] = ' ';
			//Button button = (Button)GetNode("Button" + i);
			//buttons[i] = (Button)GetNode($"Button{i+1}");
			//buttons[i].ClipText = true;
			//int buttonIndex = i;
			//buttons[i].Pressed += () => OnButtonPressed(buttonIndex);
			//button.Text = "";
		}
		currentPlayer = 'X';
		return;
	}
	
	public override string ToString()
	{
		string line1 = $"|{board[0]}|{board[1]}|{board[2]}|\n";
		string line2 = $"|{board[3]}|{board[4]}|{board[5]}|\n";
		string line3 = $"|{board[6]}|{board[7]}|{board[8]}|\n";
		return $"{line1}{line2}{line3}";
	}
}
