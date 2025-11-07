Imports System.IO
Imports System.Linq

Public Class Form1

    Private filePath As String = Path.Combine(Application.StartupPath, "numbers.txt")

    Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
        Dim input As String = txtInput.Text.Trim()

        If String.IsNullOrWhiteSpace(input) Then
            MessageBox.Show("Please enter one number.", "Input Required")
            Return
        End If

        If input.Contains(","c) Or input.Contains(" "c) Then
            MessageBox.Show("Only one number allowed at a time.", "Invalid Input")
            Return
        End If

        Dim number As Integer
        If Not Integer.TryParse(input, number) Then
            MessageBox.Show("Invalid input. Please enter a valid integer.", "Error")
            Return
        End If

        Try
            Using writer As New StreamWriter(filePath, True)
                writer.WriteLine(number)
            End Using
            MessageBox.Show($"Number {number} saved to file successfully!", "Write Successful")
            txtInput.Clear()
        Catch ex As Exception
            MessageBox.Show("Error writing to file: " & ex.Message, "File Error")
        End Try
    End Sub


    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        If Not File.Exists(filePath) Then
            MessageBox.Show("No file found. Please write some numbers first.", "File Missing")
            Return
        End If

        Try
            lstNumbers.Items.Clear()
            Dim lines = File.ReadAllLines(filePath)
            For Each line In lines
                Dim num As Integer
                If Integer.TryParse(line, num) Then
                    lstNumbers.Items.Add(num)
                End If
            Next
            MessageBox.Show("Numbers loaded from file!", "Read Successful")
        Catch ex As Exception
            MessageBox.Show("Error reading from file: " & ex.Message, "Read Error")
        End Try
    End Sub


    Private Sub btnSort_Click(sender As Object, e As EventArgs) Handles btnSort.Click
        If lstNumbers.Items.Count = 0 Then
            MessageBox.Show("No numbers to sort. Please click 'Read' first.", "Nothing to Sort")
            Return
        End If

        Try
            Dim numbers = lstNumbers.Items.Cast(Of Integer)().ToList()

            Dim sorted = numbers.OrderBy(Function(n) n).ToList()

            lstNumbers.Items.Clear()
            For Each n In sorted
                lstNumbers.Items.Add(n)
            Next

            File.WriteAllLines(filePath, sorted.Select(Function(n) n.ToString()))

            MessageBox.Show("Numbers sorted and file updated successfully!", "Sort & Update Successful")

        Catch ex As Exception
            MessageBox.Show("Error sorting or updating file: " & ex.Message, "Sort Error")
        End Try
    End Sub

End Class
