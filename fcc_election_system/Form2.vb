Imports MySql.Data.MySqlClient

Public Class Form2
    Dim mysqlConn As MySqlConnection
    Dim command As MySqlCommand
    Public firstNomCount As Integer


    Function hasVoted() As Boolean
        mysqlConn = New MySqlConnection
        mysqlConn.ConnectionString =
            "server = localhost;
             userid = root;
             password = zuze1938zvoto;
             database = fcc_election"
        Dim reader As MySqlDataReader

        mysqlConn.Open()
            Dim query0 As String
            query0 = "SELECT * FROM fcc_election.voter WHERE first_name = 'John'"
            command = New MySqlCommand(query0, mysqlConn)
            reader = command.ExecuteReader
        While reader.Read

            If reader.GetInt64("has_voted") > 0 Then
                mysqlConn.Close()
                Return False
            End If
        End While
        mysqlConn.Close()
    End Function

    Private Sub btnNomA_Click(sender As Object, e As EventArgs) Handles btnNomA.Click
        If hasVoted() = True Then

            mysqlConn = New MySqlConnection
            mysqlConn.ConnectionString =
            "server = localhost;
             userid = root;
             password = zuze1938zvoto;
             database = fcc_election"
            Dim reader As MySqlDataReader


            Try

                mysqlConn.Open()
                Dim query1 As String
                query1 = "SELECT * FROM fcc_election.votes WHERE election_type = 'Presidential'"

                command = New MySqlCommand(query1, mysqlConn)
                reader = command.ExecuteReader

                While reader.Read
                    firstNomCount = reader.GetInt64("first_nominee_votes")
                End While
                firstNomCount = firstNomCount + 1
                mysqlConn.Close()

                mysqlConn.Open()
                Dim query2 As String

                query2 = "UPDATE fcc_election.votes SET first_nominee_votes = '" & firstNomCount & "' WHERE election_type = 'Presidential'"
                command = New MySqlCommand(query2, mysqlConn)
                reader = command.ExecuteReader
                mysqlConn.Close()

                mysqlConn.Open()
                Dim query3 As String
                Dim hasVotedTrue As Integer = 1
                query3 = "UPDATE fcc_election.voter SET has_voted = '" & hasVotedTrue & "' WHERE first_name = 'John'"
                command = New MySqlCommand(query3, mysqlConn)
                reader = command.ExecuteReader
                mysqlConn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try

            MessageBox.Show("You have voted")

        Else
            MessageBox.Show("Sorry You Can only vote once")

        End If
    End Sub
End Class