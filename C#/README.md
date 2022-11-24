**Compilation command : mcs -out:main.exe main.cs API.cs debug.cs solver.cs**

## Sudoku_solver_API (API.cs) :

### SudokuAPI class :

* string AskFilePath()

* char[,] RetrieveGrid(string Xpath)

## Sudoku_solver (solver.cs) :

### Sudoku class :

* List< char > GetCrossNumbers(char[,] Xgrid, int i, int j)

* void SetRegionWH(char[,] Xgrid, out int regionWidth, out int regionHeight)

* List< char > GetRegionNumbers(char[,] Xgrid, int XregionID)

* GetPossibilitiesFromList(char[,] Xgrid, List< char > Xlist1, List< char > Xlist2)

## Sudoku_solver_debug (debug.cs) :

## Debug class :

* void ShowListChar(List< char > Xlist)

* void ShowGrid(char[,] Xgrid)