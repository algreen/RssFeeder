namespace UnitTest

open NUnit.Framework

//type Class1() = 
//    member this.X = "F#"

[<TestFixture>]
type BasicTest() =
    
    [<Test>]
    member __.``1 + 1 should equal 2`` () =
        Assert.AreEqual(1 + 1, 2)
