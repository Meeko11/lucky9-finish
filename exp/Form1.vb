Imports System.IO

Public Class Form1

    Dim rand As New Random()

    Dim ranplayercard1 As String
    Dim ranplayercard2 As String
    Dim ranplayercard3 As String

    Dim ranbanker1 As String
    Dim ranbanker2 As String
    Dim ranbanker3 As String

    Dim playerPoints As Integer = 100 ' Initial player points
    Dim wagerAmount As Integer = 10 ' Fixed wager amount

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clearComponents()
        Label1.Text = "Points: " & playerPoints
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If playerPoints < wagerAmount Then
            MsgBox("You don't have enough points to wager!")
            Return
        End If

        clearComponents()

        ranplayercard1 = rand.Next(1, 41).ToString()
        ranplayercard2 = rand.Next(1, 41).ToString()

        displayCard(PictureBox1, ranplayercard1)
        displayCard(PictureBox2, ranplayercard2)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ranplayercard1 Is Nothing Or ranplayercard2 Is Nothing Then
            MsgBox("Draw your first two cards first!")
            Return
        End If

        ranplayercard3 = rand.Next(1, 41).ToString()
        displayCard(PictureBox3, ranplayercard3)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ranplayercard1 Is Nothing Or ranplayercard2 Is Nothing Then
            MsgBox("Draw the player's cards first!")
            Return
        End If

        ranbanker1 = rand.Next(1, 41).ToString()
        ranbanker2 = rand.Next(1, 41).ToString()

        displayCard(PictureBox4, ranbanker1)
        displayCard(PictureBox5, ranbanker2)

        Dim tempbankercount As Integer = (getCardValue(ranbanker1) + getCardValue(ranbanker2)) Mod 10

        If tempbankercount <= 5 Then
            ranbanker3 = rand.Next(1, 41).ToString()
            displayCard(PictureBox6, ranbanker3)
        Else
            ranbanker3 = Nothing
            displayCard(PictureBox6, Nothing) ' No third card, show the back
        End If

        check()
    End Sub

    Sub check()
        Dim playerCardTotal As Integer = (getCardValue(ranplayercard1) + getCardValue(ranplayercard2) +
                                          If(ranplayercard3 IsNot Nothing, getCardValue(ranplayercard3), 0)) Mod 10
        Dim bankerCardTotal As Integer = (getCardValue(ranbanker1) + getCardValue(ranbanker2) +
                                          If(ranbanker3 IsNot Nothing, getCardValue(ranbanker3), 0)) Mod 10

        If playerCardTotal > bankerCardTotal Then
            playerPoints += wagerAmount ' Player wins
            MsgBox("You win!")
        ElseIf playerCardTotal < bankerCardTotal Then
            playerPoints -= wagerAmount ' Player loses
            MsgBox("You lose!")
        Else
            MsgBox("It's a tie!")
        End If

        Label1.Text = "Points: " & playerPoints

        If playerPoints <= 0 Then
            MsgBox("Game over! You have no points left.")
        End If

        resetGame()
    End Sub

    Sub clearComponents()
        ' Show back of the card for all PictureBoxes
        PictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.jpg")
        PictureBox2.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.jpg")
        PictureBox3.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.jpg")
        PictureBox4.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.jpg")
        PictureBox5.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.jpg")
        PictureBox6.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.jpg")
    End Sub

    Sub resetGame()
        ranplayercard1 = Nothing
        ranplayercard2 = Nothing
        ranplayercard3 = Nothing

        ranbanker1 = Nothing
        ranbanker2 = Nothing
        ranbanker3 = Nothing
    End Sub

    Sub displayCard(PictureBox As PictureBox, cardName As String)
        If String.IsNullOrEmpty(cardName) Then
            PictureBox.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\back.png")
        Else
            PictureBox.Image = Image.FromFile(Directory.GetCurrentDirectory & "\test\" & cardName & ".PNG")
        End If
    End Sub

    Private Function getCardValue(cardNumber As String) As Integer
        Dim cardNum As Integer = Integer.Parse(cardNumber)
        Return (cardNum - 1) Mod 10 + 1 ' Card values range from 1 to 10
    End Function
End Class
