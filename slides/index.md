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

***

### What's a <ct>Type System</ct>?

***

### <ct>Typing:</ct>
### Dynamic vs Static

---

### <ct>Dynamic</ct>

types are checked at <ct>runtime</ct> -> runtime errors

---

### <ct>Static</ct>

Types are checked at <ct>compile time</ct> -> compilation errors

***

### <ct>Typing:</ct>
### Weak vs Strong

---

### <ct>Weak</ct>

Type checking is not very strict

no guarantees on the program execution  

implicit conversions (usually)

---

### <ct>Strong</ct>

Type checking is strict

provides guarantees on the

conversions must be explicit

***

### <ct>Typing:</ct>
### Nominal vs Structural

rather using for static types

***

### <ct>Duck</ct> Typing

<quote>If it looks like a duck, swims like a duck, and quacks like a duck, then it probably is a duck.</quote>

similar to stuctural typing, but for dynamic types

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
