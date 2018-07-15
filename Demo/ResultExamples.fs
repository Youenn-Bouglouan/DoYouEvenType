namespace DoYouEvenTypeBro

open System
open System.Collections.Generic

/// Helper 3rd-party library to save to the database
module Db =

    exception DbException of message: string

    let store = new List<obj>()

    /// Save an entity to the database
    let save entity =
        let o = box entity
        if store.Contains o then
            raise (DbException "Operation failed: the object already exists in database!")
        else
            store.Add o
            entity

    /// Clear the content of the database        
    let clear () =
        store.Clear ()
    
type CustomerDto = {
    FirstName: string
    LastName: string
    Age: int
}

type Customer = {
    Id: string
    Name: string
    Age: int
}

/// Our own API to create a new customer
module ExceptionExample =

    let validateDto customerDto =
        not (String.IsNullOrWhiteSpace customerDto.FirstName) &&
        not (String.IsNullOrWhiteSpace customerDto.LastName) &&
        customerDto.Age >= 18

    let convertToCustomer customerDto =
        {
            Id = ((string customerDto.FirstName.[0]) + customerDto.LastName).ToUpper ()
            Name = customerDto.FirstName + " " + customerDto.LastName
            Age = customerDto.Age
        }

    let createCustomer customerDto =
        try
            let isValid = validateDto customerDto
            if isValid then
                let customer = convertToCustomer customerDto
                let savedCustomer = Db.save customer
                savedCustomer
            else
                Unchecked.defaultof<Customer> // the customer could not be saved, return null!
        with
            | :? Db.DbException as ex ->
                printfn "Oh no, a DB error occurred: %A" ex |> ignore
                Unchecked.defaultof<Customer> // the customer could not be saved, return null!
            | ex ->
                printfn "Oh no, an unexpected error occurred: %A" ex |> ignore
                Unchecked.defaultof<Customer> // the customer could not be saved, return null!

    // ----------------------------------------------------------------------------------------
    // Tests

    Db.clear ()
    Db.store

    let customer1 = createCustomer { FirstName = "Tomek"; LastName = "Nowak"; Age = 12 }
    let customer2 = createCustomer { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }
    let customer3 = createCustomer { FirstName = "Tomek"; LastName = "Kowalski"; Age = 18 }
    let customer4 = createCustomer { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }








// solution 1

/// Our own API to create a new customer, using Result and simple ROP technics
module ResultExampleWithSimpleROP =

    let validateDto customerDto =
        if not (String.IsNullOrWhiteSpace customerDto.FirstName) &&
           not (String.IsNullOrWhiteSpace customerDto.LastName) &&
           customerDto.Age >= 18 then
            Ok customerDto
        else
            Error "the customer dto is not valid"

    let convertToCustomer customerDto =
        {
            Id = ((string customerDto.FirstName.[0]) + customerDto.LastName).ToUpper ()
            Name = customerDto.FirstName + " " + customerDto.LastName
            Age = customerDto.Age
        }

    let saveCustomer customer =
        try
            Db.save customer |> Ok
        with
            | :? Db.DbException as ex -> Error (sprintf "%A" ex)
            | ex -> Error (sprintf "%A" ex)

    let createCustomer customerDto =
        match validateDto customerDto with
        | Error err ->
            printfn "There was a validation error"
            Error err
        | Ok dto ->
            let customer = convertToCustomer dto
            match saveCustomer customer with
            | Error err ->
                printfn "A DB error occurred"
                Error err
            | Ok savedCustomer ->
                Ok savedCustomer

    let runIfOk func result =
        match result with
        | Error err -> Error err
        | Ok res -> func res

    let convertToCustomerV2 customerDto =
        {
            Id = ((string customerDto.FirstName.[0]) + customerDto.LastName).ToUpper ()
            Name = customerDto.FirstName + " " + customerDto.LastName
            Age = customerDto.Age
        } |> Ok

    let createCustomerV2 customerDto =
        customerDto
        |> Ok
        |> runIfOk validateDto
        |> runIfOk convertToCustomerV2
        |> runIfOk saveCustomer

    // ----------------------------------------------------------------------------------------
    // Tests

    Db.clear ()
    Db.store

    let customer1 = createCustomer { FirstName = "Tomek"; LastName = "Nowak"; Age = 12 }
    let customer2 = createCustomer { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }
    let customer3 = createCustomer { FirstName = "Tomek"; LastName = "Kowalski"; Age = 18 }
    let customer4 = createCustomer { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }

    let customer1' = createCustomerV2 { FirstName = "Tomek"; LastName = "Nowak"; Age = 12 }
    let customer2' = createCustomerV2 { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }
    let customer3' = createCustomerV2 { FirstName = "Tomek"; LastName = "Kowalski"; Age = 18 }
    let customer4' = createCustomerV2 { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }



// solution 2

/// Our own API to create a new customer, using ROP technics and strongly typed errors
module ResultExampleWithErrorDU =

    type CustomerError =
        | ValidationFailed
        | DbErrorWhileSaving of message: string
        | UnexpectedError of message: string

    let validateDto customerDto =
        if not (String.IsNullOrWhiteSpace customerDto.FirstName) &&
           not (String.IsNullOrWhiteSpace customerDto.LastName) &&
           customerDto.Age >= 18 then
            Ok customerDto
        else
            Error CustomerError.ValidationFailed

    let convertToCustomer customerDto =
        {
            Id = ((string customerDto.FirstName.[0]) + customerDto.LastName).ToUpper ()
            Name = customerDto.FirstName + " " + customerDto.LastName
            Age = customerDto.Age
        } |> Ok

    let saveCustomer customer =
        try
            Db.save customer |> Ok
        with
            | :? Db.DbException as ex -> CustomerError.DbErrorWhileSaving (sprintf "%A" ex) |> Error
            | ex -> CustomerError.UnexpectedError (sprintf "%A" ex) |> Error

    let runIfOk func result =
        match result with
        | Error err -> Error err
        | Ok res -> func res

    let createCustomer customerDto =
        customerDto
        |> Ok
        |> runIfOk validateDto
        |> runIfOk convertToCustomer
        |> runIfOk saveCustomer

    type HttpResponse = { StatusCode: int; Body: string }

    let createCustomerEndpoint customerDto =
        let result = createCustomer customerDto
        match result with
        | Ok customer -> { StatusCode = 200; Body = "Customer saved successfully" }
        | Error err ->
            match err with
            | CustomerError.ValidationFailed -> { StatusCode = 400; Body = "the input customer is not valid!" }
            | CustomerError.DbErrorWhileSaving msg -> { StatusCode = 424; Body = "error while saving the customer to the database!" }
            | CustomerError.UnexpectedError msg -> { StatusCode = 500; Body = "an unexpected error occurred! Please contact the administrator." }


    // ----------------------------------------------------------------------------------------
    // Tests

    Db.clear ()
    Db.store

    let response1 = createCustomerEndpoint { FirstName = "Tomek"; LastName = "Nowak"; Age = 12 }
    let response2 = createCustomerEndpoint { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }
    let response3 = createCustomerEndpoint { FirstName = "Tomek"; LastName = "Kowalski"; Age = 18 }
    let response4 = createCustomerEndpoint { FirstName = "Tomek"; LastName = "Nowak"; Age = 18 }