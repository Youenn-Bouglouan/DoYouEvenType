- title : Do you even type, Bro?
- description : Stuff about Types!
- author : Youenn Bouglouan
- theme : night
- transition : default

***

## Do you even type, Bro?

***

### <ct>Who am I?</ct>

Youenn Bouglouan

C# by day, F# by night

 http://www.ybouglouan.pl

---

### <ct>Why today's topic?</ct>

<p class="fragment fade-in">Waterfall vs Agile</p>

<p class="fragment fade-in">Java vs C#</p>

<p class="fragment fade-in">PC vs Mac (vs Linux)</p>

<p class="fragment fade-in">Object Oriented vs Functional</p>

<ct><p class="fragment fade-in">Weakly Typed vs Strongly Typed</p></ct>

---

### <ct>Who is this presentation for?</ct>

<p class="fragment fade-in">Developers -> get a better idea of what's out there</p>

<p class="fragment fade-in">QA Specialists -> understand why there are bugs and issues</p>

***

#### Part I
____
### <ct>Typing 101</ct>

***

### What's a <ct>Type</ct>?

A way to represent <ct>data</ct> or <ct>behavior</ct> within a programming language

* <ct>primitive types</ct> (int, bool, char, float...)

* <ct>product types</ct> (classes, records, objects, tuples...)

* <ct>sum types</ct> (unions but not only... we'll see those later!)

* <ct>sets</ct> (lists, arrays, maps, dictionaries...)

* <ct>functions</ct>(!)

* <ct>interfaces</ct>(!)

***

### What's a <ct>Type System</ct>?

For a given language:

* defines the <ct>kind of types</ct> available

* <ct>maps types</ct> to the various <ct>constructs</ct> (expressions, variables, functions...  )

* determines how types <ct>interact</ct> with each other

* sets the <ct>rules</ct> that make a type either valid or invalid

***

### <ct>Typing:</ct>
### Dynamic vs Static

---

### <ct>Dynamic</ct>

types are checked at <ct>runtime</ct> -> runtime errors

```fsharp
// Python
x = 10
y = x + 1 // Ok
z = x + "1" // Runtime error
// TypeError: unsupported operand type(s) for +: 'int' and 'str'
```

---

### <ct>Static</ct>

Types are checked at <ct>compile time</ct> -> compilation errors

```fsharp
// F#
let x = 10
let y = x + 1 // Ok
let z = x + "1" // Compiler error
// The type 'string' does not match the type 'int'
```

***

### <ct>Typing:</ct>
### Weak vs Strong

Not the same as Dynamic vs Static!

---

### <ct>Weak</ct>

type checking is not (very) strict

implicit conversions (usually)

little guarantees on the program's correctness

---

### <ct>Weak</ct> typing in a <ct>dynamic</ct> language

```fsharp
// JavaScript
'1' + '2' // returns the string "12"
'1' - '2' // returns the number -1
'1' * '2' // returns the number 2
var myObject = { valueOf: function () { return 3 }} // myObject is an Object
'1' + myObject // returns a string: "13"
1 + myObject // returns a string: 4
[] + {} // returns an Object
{} + [] // returns the number 0... wait whaaaaaaaaaaaaaaaaaaaat?
```

---

### <ct>Weak</ct> typing in a <ct>static</ct> language - 1

```fsharp
// C#
var x = 2 + 2.0; // x is a double with value 4
var y = 2.1 + "2"; // y is a string with value "2.12"

char c = 'a';
int i = c; // i is an integer with value 97

int x = 100000;
int y = (short) x; // y is an integer with value -31072 due to integer overflow
```

---

### <ct>Weak</ct> typing in a <ct>static</ct> language - 2

```fsharp
// C#
public class Animal {}
public class Reptile : Animal {}
public class Mammal : Animal {}

public void Test(Animal a)
{
  var r = (Reptile) a;
}

Test(new Mammal());
// Run-time exception: Unable to cast object of type 'Mammal' to type 'Reptile'.
```

---

### <ct>Strong</ct>

Type checking is strict(er)

program's correctness is easier to prove (well, in theory)

conversions must be explicit

---

### <ct>Strong</ct> typing in a <ct>dynamic</ct> language

Back to our first example in Python!

```fsharp
// Python
x = 10
y = x + 1 // Ok
z = x + "1" // Runtime error
// TypeError: unsupported operand type(s) for +: 'int' and 'str'
```

---

### <ct>Strong</ct> typing in a <ct>static</ct> language

```fsharp
// F#
let x = 2 + 2.0 // Compiler error, The type 'float' does not match the type 'int'
let y = 2.1 + "2" // Compiler error, The type 'string' does not match the type 'float'

let c: char = 'a'
let i: int = c // Compiler error, expected to have type 'int' but here has type 'char'

let x: int = 100000
let y: int = int16 x // Compiler error, expected to have type 'int' but here has type 'int16'
let z = int16 x // this compiles and runs, but the result still is -31072
```

***

### <ct>Typing:</ct>
### Nominal vs Structural

applies to static typing

---

### <ct>Nominal</ct>

Very popular in mainstream languages like C#, C++, or Java  

Types are identified by their respective <ct>names</ct>

```fsharp
// C#
class Employee { public string Name; }
class Animal { public string Name; }

string Hire(Employee employee)
{
  // some fancy logic here...
  return employee.Name;
}

var name = Hire(new Employee()); // This works
var name = Hire(new Animal()); // This doesn't as 'Hire' explicitly expects an 'Employee'
```

---

### <ct>Structural</ct>

Also called row polymorphism

Types are identified by their respective <ct>structures</ct> and <ct>properties</ct>

Present under different forms in Elm, Go, TypeScript, Scala, OCaml, Haskell...

```fsharp
// Elm
hire: { name: String } -> String
hire entity =
  // Some fancy logic here...
  entity.name

hire { name = "Tomek Nowak" } // This works, returns the string 'Tomek Nowak'
hire { name = "Garfield" } // This also works, returns the string 'Garfield'
```

---
But there's more...

### Structural <ct>subtyping</ct>

Beware the awesomeness!

```fsharp
// Elm
type alias TypeWithName a = { a | name: String }

hire: TypeWithName a -> String
hire entity =
  // Some fancy logic here...
  entity.name

hire { name = "Tomek Nowak", age = 25, gender = "male" } // This still works!
hire { name = "Garfield", canFly = False, hasPaws = True } // And this works too!
```

The actual structure of the types is checked as compile-time,
making this super safe while giving a dynamic feel to the language

---

### <ct>Structural</ct> typing
#### for <ct>implicit</ct> interface implementation

```fsharp
// Go
type Stringer interface {
  String() string
}

import ("fmt")

type User struct {
  name string
}

func (user User) String() string {
  return fmt.Sprintf("User: name = %s", user.name)
}

func main() {
  user := User{name: "Tomek Nowak"}
  fmt.Println(user) // fmt.Println(...) takes a Stringer interface as parameter
  //prints 'User: name = Tomek Nowak'
}
```

***

### <ct>Duck</ct> Typing

<quote>If it looks like a duck, swims like a duck, and quacks like a duck, then it probably is a duck.</quote>

basically the same as structural typing, but at <ct>runtime</ct>!

```fsharp
// Python
class Duck:
    def quack(self):
        print("Quack!")

class Dog:
    def bark(self):
        print("Woooof!")

def lets_quack(animal):
  animal.quack()

donaldTusk = Duck()
rex = Dog()

lets_quack(donaldTusk) // prints 'Quack!'
lets_quack(rex) // runtime error!
```

***

### A word about <ct>Type Inference</ct>

 <img src="images/java-type-inference.png" style="background: white;" width=500 />
 <small>https://twitter.com/java/status/967463386609954816</small>

---

 <img src="images/java-type-inference-com1.png" style="background: white;" width=540 />
 <img src="images/java-type-inference-com2.png" style="background: white;" width=540 />
 <img src="images/java-type-inference-com4.png" style="background: white;" width=540 /> 

---

### Basic <ct>Type Inference</ct>

```fsharp
// C++
void withoutTypeInference(std::vector<std::complex<double>> & myVector)
{
  std::vector<std::complex<double>>::const_iterator it = myVector.begin();
  // ...
}

void withTypeInference(std::vector<std::complex<double>> & myVector)
{
  auto it = myVector.begin();
  // ...
}
```

---

### Advanced <ct>Type Inference</ct>

```fsharp
// F#
type Customer = {
  Id: Guid
  Name: string
  Age: int
}

// This is equivalent to:
// public Customer createCustomer(string name, int age) {...}
let createCustomer name age =
  {
    Id = Guid.NewGuid()
    Name = name
    Age = age
  }

// This will generate a compiler error
let newCustomer = createCustomer 18 "Tomek Nowak"

```

 <small>https://en.wikipedia.org/wiki/Hindley%E2%80%93Milner_type_system</small>

***

#### Part II

____

#### <ct>So, do you even type??</ct>

***

#### Let's start with a <ct>simple example</ct>

```fsharp
public interface ICustomerService
{
  Customer CreateCustomer(CustomerDto dto);
}

public class CustomerService : ICustomerService
{
  public Customer CreateCustomer(CustomerDto dto)
  {
    // implementation here
  }
}
```

---

#### A possible and <ct>plausible</ct> implementation

```fsharp
public Customer CreateCustomer(CustomerDto dto)
{
  var isValid = Validate(dto);
  if (isValid)
  {
    var newCustomer = _dbService.SaveCustomer(dto);
    return newCustomer;
  }
  else
  {
    return null;
  }
}

private bool Validate(CustomerDto dto)
{
  return dto.Code != "" && dto.Contact.Email.IsValid() && dto.Age >= 18;
}

```

---

#### <ct>Possible outcomes</ct>


| value of dto | Result of Validate(...) | Result of CreateCustomer(...)
|:-:|:-:|:-:|
not null | true | new Customer
not null | false | <ct>null</ct> 
null | <ct>throws exception</ct> | failure

Other possible scenarios:

* _dbService is <ct>null</ct> -> <ct>throws exception</ct>

* _dbService <ct>throws exception</ct> -> failure

---

#### Let's take a <ct>step back</ct>

```fsharp
public interface ICustomerService
{
  Customer CreateCustomer(CustomerDto dto);
}
```

<h class="fragment fade-in">
<img src="https://media.giphy.com/media/3oEduX3zdIiqpfPJLO/giphy.gif" style="background: white;" width=540 />
</h>

---

#### Just another LOB app

```fsharp
public void DisplaySalesReport(DateRangeFilter dateRange)
{
  var canGenerateReport = await authService.GetPermissions(_session.GetCurrentUser());
  if (!canGenerateReport)
    return;
  
  var salesResults = await _salesService.GetSalesResults(dateRange, CurrentCountry);
  var products = await _productsService.RetrieveProductCatalog(dateRange.Year, ProductCategory);
  var salesReport = await ReportHelper.GenerateReport(salesResults, products, SelectedCustomers);
  Show(salesReport);
}

```

---

#### A few <ct>bug reports</ct> later...

```fsharp
public void DisplaySalesReport(DateRangeFilter dateRange)
{
  var canGenerateReport = await authService.GetPermissions(_session.GetCurrentUser());
  if (!canGenerateReport)
    return;
  
  if (dateRange != null && dateRange.Year != null)
  {
    var salesResults = await _salesService.GetSalesResults(dateRange, CurrentCountry);
    var products = await _productsService.RetrieveProductCatalog(dateRange.Year, ProductCategory);
    if (salesResults == null || products == null)
      return;

    if (SelectedCustomers == null)
      SelectedCustomers = new List<Customer>();
    var salesReport = await ReportHelper.GenerateReport(salesResults, products, SelectedCustomers);

    if (salesReport != null)
      Show(salesReport);
  }
}

```

---

#### Slapping a <ct>try-catch</ct> there too, just in case of!

```fsharp
public void DisplaySalesReport(DateRangeFilter dateRange)
{
  try
  {
    var canGenerateReport = await authService.GetPermissions(_session.GetCurrentUser());
    if (!canGenerateReport) return;
    
    if (dateRange != null && dateRange.Year != null)
    {
      var salesResults = await _salesService.GetSalesResults(dateRange, CurrentCountry);
      var products = await _productsService.RetrieveProductCatalog(dateRange.Year, ProductCategory);
      if (salesResults == null || products == null) return;

      if (SelectedCustomers == null) SelectedCustomers = new List<Customer>();
      var salesReport = await ReportHelper.GenerateReport(salesResults, products, SelectedCustomers);

      if (salesReport != null) Show(salesReport);
    }
  } catch (Exception)
  {
    Log.Error("oopsie... I guess we didn't handle all nulls and exceptions after all...")
  }
}
```

***

#### So now we know what the <ct>problem</ct> is
### <ct>exceptions</ct> and <ct>nulls</ct> make our code <ct>brittle and unreliable</ct>

 <small>http://www.ybouglouan.pl/2017/05/how-to-efficiently-break-your-code-by-using-exceptions/</small>

---

#### What can we do about it?

<h class="fragment fade-in">
### <ct>Get rid of nulls and exceptions altogether!</ct>
<img src="https://media.giphy.com/media/26ufdipQqU2lhNA4g/giphy.gif" style="background: white;" width=480 />
</h>

---

### Let's see how to achieve this in <ct>3</ct> little steps

***

####Step 1
### Introducing <ct>sum types</ct> and <ct>pattern matching</ct>

---

#### <ct>Sum types</ct> can be seen as <ct>enums on steroids</ct>!

Also called Choice Types, Discriminated Unions, Tagged Unions...   
Available in F#, Elm, Haskell, TypeScript, Scala, Rust, Swift...

#### Very powerful when combined with <ct>exhaustive pattern matching</ct>

---

#### <ct>Demo</ct> - sum types and pattern matching

---

#### <ct>sum types</ct> and <ct>pattern matching</ct> in a nutshell

```fsharp
// F#
type EntityId =
  | Customer of customerCode: string
  | SalesReceipt of documentId: Guid
  | Product of productId: integer

match entity with
| Customer code -> // the entity is a customer
| SalesReceipt id -> // the entity is a sales receipt
| Product id -> // the entity is a product
// The compiler makes sure you haven't forgotten a case!
```

***

####Step 2
### Getting rid of <ct>nulls</ct> using <ct>Option</ct>
#### Just a specific <ct>sum type</ct> with 2 cases!

---

#### <ct>Option</ct> makes the <ct>possible absence</ct> of value <ct>explicit</ct>
#### You have to deal with it at <ct>compile time</ct>

```fsharp
// F#
type Option<'a> =
  | Some of 'a
  | None
```

```fsharp
// Haskell and Elm
data Maybe a = Just a | Nothing
```

```fsharp
// Rust
enum Option<T> {
  None,
  Some(T),
}
```

---

####<ct>Demo</ct> - fix nulls using Option

---

#### <ct>Option</ct> in a nutshell

```fsharp
// F#
type Contact = {
  Email: string
  PhoneNumber: string option // Explicilty mark the phone number as not mandatory
}

match contact.PhoneNumber with
| Some phoneNumber -> // deal with the phone number
| None -> // deal with the absence of phone number
```

***

####Step 3
### Getting rid of <ct>exceptions</ct> using <ct>Result</ct>
#### Yet another <ct>sum type</ct>!

---

#### <ct>Result</ct> makes both the <ct>success</ct> and <ct>error</ct> path <ct>explicit</ct>

```fsharp
// F#
type Result<'T, 'Error> =
  | Ok of 'T
  | Error of 'TError
```

```fsharp
// OCaml
type ('a, 'b) result = Ok of 'a | Error of 'b type
```

```fsharp
// Rust
enum Result<T, E> {
  Ok(T),
  Err(E),
}
```

---

####<ct>Demo</ct> - fix exceptions using Result

---

#### <ct>Result</ct> in a nutshell

```fsharp
// F#
// Result<Customer, string> validateCustomer(Customer customer) {...}
let validateCustomer customer =
  if customer.Code <> "" && customer.Age >= 18 then
       Ok customer
  else
      Error "the customer is not valid!"
```

Enables Railway-Oriented Programming
https://fsharpforfunandprofit.com/rop/

***

### <ct>That's it!</ct>
#### What did we achieve?

---

#### <ct>Direct outcomes</ct>

 * no more null checks everywhere
 * no more exceptions and try-catches everywhere
 
---

#### <ct>Indirect outcomes</ct>

 * <ct>no more lies!</ct>
 * our models are more expressive (<ct>Option</ct>)
 * our code is more readable and explicit (<ct>Result</ct>)
 * the compiler is our friend -> <ct>"if it compiles, then it works"</ct>
 * <ct>TDD!</ct>

<p class="fragment fade-in"><small>*Type Driven Development</small></p>

---

### <ct>Where to go from here?</ct>

#### <ct>Learn new languages</ct> (Elm, Elixir, F#, Rust, TypeScript)

#### <ct>Explore new concepts</ct> (FP, Actor Model...)

---

### <ct>Thank You!</ct>
