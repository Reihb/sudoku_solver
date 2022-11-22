**Compilation command : mcs -out:main.exe main.cs API.cs solver.cs**

## Sudoku_solver_API :

### SudokuAPI class :

* string AskFilePath()

* char[,] RetrieveGrid(string Xpath)

* void ShowGrid(char[,] Xtab)

## Sudoku_solver

### Sudoku class :

*  List<\char> GetCrossNumbers(char[,] Xtab, int i, int j)