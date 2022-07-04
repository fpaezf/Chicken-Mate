

Public Class Chicken

    'Chicken animations as bitmap arrays
    Dim AnimationDirection As String
    Dim WalkLeft As Bitmap() = New Bitmap(3) {My.Resources.Walk_Left1, My.Resources.Walk_Left2, My.Resources.Walk_Left1, My.Resources.Walk_Left3}
    Dim WalkRight As Bitmap() = New Bitmap(3) {My.Resources.Walk_Right1, My.Resources.Walk_Right2, My.Resources.Walk_Right1, My.Resources.Walk_Right3}

    Dim PickLeft As Bitmap() = New Bitmap(7) {My.Resources.Pick_Left1, My.Resources.Pick_Left2, My.Resources.Pick_Left3, My.Resources.Pick_Left2, My.Resources.Pick_Left3, My.Resources.Pick_Left2, My.Resources.Pick_Left3, My.Resources.Pick_Left1}
    Dim PickRight As Bitmap() = New Bitmap(7) {My.Resources.Pick_Right1, My.Resources.Pick_Right2, My.Resources.Pick_Right3, My.Resources.Pick_Right2, My.Resources.Pick_Right3, My.Resources.Pick_Right2, My.Resources.Pick_Right3, My.Resources.Pick_Right1}

    Dim frame As Integer = 0


    'Variables to handle chicken mouse drag
    Dim Is_Dragging As Boolean = False
    Dim MouseX As Integer
    Dim MouseY As Integer

    'Chicken actions
    Dim FallSpeed As Integer = 0
    Dim Is_Walking As Boolean = True
    Dim Is_Falling As Boolean = False
    Dim Is_Picking As Boolean = False

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000      'NO_IDEA            <- Unsure about this shit.
            cp.ExStyle = cp.ExStyle Or &H80           'WS_EX_TOOLWINDOW   <- Remove sheep(s) from ALT-TAB list.
            cp.ExStyle = cp.ExStyle Or &H8            'WS_EX_TOPMOST      <- Set sheep(s) TopMost on Z index.
            cp.ExStyle = cp.ExStyle Or &H80000        'WS_EX_LAYERED      <- Increase overall paint performance (Still not sure if it works).
            'cp.ExStyle = cp.ExStyle Or &H20          'WS_EX_TRANSPARENT  <- Do not draw window (Makes sheep unclickable, discarded).
            cp.ExStyle = cp.ExStyle Or &H8000000      'WS_EX_NOACTIVATE   <- prevent focus when created (Not sure if it's nedded).
            cp.Style = cp.Style Or &H80000000         'WS_POPUP           <- Unsure about this shit, disabled.
            cp.ClassStyle = cp.ClassStyle Or &H20000  'CS_DROPSHADOW      < -Cancel Drop shadow
            Return cp
        End Get
    End Property

    Public Sub StopAllAnimations()
        Is_Walking = False
        Is_Falling = False
        Is_Picking = False
        frame = 0
    End Sub

    Private Sub Animation_Tick(sender As Object, e As EventArgs) Handles Animation.Tick
        If Is_Walking = True Then
            Animation.Interval = 150
            If AnimationDirection = "Left" Then
                If frame < WalkLeft.Count - 1 Then
                    PictureBox1.Image = WalkLeft(frame)
                    frame = frame + 1
                Else
                    PictureBox1.Image = WalkLeft(frame)
                    frame = 0
                End If
                Me.Location = New Point(Me.Location.X - 5, Me.Location.Y)
                If Me.Location.X < 1 Then
                    AnimationDirection = "Right"
                End If
            ElseIf AnimationDirection = "Right" Then
                If frame < WalkRight.Count - 1 Then
                    PictureBox1.Image = WalkRight(frame)
                    frame = frame + 1
                Else
                    PictureBox1.Image = WalkRight(frame)
                    frame = 0
                End If
                Me.Location = New Point(Me.Location.X + 5, Me.Location.Y)
                If Me.Location.X > Screen.PrimaryScreen.WorkingArea.Width - Me.Width Then
                    AnimationDirection = "Left"
                End If
            End If







        ElseIf Is_Falling = True Then
            Animation.Interval = 30
            FallSpeed = FallSpeed + 1
            If FallSpeed > 30 Then
                FallSpeed = 30
            End If


            If frame < WalkLeft.Count - 1 Then
                PictureBox1.Image = WalkLeft(frame)
                frame = frame + 1
            Else
                PictureBox1.Image = WalkLeft(frame)
                frame = 0
            End If
            Me.Location = New Point(Me.Location.X, Me.Location.Y + FallSpeed)
            If Me.Location.Y >= Screen.PrimaryScreen.WorkingArea.Height - Me.Height Then
                StopAllAnimations()
                Me.Location = New Point(Me.Location.X, Screen.PrimaryScreen.WorkingArea.Height - Me.Height)
                My.Computer.Audio.Play(My.Resources.crash, AudioPlayMode.Background)
                PictureBox1.Image = My.Resources.crash_bottom
                Application.DoEvents()
                Threading.Thread.Sleep(2000)
                Is_Walking = True
            End If




        ElseIf Is_Dragging = True Then
            Animation.Interval = 50
            StopAllAnimations()



        ElseIf Is_Picking = True Then
            Animation.Interval = 300
            If AnimationDirection = "Left" Then
                If frame < PickLeft.Count - 1 Then
                    PictureBox1.Image = PickLeft(frame)
                    frame = frame + 1
                Else
                    PictureBox1.Image = PickLeft(frame)
                    frame = 0
                End If
            ElseIf AnimationDirection = "Right" Then
                If frame < PickRight.Count - 1 Then
                    PictureBox1.Image = PickRight(frame)
                    frame = frame + 1
                Else
                    PictureBox1.Image = PickRight(frame)
                    frame = 0
                End If
            End If










        End If

    End Sub

    Private Sub Chicken_Load(sender As Object, e As EventArgs) Handles Me.Load
        AnimationDirection = "Left"
        Animation.Enabled = True
        PictureBox1.BackColor = Color.Green
        Me.TransparencyKey = Color.Green
        Me.Location = New Point(Me.Location.X, Screen.PrimaryScreen.WorkingArea.Height - Me.Height)
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        StopAllAnimations()
        Is_Dragging = True
        MouseX = Windows.Forms.Cursor.Position.X - Me.Left
        MouseY = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        Is_Dragging = False
        FallSpeed = 0
        Is_Falling = True
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If Is_Dragging = True Then
            Me.Top = Windows.Forms.Cursor.Position.Y - MouseY
            Me.Left = Windows.Forms.Cursor.Position.X - MouseX
        End If
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        Is_Dragging = False
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        End
    End Sub

    Private Sub DoSpecialAnimation_Tick(sender As Object, e As EventArgs) Handles DoSpecialAnimation.Tick
        If Not Is_Falling = True Then
            If Is_Picking = False Then
                StopAllAnimations()
                Is_Picking = True
            Else
                StopAllAnimations()
                Is_Walking = True
            End If
        End If
    End Sub


End Class