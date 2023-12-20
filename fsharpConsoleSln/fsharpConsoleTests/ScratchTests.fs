namespace fsharpConsoleTests

open System.Linq
open Microsoft.VisualStudio.TestTools.UnitTesting
open Cell

[<TestClass>]
type ScratchTests () =
    // Configuration
    [<TestMethod>]
    member this.TestRecursiveMethod () =
        