Imports System.IO
Imports System.Linq
Public Class Form1

    Private filePath As String = Path.Combine(Application.StartupPath, "numbers.txt")

    Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
        If String.IsNullOrWhiteSpace(txtInput.Text) Then
            MessageBox.Show("Enter numbers separated by commas.", "Input Required")
            Return
        End If

        Try
            Dim numbers = txtInput.Text.Split(","c).Select(Function(x) Convert.ToInt32(x.Trim())).ToList()
            File.WriteAllLines(filePath, numbers.Select(Function(n) n.ToString()))
            MessageBox.Show($"Wrote {numbers.Count} numbers to file!", "Write Successful")
            txtInput.Clear()
        Catch
            MessageBox.Show("Invalid input! Please enter only numbers separated by commas.", "Error")
        End Try
    End Sub

    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        If Not File.Exists(filePath) Then
            MessageBox.Show("No file found. Please write numbers first.", "File Missing")
            Return
        End If

        lstNumbers.Items.Clear()
        For Each line In File.ReadAllLines(filePath)
            lstNumbers.Items.Add(line)
        Next
        MessageBox.Show("Numbers read successfully!", "Read Successful")
    End Sub

    Private Sub btnSort_Click(sender As Object, e As EventArgs) Handles btnSort.Click
        If Not File.Exists(filePath) Then
            MessageBox.Show("No file found. Please write numbers first.", "File Missing")
            Return
        End If

        Dim sorted = File.ReadAllLines(filePath).Select(Function(x) Convert.ToInt32(x)).OrderBy(Function(n) n)
        lstNumbers.Items.Clear()
        For Each n In sorted
            lstNumbers.Items.Add(n)
        Next
        MessageBox.Show("Numbers sorted and displayed!", "Sort Successful")
    End Sub
End Class