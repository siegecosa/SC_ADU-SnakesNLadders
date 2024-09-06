Public Class Form1
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setup()
    End Sub

    Private Sub setup()
        panel_player.Visible = False
        panel_ai1.Visible = False
        panel_ai2.Visible = False
        panel_ai3.Visible = False
        ComboBox1.Enabled = True
        ComboBox1.SelectedIndex = -1
        Button1.Text = "START GAME"
        dice1.Enabled = False
        dice2.Enabled = False
        diceclicked = False
        playerroll = 0
        ai1_roll = 0
        ai2_roll = 0
        ai3_roll = 0
        icon_player.Visible = False
        icon_ai1.Visible = False
        icon_ai2.Visible = False
        icon_ai3.Visible = False
        icon_player.Location = New Point(632, 37)
        icon_ai1.Location = New Point(668, 37)
        icon_ai2.Location = New Point(699, 37)
        icon_ai3.Location = New Point(730, 37)
        ai1pos.Text = "Position: "
        ai2pos.Text = "Position: "
        ai3pos.Text = "Position: "
    End Sub

    Dim gamemode As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "START GAME" Then
            dice1.Enabled = True
            dice2.Enabled = True
            ComboBox1.Enabled = False
            Dim selectedValue As String = ComboBox1.SelectedItem

            If selectedValue = "Single Player" Then
                gamemode = "singeplayer"
                panel_player.Visible = True
                Button1.Text = "RESTART GAME"
                icon_player.Visible = True

            ElseIf selectedValue = "Two Players" Then
                gamemode = "2player"
                panel_player.Visible = True
                panel_ai1.Visible = True
                Button1.Text = "RESTART GAME"
                icon_player.Visible = True
                icon_ai1.Visible = True

            ElseIf selectedValue = "Three Players" Then
                gamemode = "3player"
                panel_player.Visible = True
                panel_ai1.Visible = True
                panel_ai2.Visible = True
                Button1.Text = "RESTART GAME"
                icon_player.Visible = True
                icon_ai1.Visible = True
                icon_ai2.Visible = True

            ElseIf selectedValue = "Four Players" Then
                gamemode = "4player"
                panel_player.Visible = True
                panel_ai1.Visible = True
                panel_ai2.Visible = True
                panel_ai3.Visible = True
                Button1.Text = "RESTART GAME"
                icon_player.Visible = True
                icon_ai1.Visible = True
                icon_ai2.Visible = True
                icon_ai3.Visible = True

            Else
                MessageBox.Show("Error: choose how many players", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                setup()
            End If


            '===============RESTART GAME HERE
        ElseIf Button1.Text = "RESTART GAME" Then
            setup()
        End If

    End Sub

    Dim rand As New Random()
    Dim dicenum1 As Integer
    Dim dicenum2 As Integer
    Dim diceclicked As Boolean = False
    Private Sub dice1_Click(sender As Object, e As EventArgs) Handles dice1.Click
        If gamemode = "2player" Then
            ai1move()

        ElseIf gamemode = "3player" Then
            ai1move()
            ai2move()

        ElseIf gamemode = "4player" Then
            ai1move()
            ai2move()
            ai3move()

        End If

        If diceclicked = False Then
            diceclicked = True
            ' Create a random number generator
            dicenum1 = rand.Next(1, 7) ' Generate a random number between 1 and 6
            dicenum2 = rand.Next(1, 7) ' Generate another random number between 1 and 6
            Dim img1 As Image = My.Resources.ResourceManager.GetObject("d" & dicenum1.ToString()) ' Load the corresponding image for dice 1
            Dim img2 As Image = My.Resources.ResourceManager.GetObject("d" & dicenum2.ToString()) ' Load the corresponding image for dice 2
            dice1.Image = img1 ' Set the image for dice 1 picture box
            dice2.Image = img2 ' Set the image for dice 2 picture box



            boardmove()
        End If
    End Sub

    Private Sub dice2_Click(sender As Object, e As EventArgs) Handles dice2.Click
        If gamemode = "2player" Then
            ai1move()

        ElseIf gamemode = "3player" Then
            ai1move()
            ai2move()

        ElseIf gamemode = "4player" Then
            ai1move()
            ai2move()
            ai3move()

        End If

        If diceclicked = False Then
            diceclicked = True
            dicenum1 = rand.Next(1, 7) ' Generate a random number between 1 and 6
            dicenum2 = rand.Next(1, 7) ' Generate another random number between 1 and 6
            Dim img1 As Image = My.Resources.ResourceManager.GetObject("d" & dicenum1.ToString()) ' Load the corresponding image for dice 1
            Dim img2 As Image = My.Resources.ResourceManager.GetObject("d" & dicenum2.ToString()) ' Load the corresponding image for dice 2
            dice1.Image = img1 ' Set the image for dice 1 picture box
            dice2.Image = img2 ' Set the image for dice 2 picture box


            boardmove()
        End If
    End Sub



    Dim playerroll As Integer = 0
    Dim playerrollexceed As Integer = 0

    Dim ai1_dicenum1 As Integer
    Dim ai1_dicenum2 As Integer
    Dim ai1_roll As Integer

    Dim ai2_dicenum1 As Integer
    Dim ai2_dicenum2 As Integer
    Dim ai2_roll As Integer

    Dim ai3_dicenum1 As Integer
    Dim ai3_dicenum2 As Integer
    Dim ai3_roll As Integer
    Private Sub boardmove()
        playerroll = playerroll + dicenum1 + dicenum2


        If playerroll < 100 Then ' Check if the number is within the range of available PictureBoxes
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & playerroll.ToString()), PictureBox)
            playerpos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_player.Location = New Point(pb.Left, pb.Top)
                icon_player.BringToFront() ' Bring the icon_player to the front of the form
            End If

            If icon_player.Bounds.IntersectsWith(PictureBox13.Bounds) Then
                playerroll = playerroll + 6
                MessageBox.Show("You got power up of 6!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox21.Bounds) Then
                playerroll = playerroll + 10
                MessageBox.Show("You got power up of 10!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox32.Bounds) Then
                playerroll = playerroll + 4
                MessageBox.Show("You got power up of 4!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox43.Bounds) Then
                playerroll = playerroll + 7
                MessageBox.Show("You got power up of 7!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox69.Bounds) Then
                playerroll = playerroll + 1
                MessageBox.Show("You got power up of 1!")

                'POWER DOWN HERE
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox16.Bounds) Then
                playerroll = playerroll - 10
                MessageBox.Show("You got power down of -10!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                playerroll = playerroll - 36
                MessageBox.Show("You got power down of -36! Ouchie!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox42.Bounds) Then
                playerroll = playerroll - 9
                MessageBox.Show("You got power down of -9!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox65.Bounds) Then
                playerroll = playerroll - 3
                MessageBox.Show("You got power down of -3!")
            ElseIf icon_player.Bounds.IntersectsWith(PictureBox89.Bounds) Then
                playerroll = playerroll - 12
                MessageBox.Show("You got power down of -12!")

            End If
            pb = CType(Me.Controls("PictureBox" & playerroll.ToString()), PictureBox)
            playerpos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_player.Location = New Point(pb.Left, pb.Top)
                icon_player.BringToFront() ' Bring the icon_player to the front of the form
            End If




        ElseIf playerroll > 100 Then
            Dim playerrollexceed As Integer = playerroll - 100
            playerroll = 100 - playerrollexceed

            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & playerroll.ToString()), PictureBox)
            playerpos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_player.Location = New Point(pb.Left + (pb.Width - icon_player.Width) \ 2, pb.Top + (pb.Height - icon_player.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_player.BringToFront() ' Bring the icon_player to the front of the form
            End If
            Dim message As String = "You have exceeded by " & playerrollexceed.ToString() & " moves past the 100th mark and have been moved backwards accordingly."
            MessageBox.Show(message, "Moves Exceeded!", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'in case player exceeds and goes back, but to 99 specifically.. go downnnnnnnnnn
            If icon_player.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                playerroll = playerroll - 36
                MessageBox.Show("You got power down of -36! Ouchie!")
            End If
            pb = CType(Me.Controls("PictureBox" & playerroll.ToString()), PictureBox)
            playerpos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_player.Location = New Point(pb.Left + (pb.Width - icon_player.Width) \ 2, pb.Top + (pb.Height - icon_player.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_player.BringToFront() ' Bring the icon_player to the front of the form
            End If


        ElseIf playerroll = 100 Then
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & playerroll.ToString()), PictureBox)
            playerpos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_player.Location = New Point(pb.Left, pb.Top)
                icon_player.BringToFront() ' Bring the icon_player to the front of the form
            End If
            MessageBox.Show("You won! Game over. Restarting...")
            setup()
        End If
        diceclicked = False
    End Sub





    Private Sub ai1move()
        'Roll the dice here
        ai1_dicenum1 = rand.Next(1, 7) ' Generate a random number between 1 and 6
        ai1_dicenum2 = rand.Next(1, 7) ' Generate another random number between 1 and 6

        ai1_roll = ai1_roll + ai1_dicenum1 + ai1_dicenum2
        If ai1_roll < 100 Then ' Check if the number is within the range of available PictureBoxes
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai1_roll.ToString()), PictureBox)
            ai1pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai1.Location = New Point(pb.Left, pb.Top)
                icon_ai1.BringToFront() ' Bring the icon_player to the front of the form
            End If

            If icon_ai1.Bounds.IntersectsWith(PictureBox13.Bounds) Then
                ai1_roll = ai1_roll + 6
                MessageBox.Show("AI P1 got power up of 6!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox21.Bounds) Then
                ai1_roll = ai1_roll + 10
                MessageBox.Show("AI P1 got power up of 10!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox32.Bounds) Then
                ai1_roll = ai1_roll + 4
                MessageBox.Show("AI P1 got power up of 4!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox43.Bounds) Then
                ai1_roll = ai1_roll + 7
                MessageBox.Show("AI P1 got power up of 7!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox69.Bounds) Then
                ai1_roll = ai1_roll + 1
                MessageBox.Show("AI P1 got power up of 1!")

                'POWER DOWN HERE
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox16.Bounds) Then
                ai1_roll = ai1_roll - 10
                MessageBox.Show("AI P1 got power down of -10!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                ai1_roll = ai1_roll - 36
                MessageBox.Show("AI P1 got power down of -36!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox42.Bounds) Then
                ai1_roll = ai1_roll - 9
                MessageBox.Show("AI P1 got power down of -9!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox65.Bounds) Then
                ai1_roll = ai1_roll - 3
                MessageBox.Show("AI P1 got power down of -3!")
            ElseIf icon_ai1.Bounds.IntersectsWith(PictureBox89.Bounds) Then
                ai1_roll = ai1_roll - 12
                MessageBox.Show("AI P1 got power down of -12!")
            End If

            pb = CType(Me.Controls("PictureBox" & ai1_roll.ToString()), PictureBox)
            ai1pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai1.Location = New Point(pb.Left, pb.Top)
                icon_ai1.BringToFront() ' Bring the icon_player to the front of the form
            End If






        ElseIf ai1_roll > 100 Then
                Dim ai1_rollexceed As Integer = ai1_roll - 100
            ai1_roll = 100 - ai1_rollexceed


            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai1_roll.ToString()), PictureBox)
                ai1pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai1.Location = New Point(pb.Left + 5, pb.Top + (pb.Height - icon_ai1.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_ai1.BringToFront() ' Bring the icon_player to the front of the form
            End If

            'in case player exceeds and goes back, but to 99 specifically.. go downnnnnnnnnn
            If icon_ai1.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                ai1_roll = ai1_roll - 36
                MessageBox.Show("AI P1 got power down of -36!")
            End If
            pb = CType(Me.Controls("PictureBox" & ai1_roll.ToString()), PictureBox)
            ai1pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai1.Location = New Point(pb.Left + 5, pb.Top + (pb.Height - icon_ai1.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_ai1.BringToFront() ' Bring the icon_player to the front of the form
            End If


        ElseIf ai1_roll = 100 Then
                Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai1_roll.ToString()), PictureBox)
            ai1pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai1.Location = New Point(pb.Left, pb.Top)
                icon_ai1.BringToFront() ' Bring the icon_player to the front of the form
            End If
            MessageBox.Show("AI P1 won! Game over.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information)
            setup()
        End If

    End Sub

    Private Sub ai2move()
        'Roll the dice here
        ai2_dicenum1 = rand.Next(1, 7) ' Generate a random number between 1 and 6
        ai2_dicenum2 = rand.Next(1, 7) ' Generate another random number between 1 and 6

        ai2_roll = ai2_roll + ai2_dicenum1 + ai2_dicenum2
        If ai2_roll < 100 Then ' Check if the number is within the range of available PictureBoxes
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai2_roll.ToString()), PictureBox)
            ai2pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai2.Location = New Point(pb.Left, pb.Top)
                icon_ai2.BringToFront() ' Bring the icon_player to the front of the form
            End If


            If icon_ai2.Bounds.IntersectsWith(PictureBox13.Bounds) Then
                ai2_roll = ai2_roll + 6
                MessageBox.Show("AI P2 got power up of 6!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox21.Bounds) Then
                ai2_roll = ai2_roll + 10
                MessageBox.Show("AI P2 got power up of 10!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox32.Bounds) Then
                ai2_roll = ai2_roll + 4
                MessageBox.Show("AI P2 got power up of 4!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox43.Bounds) Then
                ai2_roll = ai2_roll + 7
                MessageBox.Show("AI P2 got power up of 7!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox69.Bounds) Then
                ai2_roll = ai2_roll + 1
                MessageBox.Show("AI P2 got power up of 1!")

                'POWER DOWN HERE
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox16.Bounds) Then
                ai2_roll = ai2_roll - 10
                MessageBox.Show("AI P2 got power down of -10!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                ai2_roll = ai2_roll - 36
                MessageBox.Show("AI P2 got power down of -36!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox42.Bounds) Then
                ai2_roll = ai2_roll - 9
                MessageBox.Show("AI P2 got power down of -9!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox65.Bounds) Then
                ai2_roll = ai2_roll - 3
                MessageBox.Show("AI P2 got power down of -3!")
            ElseIf icon_ai2.Bounds.IntersectsWith(PictureBox89.Bounds) Then
                ai2_roll = ai2_roll - 12
                MessageBox.Show("AI P2 got power down of -12!")
            End If

            pb = CType(Me.Controls("PictureBox" & ai2_roll.ToString()), PictureBox)
            ai2pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai2.Location = New Point(pb.Left, pb.Top)
                icon_ai2.BringToFront() ' Bring the icon_player to the front of the form
            End If






        ElseIf ai2_roll > 100 Then
            Dim ai2_rollexceed As Integer = ai2_roll - 100
            ai2_roll = 100 - ai2_rollexceed


            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai2_roll.ToString()), PictureBox)
            ai2pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai2.Location = New Point(pb.Left + 3, pb.Top + (pb.Height - icon_ai2.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_ai2.BringToFront() ' Bring the icon_player to the front of the form
            End If

            'in case player exceeds and goes back, but to 99 specifically.. go downnnnnnnnnn
            If icon_ai2.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                ai2_roll = ai2_roll - 36
                MessageBox.Show("AI P2 got power down of -36!")
            End If
            pb = CType(Me.Controls("PictureBox" & ai2_roll.ToString()), PictureBox)
            ai2pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai2.Location = New Point(pb.Left + 3, pb.Top + (pb.Height - icon_ai2.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_ai2.BringToFront() ' Bring the icon_player to the front of the form
            End If



        ElseIf ai2_roll = 100 Then
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai2_roll.ToString()), PictureBox)
            ai2pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai2.Location = New Point(pb.Left, pb.Top)
                icon_ai2.BringToFront() ' Bring the icon_player to the front of the form
            End If
            MessageBox.Show("AI P2 won! Game over.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information)
            setup()

        End If
    End Sub

    Private Sub ai3move()
        'Roll the dice here
        ai3_dicenum1 = rand.Next(1, 7) ' Generate a random number between 1 and 6
        ai3_dicenum2 = rand.Next(1, 7) ' Generate another random number between 1 and 6

        ai3_roll = ai3_roll + ai3_dicenum1 + ai3_dicenum2
        If ai3_roll < 100 Then ' Check if the number is within the range of available PictureBoxes
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai3_roll.ToString()), PictureBox)
            ai3pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai3.Location = New Point(pb.Left, pb.Top)
                icon_ai3.BringToFront() ' Bring the icon_player to the front of the form
            End If


            If icon_ai3.Bounds.IntersectsWith(PictureBox13.Bounds) Then
                ai3_roll = ai3_roll + 6
                MessageBox.Show("AI P3 got power up of 6!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox21.Bounds) Then
                ai3_roll = ai3_roll + 10
                MessageBox.Show("AI P3 got power up of 10!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox32.Bounds) Then
                ai3_roll = ai3_roll + 4
                MessageBox.Show("AI P3 got power up of 4!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox43.Bounds) Then
                ai3_roll = ai3_roll + 7
                MessageBox.Show("AI P3 got power up of 7!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox69.Bounds) Then
                ai3_roll = ai3_roll + 1
                MessageBox.Show("AI P3 got power up of 1!")

                'POWER DOWN HERE
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox16.Bounds) Then
                ai3_roll = ai3_roll - 10
                MessageBox.Show("AI P3 got power down of -10!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                ai3_roll = ai3_roll - 36
                MessageBox.Show("AI P3 got power down of -36!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox42.Bounds) Then
                ai3_roll = ai3_roll - 99
                MessageBox.Show("AI P3 got power down of -9!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox65.Bounds) Then
                ai3_roll = ai3_roll - 3
                MessageBox.Show("AI P3 got power down of -3!")
            ElseIf icon_ai3.Bounds.IntersectsWith(PictureBox89.Bounds) Then
                ai3_roll = ai3_roll - 12
                MessageBox.Show("AI P3 got power down of -12!")
            End If

            pb = CType(Me.Controls("PictureBox" & ai3_roll.ToString()), PictureBox)
            ai3pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai3.Location = New Point(pb.Left, pb.Top)
                icon_ai3.BringToFront() ' Bring the icon_player to the front of the form
            End If



        ElseIf ai3_roll > 100 Then
            Dim ai3_rollexceed As Integer = ai3_roll - 100
            ai3_roll = 100 - ai3_rollexceed


            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai3_roll.ToString()), PictureBox)
            ai3pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai3.Location = New Point(pb.Left, pb.Top + (pb.Height - icon_ai3.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_ai3.BringToFront() ' Bring the icon_player to the front of the form
            End If

            'in case player exceeds and goes back, but to 99 specifically.. go downnnnnnnnnn
            If icon_ai3.Bounds.IntersectsWith(PictureBox99.Bounds) Then
                ai3_roll = ai3_roll - 36
                MessageBox.Show("AI P3 got power down of -36!")
            End If
            pb = CType(Me.Controls("PictureBox" & ai3_roll.ToString()), PictureBox)
            ai3pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai3.Location = New Point(pb.Left, pb.Top + (pb.Height - icon_ai3.Height) \ 2) ' Set the location of the icon_player to the middle of the PictureBox
                icon_ai3.BringToFront() ' Bring the icon_player to the front of the form
            End If

        ElseIf ai3_roll = 100 Then
            Dim pb As PictureBox = CType(Me.Controls("PictureBox" & ai3_roll.ToString()), PictureBox)
            ai3pos.Text = "Position: " & pb.Name.Replace("PictureBox", "")
            If pb IsNot Nothing Then ' Check if the PictureBox exists
                icon_ai3.Location = New Point(pb.Left, pb.Top)
                icon_ai3.BringToFront() ' Bring the icon_player to the front of the form
            End If
            MessageBox.Show("AI P3 won! Game over.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information)
            setup()


        End If
    End Sub





    'if AIs are done rolling, then diceclicked = false
End Class
