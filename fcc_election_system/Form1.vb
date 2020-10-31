Imports MySql.Data.MySqlClient

Public Class Form1
    Dim mysqlConn As MySqlConnection
    Dim command As MySqlCommand

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        mysqlConn = New MySqlConnection
        mysqlConn.ConnectionString =
            "server = localhost; 
             userid = root;
             password = zuze1938zvoto;
             database = fcc_election"
        Dim reader As MySqlDataReader

        Try
            mysqlConn.Open()
            Dim query As String
            query = "SELECT * FROM fcc_election.voter WHERE 
                    email = '" & txtEmail.Text & "' AND password = '" & txtPassword.Text & "' "
            command = New MySqlCommand(query, mysqlConn)
            reader = command.ExecuteReader
            Dim count As Integer
            count = 0

            While reader.Read
                count = count + 1
            End While

            If count = 1 Then

                Form2.Show()
                MessageBox.Show("Welcome to the Voting Panel")
                Me.Hide()
            Else
                MessageBox.Show("invalid credentials")
            End If


            mysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            mysqlConn.Dispose()
        End Try
    End Sub
End Class
