namespace fsharpConsoleTests

open System
open System.Linq
open Microsoft.VisualStudio.TestTools.UnitTesting
open Cell

[<TestClass>]
type SudokuTests () =
    // Configuration
    [<TestMethod>]
    member this.TestCorrectIdIsAssignedToCells () =
        // Arrange
        // Act
        let cellList = Cell.createCellList ()

        // Assert
        Assert.AreEqual(1, cellList[0].Id)
        Assert.AreEqual(34, cellList[33].Id)
        Assert.AreEqual(46, cellList[45].Id)
        Assert.AreEqual(81, cellList[80].Id)
        ()

    [<TestMethod>]
    member this.TestCorrectRowIdIsAssignedToCells () =
        // Arrange
        // Act
        let cellList = createCellList ()

        // Assert
        Assert.AreEqual(1, cellList[0].RowId)
        Assert.AreEqual(2, cellList[9].RowId)
        Assert.AreEqual(4, cellList[30].RowId)
        Assert.AreEqual(5, cellList[42].RowId)
        Assert.AreEqual(9, cellList[80].RowId)
        ()

    [<TestMethod>]
    member this.TestCorrectColumnIdIsAssignedToCells () =
        // Arrange
        // Act
        let cellList = Cell.createCellList ()

        // Assert
        Assert.AreEqual(1, cellList[0].ColId)
        Assert.AreEqual(1, cellList[9].ColId)
        Assert.AreEqual(4, cellList[30].ColId)
        Assert.AreEqual(7, cellList[42].ColId)
        Assert.AreEqual(9, cellList[80].ColId)
        ()

    [<TestMethod>]
    member this.TestCorrectBlockIdIsAssignedToCells () =
        // Arrange
        // Act
        let cellList = Cell.createCellList ()

        // Assert
        Assert.AreEqual(1, cellList[0].BlockId)
        Assert.AreEqual(1, cellList[9].BlockId)
        Assert.AreEqual(5, cellList[30].BlockId)
        Assert.AreEqual(6, cellList[42].BlockId)
        Assert.AreEqual(9, cellList[80].BlockId)
        ()

    [<TestMethod>]
    member this.TestLoadingSeedCellValuesIsAccurate () =
        // Arrange
        let cellList = Cell.createCellList ()
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

        // Act
        let seededCellList = Puzzle.loadValuesIntoCellList seedValues cellList

        // Assert
        let getCellVal id = (seededCellList |> List.filter (fun x -> x.Id=id)).FirstOrDefault().Value
        // there is another way to do .FirstOrDefault() in a functional way using F#, but not sure what it is...
        Assert.AreEqual(7, getCellVal 3)
        Assert.AreEqual(4, getCellVal 4)
        Assert.AreEqual(6, getCellVal 5)
        Assert.AreEqual(5, getCellVal 22)
        Assert.AreEqual(8, getCellVal 71)
        
        Assert.AreEqual(0, getCellVal 59)
        Assert.AreEqual(0, getCellVal 81)
        ()
    
    [<TestMethod>]
    member this.TestCreatedCellsHaveInitialValueStatusOfUnconfirmedEvenIfValueIsNotZeroWithoutLoadedSeedValues () =
        // Arrange
        let cellList = Cell.createCellList ()

        // Act
        // Assert
        let getCellValStatus id = (cellList |> List.filter (fun x -> x.Id=id)).FirstOrDefault().ValueStatus
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 1)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 6)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 57)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 78)
        ()

    [<TestMethod>]
    member this.TestCreatedCellsHaveInitialValueStatusOfUnconfirmedEvenIfValueIsNotZeroWithLoadedSeedValues () = 
        // Arrange
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

        let cellList =
            Cell.createCellList ()
        // Act
            |> Puzzle.loadValuesIntoCellList seedValues
        
        // Assert
        let getCellVal id = (cellList |> List.filter (fun x -> x.Id=id)).FirstOrDefault().Value
        let getCellValStatus id = (cellList |> List.filter (fun x -> x.Id=id)).FirstOrDefault().ValueStatus
        Assert.AreEqual(0, getCellVal 1)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 1)
        Assert.AreEqual(0, getCellVal 6)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 6)
        Assert.AreEqual(3, getCellVal 57)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 57)
        Assert.AreEqual(4, getCellVal 78)
        Assert.AreEqual(Cell.ValueStatus.Unconfirmed, getCellValStatus 78)

    [<TestMethod>]
    member this.TestGettingCellsOfARowReturnsTheCorrectCells () =
        // Arrange
        let cellList = Cell.createCellList ()

        // Act
        // Assert
        let cellsOfRow = cellList |> Cell.getCellsOfRow 4
        
        Assert.AreEqual(9, List.length cellsOfRow)
        Assert.AreEqual(28, cellsOfRow[0].Id)
        Assert.AreEqual(31, cellsOfRow[3].Id)
        Assert.AreEqual(34, cellsOfRow[6].Id)
        Assert.AreEqual(36, cellsOfRow[8].Id)

        let cellsOfRow = cellList |> Cell.getCellsOfRow 9

        Assert.AreEqual(9, List.length cellsOfRow)
        Assert.AreEqual(73, cellsOfRow[0].Id)
        Assert.AreEqual(76, cellsOfRow[3].Id)
        Assert.AreEqual(79, cellsOfRow[6].Id)
        Assert.AreEqual(81, cellsOfRow[8].Id)
        ()

    [<TestMethod>]
    member this.TestGettingCellsOfAColumnReturnsTheCorrectCells () =
        // Arrange
        let cellList = Cell.createCellList ()

        // Act
        // Assert
        let cellsOfCol = cellList |> Cell.getCellsOfColumn 4

        Assert.AreEqual(9, List.length cellsOfCol)
        Assert.AreEqual(4, cellsOfCol[0].Id)
        Assert.AreEqual(31, cellsOfCol[3].Id)
        Assert.AreEqual(58, cellsOfCol[6].Id)
        Assert.AreEqual(76, cellsOfCol[8].Id)

        let cellsOfCol = cellList |> Cell.getCellsOfColumn 9

        Assert.AreEqual(9, List.length cellsOfCol)
        Assert.AreEqual(9, cellsOfCol[0].Id)
        Assert.AreEqual(36, cellsOfCol[3].Id)
        Assert.AreEqual(63, cellsOfCol[6].Id)
        Assert.AreEqual(81, cellsOfCol[8].Id)
        ()

    [<TestMethod>]
    member this.TestGettingCellsOfABlockReturnsTheCorrectCells () =
        // Arrange
        let cellList = Cell.createCellList ()

        // Act
        // Assert
        let cellsOfBlock = cellList |> Cell.getCellsOfBlock 4

        Assert.AreEqual(9, List.length cellsOfBlock)
        Assert.AreEqual(28, cellsOfBlock[0].Id)
        Assert.AreEqual(37, cellsOfBlock[3].Id)
        Assert.AreEqual(46, cellsOfBlock[6].Id)
        Assert.AreEqual(48, cellsOfBlock[8].Id)

        let cellsOfBlock = cellList |> Cell.getCellsOfBlock 9

        Assert.AreEqual(9, List.length cellsOfBlock)
        Assert.AreEqual(61, cellsOfBlock[0].Id)
        Assert.AreEqual(70, cellsOfBlock[3].Id)
        Assert.AreEqual(79, cellsOfBlock[6].Id)
        Assert.AreEqual(81, cellsOfBlock[8].Id)
        ()

    [<TestMethod>]
    member this.TestSetGivenCellsBasedOnValuePresent () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
        // Act
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue

        // Assert
        let getCellViaMatrix (rowId, colId) = cellList |> Cell.getCellViaMatrix (rowId+1, colId+1)

        Assert.AreEqual(ValueStatus.Given, (getCellViaMatrix (0,2)).ValueStatus);
        Assert.AreEqual(ValueStatus.Given, (getCellViaMatrix (1,2)).ValueStatus);
        Assert.AreEqual(ValueStatus.Given, (getCellViaMatrix (3,0)).ValueStatus);
        Assert.AreEqual(ValueStatus.Given, (getCellViaMatrix (4,0)).ValueStatus);
        Assert.AreEqual(ValueStatus.Given, (getCellViaMatrix (8,6)).ValueStatus);
        ()
    // End Configuration.

    // Candidates mgmt:
    [<TestMethod>]
    member this.TestEliminateCandidatesDistinctInRow () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue  // required
        // Act
            |> Candidates.eliminateCandidatesByDistinctInRow

        // Assert
        let getCellViaMatrix (rowId, colId) = cellList |> Cell.getCellViaMatrix (rowId+1, colId+1)

        // Row 0:
        // 5 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (0,0)).Values[5]);
        Assert.AreEqual(0, (getCellViaMatrix (0,8)).Values[5])
        // 4 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (0,0)).Values[4]);
        Assert.AreEqual(0, (getCellViaMatrix (0,8)).Values[4]);
        // 3 is a candidate
        Assert.AreEqual(3, (getCellViaMatrix (0,0)).Values[3]);
        Assert.AreEqual(3, (getCellViaMatrix (0,8)).Values[3]);
        // 1 is a candidate
        Assert.AreEqual(1, (getCellViaMatrix (0,0)).Values[1]);
        Assert.AreEqual(1, (getCellViaMatrix (0,8)).Values[1]);

        // Row 4:
        // 2 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (4,4)).Values[2]);
        Assert.AreEqual(0, (getCellViaMatrix (4,6)).Values[2]);
        // 3 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (4,4)).Values[3]);
        Assert.AreEqual(0, (getCellViaMatrix (4,6)).Values[3]);
        // 6 is a candidate
        Assert.AreEqual(6, (getCellViaMatrix (4,4)).Values[6]);
        Assert.AreEqual(6, (getCellViaMatrix (4,6)).Values[6]);
        // 5 is a candidate
        Assert.AreEqual(5, (getCellViaMatrix (4,4)).Values[5]);
        Assert.AreEqual(5, (getCellViaMatrix (4,6)).Values[5]);
        ()

    [<TestMethod>]
    member this.TestEliminateCandidatesDistinctInColumn () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue  // required
        // Act
            |> Candidates.eliminateCandidatesByDistinctInColumn

        // Assert
        let getCellViaMatrix (rowId, colId) = cellList |> Cell.getCellViaMatrix (rowId+1, colId+1)

        // Column 0:
        // 2 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (1,0)).Values[2]);
        Assert.AreEqual(0, (getCellViaMatrix (8,0)).Values[2]);
        // 4 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (1,0)).Values[4]);
        Assert.AreEqual(0, (getCellViaMatrix (8,0)).Values[4]);
        // 3 is a candidate
        Assert.AreEqual(3, (getCellViaMatrix (1,0)).Values[3]);
        Assert.AreEqual(3, (getCellViaMatrix (8,0)).Values[3]);
        // 1 is a candidate
        Assert.AreEqual(1, (getCellViaMatrix (1,0)).Values[1]);
        Assert.AreEqual(1, (getCellViaMatrix (8,0)).Values[1]);

        // Column 4:
        // 4 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (1,4)).Values[4]);
        Assert.AreEqual(0, (getCellViaMatrix (7,4)).Values[4]);
        // 5 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (1,4)).Values[5]);
        Assert.AreEqual(0, (getCellViaMatrix (7,4)).Values[5]);
        // 6 is a candidate
        Assert.AreEqual(6, (getCellViaMatrix (1,4)).Values[6]);
        Assert.AreEqual(6, (getCellViaMatrix (7,4)).Values[6]);
        // 2 is a candidate
        Assert.AreEqual(2, (getCellViaMatrix (1,4)).Values[2]);
        Assert.AreEqual(2, (getCellViaMatrix (7,4)).Values[2]);
        ()

    [<TestMethod>]
    member this.TestEliminateCandidatesDistinctInBlock () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue  // required
        // Act
            |> Candidates.eliminateCandidatesByDistinctInBlock

        // Assert
        let getCellViaMatrix (rowId, colId) = cellList |> Cell.getCellViaMatrix (rowId+1, colId+1)

        // Block 0:
        // 5 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (0,0)).Values[5]); 
        Assert.AreEqual(0, (getCellViaMatrix (2,2)).Values[5]);
        // 3 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (0,0)).Values[3]);
        Assert.AreEqual(0, (getCellViaMatrix (2,2)).Values[3]);
        // 7 is a candidate
        Assert.AreEqual(7, (getCellViaMatrix (0,0)).Values[7]);
        Assert.AreEqual(7, (getCellViaMatrix (2,2)).Values[7]);
        // 1 is a candidate
        Assert.AreEqual(1, (getCellViaMatrix (0,0)).Values[1]);
        Assert.AreEqual(1, (getCellViaMatrix (2,2)).Values[1]);

        // Block 4:
        // 8 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (3,5)).Values[8]);
        Assert.AreEqual(0, (getCellViaMatrix (5,3)).Values[8]);
        // 3 is NOT a candidate
        Assert.AreEqual(0, (getCellViaMatrix (3,5)).Values[3]);
        Assert.AreEqual(0, (getCellViaMatrix (5,3)).Values[3]);
        // 6 is a candidate
        Assert.AreEqual(6, (getCellViaMatrix (3,5)).Values[6]);
        Assert.AreEqual(6, (getCellViaMatrix (5,3)).Values[6]);
        // 5 is a candidate
        Assert.AreEqual(5, (getCellViaMatrix (3,5)).Values[5]);
        Assert.AreEqual(5, (getCellViaMatrix (5,3)).Values[5]);
        ()

    [<TestMethod>]
    member this.TestEliminateCandidatesForGivenAndConfirmedCells () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue  // required
            |> List.map (fun x -> if x.Id=1 then { x with Value=1; ValueStatus=ValueStatus.Confirmed } else x)
        // Act
            |> Candidates.eliminateCandidatesForGivenAndConfirmedCells

        // Assert
        let getCellViaMatrix (rowId, colId) = cellList |> Cell.getCellViaMatrix (rowId+1, colId+1)

        // cell.Value=5:  (given)
        Assert.AreEqual (0, (getCellViaMatrix (0,2)).Values[5])
        Assert.AreEqual (0, (getCellViaMatrix (0,2)).Values[3])
        Assert.AreEqual (0, (getCellViaMatrix (0,2)).Values[8])

        // cell.Value=9:  (confirmed)
        Assert.AreEqual (0, (getCellViaMatrix (0,0)).Values[9])
        Assert.AreEqual (0, (getCellViaMatrix (0,0)).Values[5])
        Assert.AreEqual (0, (getCellViaMatrix (0,0)).Values[3])

        // cell.Value=0:  (control)
        Assert.AreEqual (5, (getCellViaMatrix (2,0)).Values[5])
        Assert.AreEqual (3, (getCellViaMatrix (2,0)).Values[3])
        Assert.AreEqual (8, (getCellViaMatrix (2,0)).Values[8])
        ()
    [<TestMethod>]
    member this.TestRowColumnAndBlockCandidatesEliminationWorkWhenAppliedTogether () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue  // required
        // Act
            |> Candidates.eliminateCandidatesByDistinctInRow
            |> Candidates.eliminateCandidatesByDistinctInColumn
            |> Candidates.eliminateCandidatesByDistinctInBlock

        // Assert
        let getCellViaMatrix (rowId, colId) = cellList |> Cell.getCellViaMatrix (rowId, colId)
        let cellCoords = (2,2)
        
        // 2 is NOT a candidate (row)
        Assert.AreEqual(0, (getCellViaMatrix cellCoords).Values[2]);
        // 8 is NOT a candidate (column)
        Assert.AreEqual(0, (getCellViaMatrix cellCoords).Values[8]);
        // 5 is NOT a candidate (block)
        Assert.AreEqual(0, (getCellViaMatrix cellCoords).Values[5]); 
        // 7 is a candidate
        Assert.AreEqual(7, (getCellViaMatrix cellCoords).Values[7]);
        // 1 is a candidate
        Assert.AreEqual(1, (getCellViaMatrix cellCoords).Values[1]);
        ()

    [<TestMethod>]
    member this.TestCreateCellManagerFromCellListAndOlderPastCellManager () =
        // Arrange
        let cellList0 = Cell.createCellList ()
        let cM0 = cellList0 |> CellManager.create
        let cM0' = cM0 |> CellManager.goToNextCell |> CellManager.goToNextCell

        // Act
        let testResult = cM0' |> CellManager.createCellManagerFromCellListAndOlderCellManager cellList0

        // Assert
        let prev = [cellList0[0]; cellList0[1]]
        let curr = cellList0[2]
        Assert.AreEqual(prev, testResult.Previous)        
        Assert.AreEqual(curr, testResult.Current)        
        Assert.AreEqual(78, (List.length testResult.Remaining))
    (*
    [<TestMethod>]
    member this.TestGetNextCandidateReturnsNextCandidate () =
        // Arrange
        let seedValues = 
            [ 0; 0; 5;   0; 4; 0;   0; 8; 0;
              0; 0; 3;   0; 0; 2;   0; 0; 0;
              0; 0; 0;   0; 0; 0;   0; 9; 1;

              8; 0; 0;   7; 0; 0;   0; 1; 0;
              2; 0; 0;   8; 0; 3;   0; 0; 7;
              0; 6; 0;   0; 0; 4;   0; 0; 9;

              4; 3; 0;   0; 0; 0;   0; 0; 0;
              0; 0; 0;   9; 0; 0;   1; 0; 0;
              0; 8; 0;   0; 5; 0;   6; 0; 0; ]

        let cellList =
            Cell.createCellList ()
            |> Puzzle.loadValuesIntoCellList seedValues
            // (start of the solve algo:)
            |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue

        // Act

        // Assert
        Assert.AreEqual ()
        ()
    (*)
    [<TestMethod>]
    member this.TestGetNextCandidateReturnsNoneOptionWhenNoMoreCandidates () =
        ()
    [<TestMethod>]
    member this.TestGoBackToPreviousCellResetsCandidatesOfCurrentCellBeforeMakingPreviousCellTheCurrentCell () =
        ()
    [<TestMethod>]
    member this.TestGoBackToPreviousCellMakesPreviousCellTheCurrentCell () =
        ()
    [<TestMethod>]
    member this.TestCheckIfPuzzleIsStillSolvableBasedOnCandidatesInRowsColumnsAndBlocks () =
        ()
    *)
    (*
    // BlockRow and BlockCol items are not needed at this time.
    // These first four tests are complete.
    [<TestMethod>]
    member this.TestCorrectBlockRowIdIsAssignedToCells () =
        let cellList = Cell.createCellList ()

        Assert.AreEqual(1, cellList[0].BlockRowId)
        Assert.AreEqual(1, cellList[9].BlockRowId)
        Assert.AreEqual(2, cellList[30].BlockRowId)
        Assert.AreEqual(2, cellList[42].BlockRowId)
        Assert.AreEqual(3, cellList[66].BlockRowId)
        Assert.AreEqual(3, cellList[80].BlockRowId)
        ()

    [<TestMethod>]
    member this.TestCorrectBlockColumnIdIsAssignedToCells () =
        let cellList = Cell.createCellList ()

        Assert.AreEqual(1, cellList[0].BlockColId)
        Assert.AreEqual(1, cellList[9].BlockColId)
        Assert.AreEqual(2, cellList[30].BlockColId)
        Assert.AreEqual(3, cellList[42].BlockColId)
        Assert.AreEqual(2, cellList[66].BlockColId)
        Assert.AreEqual(3, cellList[80].BlockColId)
        ()
    [<TestMethod>]
    member this.TestGettingCellsOfABlockRowReturnsTheCorrectCells () =
        let cellList = Cell.createCellList ()

        let cellsOfBlockRow = cellList |> Cell.getCellsOfBlockRow 1

        Assert.AreEqual(27, List.length cellsOfBlockRow)
        Assert.AreEqual(1, cellsOfBlockRow[0].Id)
        Assert.AreEqual(10, cellsOfBlockRow[9].Id)
        Assert.AreEqual(14, cellsOfBlockRow[13].Id)
        Assert.AreEqual(27, cellsOfBlockRow[26].Id)

        let cellsOfBlockRow = cellList |> Cell.getCellsOfBlockRow 2

        Assert.AreEqual(27, List.length cellsOfBlockRow)
        Assert.AreEqual(28, cellsOfBlockRow[0].Id)
        Assert.AreEqual(40, cellsOfBlockRow[12].Id)
        Assert.AreEqual(50, cellsOfBlockRow[22].Id)
        Assert.AreEqual(54, cellsOfBlockRow[26].Id)
        ()

    [<TestMethod>]
    member this.TestGettingCellsOfABlockColumnReturnsTheCorrectCells () =
        let cellList = Cell.createCellList ()

        let cellsOfBlockCol = cellList |> Cell.getCellsOfBlockColumn 1

        Assert.AreEqual(27, List.length cellsOfBlockCol)
        Assert.AreEqual(1, cellsOfBlockCol[0].Id)
        Assert.AreEqual(28, cellsOfBlockCol[9].Id)
        Assert.AreEqual(38, cellsOfBlockCol[13].Id)
        Assert.AreEqual(75, cellsOfBlockCol[26].Id)

        let cellsOfBlockCol = cellList |> Cell.getCellsOfBlockColumn 3

        Assert.AreEqual(27, List.length cellsOfBlockCol)
        Assert.AreEqual(7, cellsOfBlockCol[0].Id)
        Assert.AreEqual(43, cellsOfBlockCol[12].Id)
        Assert.AreEqual(71, cellsOfBlockCol[22].Id)
        Assert.AreEqual(81, cellsOfBlockCol[26].Id)
        ()
    // 4 more functions, 2 that return blocks, 1 that returns rows, 1 that returns cols
    *)
