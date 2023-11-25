namespace fsharpConsoleTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type SudokuTests () =
    // Configuration
    [<TestMethod>]
    member this.TestCreatedCellListHasCorrectIdsForAllIdsOfCell () =
        // Probably break this up into parts:
        ()

    [<TestMethod>]
    member this.TestCorrectRowIdIsAssignedToCells () =
        ()

    [<TestMethod>]
    member this.TestCorrectColumnIdIsAssignedToCells () =
        ()

    [<TestMethod>]
    member this.TestCorrectBlockIdIsAssignedToCells () =
        ()

    [<TestMethod>]
    member this.TestCorrectBlockRowIdIsAssignedToCells () =
        ()

    [<TestMethod>]
    member this.TestCorrectBlockColumnIdIsAssignedToCells () =
        ()

    [<TestMethod>]
    member this.TestCreatedCellsHaveInitialValueStatusOfUnconfirmedEvenIfValueIsNotZero () =
        ()

    [<TestMethod>]
    member this.TestGetRowsReutrnsTheCorrectBlocksForAGivenRowOfCells () =
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

    // During solve:
    [<TestMethod>]
    member this.TestUponStartOfSolveCellsWithNonZeroValueAreMarkedAsGiven () =
        ()

    [<TestMethod>]
    member this.TestUponStartOfSolveCellsWithZeroValueAreMarkedAsUnconfirmed () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()

    [<TestMethod>]
    member this.Test () =
        ()
