import { useEffect, useState } from "react"
import "./styles.css"
import { NewTodoForm } from "./NewTodoForm"
import { TodoList } from "./TodoList"
import { ApiComponent } from "./ApiClient"

function App() {

  //Destructing Array Syntax
  const [todos, setTodos] = useState(()=>{
    const localStoredTodos = localStorage.getItem("TODOITEMS")
    if(localStoredTodos == null) return []
    return JSON.parse(localStoredTodos)
  })

  useEffect(()=>{
    localStorage.setItem("TODOITEMS", JSON.stringify(todos))
  }, [todos])

  function addTodo(title){
    // here calling set state with function param
    setTodos((currentTodos)=>{
      // ... is spread operator used to append to array (concatenate on end) ?due to immuntability?
      return [...currentTodos, {id: crypto.randomUUID(), title: title, completed: false}]
    })
  }

  function toggleTodo(id, completed){
    setTodos((currentTodos)=>{
      return currentTodos.map(todo=>{
        if(todo.id == id){
          return {...todo, completed}
        }

        return todo
      })
    })
  }

  function handleDelete(id){
    setTodos(currentTodos =>{
      return currentTodos.filter(todo => todo.id !== id)
    })
  }

  return  (
    <>  
      <NewTodoForm onSubmitFunction={addTodo} />

      <h1 className="header">Todo List</h1>
      <TodoList todosProp={todos} toggleTodo={toggleTodo} handleDelete={handleDelete} />

      <ApiComponent/>
    </>
  )
}

export default App
