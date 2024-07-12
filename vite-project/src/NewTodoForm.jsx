import { useState } from "react"

export function NewTodoForm(props){
    const [newItem, setNewItem] = useState("") //Destructing Array Syntax
  
    function handleSubmit(e){
      e.preventDefault() //prevent default processing of event (Clicking on a "Submit" button, prevent it from submitting a form)
  
      if(newItem === "") return
      props.onSubmitFunction(newItem)
      
      setNewItem("")
    }
  
    return  (
      <>  
        <form onSubmit={handleSubmit} className="new-item-form">
          <div className="form-row">
            <label htmlFor="item">New Item</label>
            <input 
              value={newItem} 
              onChange={e => setNewItem(e.target.value)}
              type="text"
              id="item">
            </input>
          </div>
          <button className="btn">Add</button>
        </form>
      </>
    )
}