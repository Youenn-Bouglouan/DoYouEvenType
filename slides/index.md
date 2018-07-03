- title : Do you even type, Bro?
- description : Introduction to Elm
- author : Youenn Bouglouan
- theme : night
- transition : default

***

## Do you even type, Bro?

***

a few words about this presentation  
prepared in just 4 hours
(todo add picture unicorn...)

***

### <ct>Typing 101</ct>
#### let's get those straight

***

### Things <ct>to do</ct> to start a <ct>holy</ct> war
### The <ct>developers</ct>' way

</br>

Spaces vs Tabs

Waterfall vs Agile

Java vs C#

PC vs Mac

Object Oriented vs Functional

Weakly Typed vs Strongly Typed

***

### What's a <ct>Type</ct>?

primitive types  
sum types  
choice types  
product types  
functions!  
interfaces  

***

### What's a <ct>Type System</ct>?

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
// TypeError: unsupported operand type(s) for +: 'int' and 'str'
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
'5' - '2' // returns the number -1
'5' * '2' // returns the number 10
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

### <ct>TODO</ct>

One more example in Go!

implicit interface implementation

***

### <ct>Duck</ct> Typing

<quote>If it looks like a duck, swims like a duck, and quacks like a duck, then it probably is a duck.</quote>

It's basically runtime structural typing!

---

### <ct>TODO</ct>

example of duck typing in Python

***

### A word about <ct>Type Inference</ct>

!link tweet to Java var introduction!

***

It's not always black and white

***

## So, what is the issue here?

<img src="images/elm-two-way-data-binding-angularjs.jpg" style="background: transparent; border-style: none;"  width=500>

---

## Hard to track when and where changes are made

 <img src="images/elm-angular-code.png" style="background: white;" width=500 />

---

## Programming languages 


| Functional  | Imperative  |
|:-:|:-:|
| series of expressions   | series of statements  |
| immutable state | mutable state |
| purity (same input -> same output)  | side effects |

--- 

## Yeah... but why ELM ?? 

### What is so unique in it?

* Functional but beginner-friendly 
    - Concepts are easy to understand and learn 
    - Nice introduction to the functional world
* Statically typed
* Immutable data
* Funtional types system - no null
* No runtime exceptions!
* Consistent management of state (famous ELM architecture)
* Interoperability with JavaScript
    * compiles to JS
* ELM time-travel debugger
* ...

---

## Ok, Ok, but... functional?!

---

### Functions

```elm
add1: number -> number
add1 x = x + 1

add: number -> number -> number
add x y = x + y
```

---

### Partial Application

```elm
add: number -> number -> number
add x y = x + y

add2: number -> number
add2 = add 2

// add2 y = add 2 y
```

---

### Function Composition

```elm
isEven: Int -> Bool
isEven n = n % 2 == 0

// (<<) (b -> c) -> (a -> b) -> a -> c
// not: Bool -> Bool

isOdd: Int -> Bool
isOdd: not << isEven
```

---

### Function Application

* pipe operator

```elm
(|>) : a -> (a -> b) -> b
// x |> f is the same as f x 

myFunction: List number -> String
myFunction someNumbers = 
  someNumbers
  |> List.filter (\x -> x > 2)
  |> List.length
  |> toString
// myFunction [1,2,3,4,5] returns "3"

// alternatively: 
myFunction = toString << List.length << List.filter (\x -> x > 2)
```

---

### Pattern matching

```elm
listSum : List number -> number
listSum myList =
    case myList of
        [] -> 0
        x :: xs -> x + listSum xs
// listSum [1,2] returns 3

listLength: List a -> number
listLength myList =
    case myList of
        [] -> 0
        _ :: xs -> 1 + listLength xs
// listLength [1,2] returns 2
```
---

### Functions as first class values

```elm
listFilter: (a -> Bool) -> List a -> List a
listFilter fn myList =
    case myList of 
        [] -> []
        x::xs -> 
            if fn x then x::(listFilter fn xs)
            else listFilter fn xs
// listFilter (\x -> x == 2) [1,2,1,2,1,2] returns [2,2,2]
```
---

### Type Aliases

```elm
type alias UserName = String
type alias UserId = Int

type alias User = 
    { name: UserName
    , id: UserId
    }
// types as a helpful design tool!
```

---

### Union Types

```elm
type Msg 
    = Increment
    | Decrement
    | SetValue Int
```

---

### Tuples

```elm
myTuple = ("A", "B", "C")
myNestedTuple = ("A", "B", "C", ("X", "Y", "Z"))

let
  (a,b,c) = myTuple
in 
  a ++ b ++ c
// result is "ABC"

let
  (a,b,c,(x,y,z)) = myNestedTuple
in
  a ++ b ++ c ++ x ++ y ++ z
// result is "ABCXYZ"
```

---

### Tuples - pattern matching

```elm
isOrdered: (String, String, String) -> String
isOrdered tuple =
 case tuple of
  ("A","B","C") as orderedTuple ->
    toString orderedTuple ++ " is an ordered tuple."
    
  (_,_,_) as unorderedTuple ->
    toString unorderedTuple ++ " is an unordered tuple."


> isOrdered myTuple
"(\"A\",\"B\",\"C\") is an ordered tuple."

> isOrdered ("B", "C", "A")
"(\"B\",\"C\",\"A\") is an unordered tuple."
```

---

### Examples ...

```elm
type Visibility = All | Active | Completed

> All
All : Visibility

> Active
Active : Visibility

> Completed
Completed : Visibility
```

---

### ... Examples ...

```elm
type alias Task = { task : String, complete : Bool }

buy : Task
buy =
  { task = "Buy milk", complete = True }

drink : Task
drink =
  { task = "Drink milk", complete = False }

tasks : List Task
tasks =
  [ buy, drink ]
```

---

### ... Examples

```elm
type Visibility = All | Active | Completed
type alias Task = { task : String, complete : Bool }

keep: Visibility -> List Task -> List Task
keep visibility tasks =
  case visibility of
    All -> tasks
    Active -> List.filter (\task -> not task.complete) tasks
    Completed -> List.filter (\task -> task.complete) tasks

// keep All tasks      returns [buy,drink]
// keep Active tasks   returns [drink]
// keep Complete tasks returns [buy]
```

*** 

### Model - View - Update

### "Elm - Architecture"

 <img src="images/Elm.png" style="background: white;" width=700 />


 <small>http://danielbachler.de/2016/02/11/berlinjs-talk-about-elm.html</small>


---

### Model - View - Update


```elm
type alias Model = (...) // record type

type Msg = (...) // union type

update: Msg -> Model -> (Model, Cmd Msg)
update msg model = (...)

view: Model -> Html Msg
view model = (...)
```
---

### Model - View - Update


```elm
type Msg 
    = Increment 
    | Decrement

update msg model =
  case msg of
    Increment ->
      model + 1

    Decrement ->
      model - 1

```
---

### Model - View - Update


```elm
view model =
  div []
    [ button [ onClick Decrement ] [ text "-" ]
    , div [] [ text (toString model) ]
    , button [ onClick Increment ] [ text "+" ]
    ]
```

<br/>
<br/>

---

### Virtual DOM - Initial

<br />
<br />


 <img src="images/onchange_vdom_initial.svg" style="background: white;" />

<br />
<br />

 <small>http://teropa.info/blog/2015/03/02/change-and-its-detection-in-javascript-frameworks.html</small>

---

### Virtual DOM - Change

<br />
<br />


 <img src="images/onchange_vdom_change.svg" style="background: white;" />

<br />
<br />

 <small>http://teropa.info/blog/2015/03/02/change-and-its-detection-in-javascript-frameworks.html</small>

---

### Virtual DOM - Reuse

<br />
<br />


 <img src="images/onchange_immutable.svg" style="background: white;" />

<br />
<br />

 <small>http://teropa.info/blog/2015/03/02/change-and-its-detection-in-javascript-frameworks.html</small>


---

### Model - View - Update

# [Demo](http://elm-lang.org/examples/buttons)

***

### TakeAways

* Learn all the FP you can!
* Simple modular design
* .Net fans? F# (Fable) + Elm!  
* [elm-lang.org/try](http://elm-lang.org/try)

*** 

### Thank you!
* https://www.youtube.com/watch?v=vgsckgtVdoQ
* https://gist.github.com/yang-wei/4f563fbf81ff843e8b1e
* https://github.com/fable-compiler/fable-elmish
* https://ionide.io
* https://facebook.github.io/react-native/
