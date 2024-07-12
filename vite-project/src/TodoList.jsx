import { TodoItem } from "./TodoItem"

export function TodoList({ todosProp, toggleTodo, handleDelete }){ // pass subset of props via destrction 

    return (
        <ul className="list">
            {todosProp.length === 0 && "No Todos (with short circuit render / AND evaluation)"}
            {todosProp.map(todo =>{
                return (
                    <TodoItem 
                    {...todo}
                    key={todo.id}
                    toggleTodo={toggleTodo}
                    handleDelete={handleDelete}
                    />
                )
            })}        
        </ul>
    )
}