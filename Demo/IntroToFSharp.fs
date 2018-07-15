namespace DoYouEvenTypeBro

open System

module IntroToFSharp =

    /// BASICS - values and functions

    let aValue = "this is an immutable value of type String"
    //aValue <- "some other string" // Compiler error
    let mutable aVariable = 1
    aVariable <- 2

    let add a b = a + b

    let addResult = add 1 3

    /// this is a record type (product type). It is immutable by default.
    type Customer = {
        Id: Guid
        Name: string
        Age: int
    }

    /// a function that takes 3 parameters and returns a Customer
    let createCustomer name age =
        {
            Id = Guid.NewGuid()
            Name = name
            Age = age
        }

    let newCustomer = createCustomer "Tomek Nowak" 23

    let printMessageIfTrue message flag =
        if flag then printfn message
        else printfn "flag was false"

    printMessageIfTrue "Hello!" false

    /// a generic function that runs another function passed as parameter
    let calculatePrice a b func =
        func a b

    let price1 = calculatePrice 10 12 (fun a b -> a + b)
    let price2 = calculatePrice 10. 12.2 (fun a b -> a * b)

    /// -----------------------------------------------------------------------------
    /// discriminated unions (aka 'Sum Types' or 'Choice Types') and pattern matching
    /// -----------------------------------------------------------------------------

    type MyChoiceType =
        | FirstChoice
        | SecondChoice
        | ThirdChoice
    
    let choose choice =
        match choice with
        | FirstChoice -> "I chose the first option"
        | SecondChoice -> "I chose the second option"
        | ThirdChoice -> "I chose the third option"

    let myChoice = choose MyChoiceType.SecondChoice

    /// more advanced example

    type QueryCustomers =
        | PerCountry of country: string
        | PerStatus of isVIP: bool
        | PerCreationDate of dateFrom: DateTime * dateTo: DateTime
        | PerFavouriteColors of colors: string array
    
    let queryCustomersFromDb query =
        match query with
        | QueryCustomers.PerCountry country -> sprintf "select * from Customers where Country = '%s'" country
        | QueryCustomers.PerStatus vip -> sprintf "select * from Customers where IsVip = %i" (Convert.ToInt32 vip)
        | QueryCustomers.PerCreationDate (fromDate, toDate) -> sprintf "select * from Customers where CreationDate >= '%s' and CreationDate <= '%s'" (string fromDate) (string toDate)
        | QueryCustomers.PerFavouriteColors colors -> sprintf "select * from Customers where FavouriteColor in = (%s)" (colors |> String.concat ",")

    let polishCustomers = queryCustomersFromDb (QueryCustomers.PerCountry "Poland")
    let vipCustomers = queryCustomersFromDb (QueryCustomers.PerStatus true)
    let dateFrom = DateTime.Now.AddDays(-7.)
    let dateTo = DateTime.Now
    let lastWeekCustomers = queryCustomersFromDb (QueryCustomers.PerCreationDate (dateFrom, dateTo))
    let colorCustomers = queryCustomersFromDb (QueryCustomers.PerFavouriteColors [| "red"; "magenta" |])
