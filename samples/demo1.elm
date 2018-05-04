-- Read more about this program in the official Elm guide:
-- https://guide.elm-lang.org/architecture/user_input/buttons.html

import Html exposing (beginnerProgram, div, button, text)
import Html.Events exposing (onClick)


type alias Model =
  {
    value: Int
  }
  
type Msg 
  = Increment 
  | Decrement
  | SetValue Int
  
main =
  beginnerProgram { model = (Model 100), view = view, update = update }

view: Model -> Html Msg
view model =
  div [] [
    div []
      [ button [ onClick Decrement ] [ text "-" ]
      , div [] [ text (toString model.value) ]
      , button [ onClick Increment ] [ text "+" ]    
      ]    
    , div [] 
      [ button [ onClick (SetValue 0) ] [ text "Reset" ]
      ]
  ]
  




update : Msg -> Model -> Model
update msg model =
  case msg of
    Increment ->
      {model | value = model.value + 1}

    Decrement ->
      {model | value = model.value - 1}
      
    SetValue value ->
      {model | value = value}
