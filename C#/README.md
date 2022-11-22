**Compilation command : mcs -out:main.exe main.cs API.cs solver.cs**

## Sudoku_solver_API (API.cs) :

### SudokuAPI class :

* string AskFilePath()

* char[,] RetrieveGrid(string Xpath)

* void ShowGrid(char[,] Xtab)

## Sudoku_solver (solver.cs) :

### Sudoku class :

*  List< char > GetCrossNumbers(char[,] Xtab, int i, int j)