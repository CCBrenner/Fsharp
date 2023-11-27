namespace fsharpConsoleTests

open System
open System.Linq
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type SudokuTests () =
    // Configuration
    [<TestMethod>]
    member this.TestCorrectIdIsAssignedToCells () =
        let cellList = Puzzle.createCellList ()

        Assert.AreEqual(1, cellList[0].Id)
        Assert.AreEqual(34, cellList[33].Id)
        Assert.AreEqual(46, cellList[45].Id)
        Assert.AreEqual(81, cellList[80].Id)
        ()

    [<TestMethod>]
    member this.TestCorrectRowIdIsAssignedToCells () =
        let cellList = Puzzle.createCellList ()

        Assert.AreEqual(1, cellList[0].RowId)
        Assert.AreEqual(2, cellList[9].RowId)
        Assert.AreEqual(4, cellList[30].RowId)
        Assert.AreEqual(5, cellList[42].RowId)
        Assert.AreEqual(9, cellList[80].RowId)
        ()

    [<TestMethod>]
    member this.TestCorrectColumnIdIsAssignedToCells () =
        let cellList = Puzzle.createCellList ()

        Assert.AreEqual(1, cellList[0].ColId)
        Assert.AreEqual(1, cellList[9].ColId)
        Assert.AreEqual(4, cellList[30].ColId)
        Assert.AreEqual(7, cellList[42].ColId)
        Assert.AreEqual(9, cellList[80].ColId)
        ()
    [<TestMethod>]
    member this.TestCorrectBlockIdIsAssignedToCells () =
        let cellList = Puzzle.createCellList ()

        Assert.AreEqual(1, cellList[0].BlockId)
        Assert.AreEqual(1, cellList[9].BlockId)
        Assert.AreEqual(5, cellList[30].BlockId)
        Assert.AreEqual(6, cellList[42].BlockId)
        Assert.AreEqual(9, cellList[80].BlockId)
        ()

    [<TestMethod>]
    member this.TestCorrectBlockRowIdIsAssignedToCells () =
        let cellList = Puzzle.createCellList ()

        Assert.AreEqual(1, cellList[0].BlockRowId)
        Assert.AreEqual(1, cellList[9].BlockRowId)
        Assert.AreEqual(2, cellList[30].BlockRowId)
        Assert.AreEqual(2, cellList[42].BlockRowId)
        Assert.AreEqual(3, cellList[66].BlockRowId)
        Assert.AreEqual(3, cellList[80].BlockRowId)
        ()

    [<TestMethod>]
    member this.TestCorrectBlockColumnIdIsAssignedToCells () =
        let cellList = Puzzle.createCellList ()

        Assert.AreEqual(1, cellList[0].BlockColId)
        Assert.AreEqual(1, cellList[9].BlockColId)
        Assert.AreEqual(2, cellList[30].BlockColId)
        Assert.AreEqual(3, cellList[42].BlockColId)
        Assert.AreEqual(2, cellList[66].BlockColId)
        Assert.AreEqual(3, cellList[80].BlockColId)
        ()

    [<TestMethod>]
    member this.TestLoadingSeedCellValuesIsAccurate () =
        let cellList = Puzzle.createCellList ()
        let seedValues = 
            [ 0; 0; 7;   4; 6; 0;   2; 0; 0;
              0; 3; 0;   0; 0; 0;   4; 0; 0;
              0; 9; 0;   5; 0; 0;   6; 0; 0;

              2; 0; 0;   1; 0; 0;   5; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 0; 0;
              0; 7; 0;   6; 0; 0;   0; 9; 0;

              0; 0; 3;   0; 0; 1;   0; 5; 0;
              0; 1; 0;   7; 0; 0;   0; 8; 0;
              0; 0; 0;   0; 3; 4;   0; 0; 0; ]

        let seededCellList = Puzzle.loadValuesIntoCellList cellList seedValues

        let getCellVal id = (seededCellList |> List.filter (fun (x:Cell.Cell) -> x.Id=id)).FirstOrDefault().Value
        Assert.AreEqual(7, getCellVal 3)
        Assert.AreEqual(4, getCellVal 4)
        Assert.AreEqual(6, getCellVal 5)
        Assert.AreEqual(5, getCellVal 22)
        Assert.AreEqual(8, getCellVal 71)
        
        Assert.AreEqual(0, getCellVal 59)
        Assert.AreEqual(0, getCellVal 81)
        ()
    (*
    
    [<TestMethod>]
    member this.TestCreatedCellsHaveInitialValueStatusOfUnconfirmedEvenIfValueIsNotZero () =
        let cellList = Puzzle.createCellList ()
        

        ()

    [<TestMethod>]
    member this.TestGetRowsReturnsTheCorrectBlocksForAGivenRowOfCells () =
        ()

    [<TestMethod>]
    member this.TestCreatingCellsListSetsNonGivenValuesToCorrespondingValueOfTemplateMatrix () =
        ()

    [<TestMethod>]
    member this.TestGetRowsReturnsTheCorrectCellsForAGivenRowOfCells () =
        ()

    [<TestMethod>]
    member this.TestGetColumnsReturnsTheCorrectCellsForAGivenColumnOfCells () =
        ()

    [<TestMethod>]
    member this.TestGetBlocksReturnsTheCorrectCellsForAGivenBlockOfCells () =
        ()

    // Candidates mgmt:
    [<TestMethod>]
    member this.TestEliminateCandidatesDistinctInRow () =
        ()

    [<TestMethod>]
    member this.TestEliminateCandidatesDistinctInColumn () =
        ()

    [<TestMethod>]
    member this.TestEliminateCandidatesDistinctInBlock () =
        ()

    [<TestMethod>]
    member this.TestEliminateCellsViaCandidateLinesCheck () =
        ()

    [<TestMethod>]
    member this.TestRowColumnAndBlockCandidatesEliminationWorkWhenAppliedTogether () =
        ()
    *)