#r "src/Qtfy.Numerics.LinearAlgebra/bin/Debug/net10.0/Qtfy.Numerics.LinearAlgebra.dll"

using Qtfy.Numerics.LinearAlgebra;

var rows = 2;
var columns = 3;
var matrix = new Matrix<double>(rows, columns, isRowMajor: true);

var value = 1.0;
for (var r = 0; r < rows; r++)
{
    for (var c = 0; c < columns; c++)
    {
        matrix[r, c] = value++;
    }
}

var view = matrix.AsView();

System.Console.WriteLine($"Matrix {view.Rows}x{view.Columns}, rowSpan={view.RowSpan}, colSpan={view.ColSpan}");
PrintMatrix(view);

var row1 = view.Row(1);
var rowSum = 0.0;
for (var i = 0; i < row1.Length; i++)
{
    rowSum += row1[i];
}

var col0 = view.Column(0);
var colSum = 0.0;
for (var i = 0; i < col0.Length; i++)
{
    colSum += col0[i];
}

System.Console.WriteLine($"Row 1 sum: {rowSum}");
System.Console.WriteLine($"Column 0 sum: {colSum}");

static void PrintMatrix(MatrixView<double> matrix)
{
    for (var r = 0; r < matrix.Rows; r++)
    {
        for (var c = 0; c < matrix.Columns; c++)
        {
            System.Console.Write($"{matrix[r, c],6:0.##}");
        }

        System.Console.WriteLine();
    }
}
